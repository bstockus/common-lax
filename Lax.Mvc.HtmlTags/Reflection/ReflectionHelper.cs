using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Lax.Mvc.HtmlTags.Reflection {

    internal static class ReflectionHelper {

        public static bool MeetsSpecialGenericConstraints(Type genericArgType, Type proposedSpecificType) {
            var gpa = genericArgType.GetTypeInfo().GenericParameterAttributes;
            var constraints = gpa & GenericParameterAttributes.SpecialConstraintMask;

            // No constraints, away we go!
            if (constraints == GenericParameterAttributes.None) {
                return true;
            }

            // "class" constraint and this is a value type
            if ((constraints & GenericParameterAttributes.ReferenceTypeConstraint) != 0
                && proposedSpecificType.GetTypeInfo().IsValueType) {
                return false;
            }

            // "struct" constraint and this is not a value type
            if ((constraints & GenericParameterAttributes.NotNullableValueTypeConstraint) != 0
                && !proposedSpecificType.GetTypeInfo().IsValueType) {
                return false;
            }

            // "new()" constraint and this type has no default constructor
            return (constraints & GenericParameterAttributes.DefaultConstructorConstraint) == 0 ||
                   proposedSpecificType.GetConstructor(Type.EmptyTypes) != null;
        }

        public static PropertyInfo GetProperty<TModel>(Expression<Func<TModel, object>> expression) {
            var memberExpression = GetMemberExpression(expression);
            return (PropertyInfo) memberExpression.Member;
        }

        public static PropertyInfo GetProperty<TModel, T>(Expression<Func<TModel, T>> expression) {
            var memberExpression = GetMemberExpression(expression);
            return (PropertyInfo) memberExpression.Member;
        }

        public static PropertyInfo GetProperty(LambdaExpression expression) {
            var memberExpression = GetMemberExpression(expression, true);
            return (PropertyInfo) memberExpression.Member;
        }

        private static MemberExpression GetMemberExpression<TModel, T>(Expression<Func<TModel, T>> expression) {
            MemberExpression memberExpression = null;
            switch (expression.Body.NodeType) {
                case ExpressionType.Convert: {
                    var body = (UnaryExpression) expression.Body;
                    memberExpression = body.Operand as MemberExpression;
                    break;
                }
                case ExpressionType.MemberAccess:
                    memberExpression = expression.Body as MemberExpression;
                    break;
            }


            if (memberExpression == null) {
                throw new ArgumentException("Not a member access", nameof(expression));
            }

            return memberExpression;
        }

        public static IAccessor GetAccessor(LambdaExpression expression) {
            var memberExpression = GetMemberExpression(expression, true);

            return GetAccessor(memberExpression);
        }

        public static MemberExpression GetMemberExpression(this LambdaExpression expression,
            bool enforceMemberExpression) {
            MemberExpression memberExpression = null;
            switch (expression.Body.NodeType) {
                case ExpressionType.Convert: {
                    var body = (UnaryExpression) expression.Body;
                    memberExpression = body.Operand as MemberExpression;
                    break;
                }
                case ExpressionType.MemberAccess:
                    memberExpression = expression.Body as MemberExpression;
                    break;
            }


            if (enforceMemberExpression && memberExpression == null) {
                throw new ArgumentException("Not a member access", nameof(enforceMemberExpression));
            }

            return memberExpression;
        }

        public static bool IsMemberExpression<T>(Expression<Func<T, object>> expression) =>
            IsMemberExpression<T, object>(expression);

        public static bool IsMemberExpression<T, TU>(Expression<Func<T, TU>> expression) =>
            GetMemberExpression(expression, false) != null;

        public static IAccessor GetAccessor<TModel>(Expression<Func<TModel, object>> expression) {
            if (expression.Body is MethodCallExpression || expression.Body.NodeType == ExpressionType.ArrayIndex) {
                return GetAccessor(expression.Body);
            }

            var memberExpression = GetMemberExpression(expression);

            return GetAccessor(memberExpression);
        }

        public static IAccessor GetAccessor(Expression memberExpression) {
            var list = new List<IValueGetter>();

            BuildValueGetters(memberExpression, list);

            switch (list.Count) {
                case 1 when list[0] is PropertyValueGetter:
                    return new SingleProperty(((PropertyValueGetter) list[0]).PropertyInfo);
                case 1 when list[0] is MethodValueGetter:
                    return new SingleMethod((MethodValueGetter) list[0]);
                case 1 when list[0] is IndexerValueGetter:
                    return new ArrayIndexer((IndexerValueGetter) list[0]);
                default:
                    list.Reverse();
                    return new PropertyChain(list.ToArray());
            }
        }

        private static void BuildValueGetters(Expression expression, IList<IValueGetter> list) {
            while (true) {
                switch (expression) {
                    case MemberExpression memberExpression: {
                        var propertyInfo = (PropertyInfo) memberExpression.Member;
                        list.Add(new PropertyValueGetter(propertyInfo));
                        if (memberExpression.Expression != null) {
                            BuildValueGetters(memberExpression.Expression, list);
                        }

                        break;
                    }
                    //deals with collection indexers, an indexer [0] will look like a get(0) method call expression
                    case MethodCallExpression methodCallExpression: {
                        var methodInfo = methodCallExpression.Method;
                        var argument = methodCallExpression.Arguments.FirstOrDefault();

                        if (argument == null) {
                            var methodValueGetter = new MethodValueGetter(methodInfo, new object[0]);
                            list.Add(methodValueGetter);
                        } else {
                            if (TryEvaluateExpression(argument, out var value)) {
                                var methodValueGetter = new MethodValueGetter(methodInfo, new object[] {value});
                                list.Add(methodValueGetter);
                            }
                        }


                        if (methodCallExpression.Object != null) {
                            BuildValueGetters(methodCallExpression.Object, list);
                        }

                        break;
                    }
                }

                if (expression.NodeType != ExpressionType.ArrayIndex) {
                    return;
                }

                var binaryExpression = (BinaryExpression) expression;

                var indexExpression = binaryExpression.Right;

                if (TryEvaluateExpression(indexExpression, out var index)) {
                    var indexValueGetter = new IndexerValueGetter(binaryExpression.Left.Type, (int) index);

                    list.Add(indexValueGetter);
                }

                expression = binaryExpression.Left;
            }
        }

        private static bool TryEvaluateExpression(Expression operation, out object value) {
            if (operation == null) {
                // used for static fields, etc
                value = null;
                return true;
            }

            switch (operation.NodeType) {
                case ExpressionType.Constant:
                    value = ((ConstantExpression) operation).Value;
                    return true;
                case ExpressionType.MemberAccess:
                    var me = (MemberExpression) operation;
                    if (TryEvaluateExpression(me.Expression, out var target)) {
                        switch (me.Member) {
                            // instance target
                            case FieldInfo fieldInfo:
                                value = fieldInfo.GetValue(target);
                                return true;
                            case PropertyInfo propertyInfo:
                                value = propertyInfo.GetValue(target, null);
                                return true;
                        }
                    }

                    break;
            }

            value = null;
            return false;
        }

        public static IAccessor GetAccessor<TModel, T>(Expression<Func<TModel, T>> expression) {
            var memberExpression = GetMemberExpression(expression);

            return GetAccessor(memberExpression);
        }

        public static MethodInfo GetMethod<T>(Expression<Func<T, object>> expression) =>
            new FindMethodVisitor(expression).Method;

        public static MethodInfo GetMethod(Expression<Func<object>> expression) => GetMethod<Func<object>>(expression);

        public static MethodInfo GetMethod(Expression expression) => new FindMethodVisitor(expression).Method;

        public static MethodInfo GetMethod<TDelegate>(Expression<TDelegate> expression) =>
            new FindMethodVisitor(expression).Method;

        public static MethodInfo GetMethod<T, TU>(Expression<Func<T, TU>> expression) =>
            new FindMethodVisitor(expression).Method;

        public static MethodInfo GetMethod<T, TU, TV>(Expression<Func<T, TU, TV>> expression) =>
            new FindMethodVisitor(expression).Method;

        public static MethodInfo GetMethod<T>(Expression<Action<T>> expression) =>
            new FindMethodVisitor(expression).Method;

    }

}