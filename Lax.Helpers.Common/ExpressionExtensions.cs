using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Lax.Helpers.Common {

    public static class ExpressionExtensions {

        public static PropertyInfo GetPropertyAccess(this LambdaExpression propertyAccessExpression) {
            Debug.Assert(propertyAccessExpression.Parameters.Count == 1);

            var parameterExpression = propertyAccessExpression.Parameters.Single();
            var propertyInfo = parameterExpression.MatchSimplePropertyAccess(propertyAccessExpression.Body);


            var declaringType = propertyInfo.DeclaringType;
            var parameterType = parameterExpression.Type;

            if (declaringType == null || declaringType == parameterType || !declaringType.GetTypeInfo().IsInterface ||
                !declaringType.GetTypeInfo().IsAssignableFrom(parameterType.GetTypeInfo())) {
                return propertyInfo;
            }

            var propertyGetter = propertyInfo.GetMethod;
            var interfaceMapping = parameterType.GetTypeInfo().GetRuntimeInterfaceMap(declaringType);
            var index = Array.FindIndex(interfaceMapping.InterfaceMethods, p => p == propertyGetter);
            var targetMethod = interfaceMapping.TargetMethods[index];
            foreach (var runtimeProperty in parameterType.GetRuntimeProperties()) {
                if (targetMethod == runtimeProperty.GetMethod) {
                    return runtimeProperty;
                }
            }

            return propertyInfo;
        }

        public static IReadOnlyList<PropertyInfo>
            GetPropertyAccessList(this LambdaExpression propertyAccessExpression) {
            Debug.Assert(propertyAccessExpression.Parameters.Count == 1);

            var propertyPaths
                = MatchPropertyAccessList(propertyAccessExpression, (p, e) => e.MatchSimplePropertyAccess(p));


            return propertyPaths;
        }

        private static IReadOnlyList<PropertyInfo> MatchPropertyAccessList(
            this LambdaExpression lambdaExpression, Func<Expression, Expression, PropertyInfo> propertyMatcher) {
            Debug.Assert(lambdaExpression.Body != null);

            if (RemoveConvert(lambdaExpression.Body) is NewExpression newExpression) {
                var parameterExpression
                    = lambdaExpression.Parameters.Single();

                var propertyInfos
                    = newExpression
                        .Arguments
                        .Select(a => propertyMatcher(a, parameterExpression))
                        .Where(p => p != null)
                        .ToList();

                return propertyInfos.Count != newExpression.Arguments.Count ? null : propertyInfos;
            }

            var propertyPath
                = propertyMatcher(lambdaExpression.Body, lambdaExpression.Parameters.Single());

            return propertyPath != null ? new[] { propertyPath } : null;
        }

        private static PropertyInfo MatchSimplePropertyAccess(
            this Expression parameterExpression, Expression propertyAccessExpression) {
            var propertyInfos = MatchPropertyAccess(parameterExpression, propertyAccessExpression);

            return (propertyInfos != null) && (propertyInfos.Length == 1) ? propertyInfos[0] : null;
        }

        public static PropertyInfo[] GetComplexPropertyAccess(this LambdaExpression propertyAccessExpression) {
            Debug.Assert(propertyAccessExpression.Parameters.Count == 1);

            var propertyPath
                = propertyAccessExpression
                    .Parameters
                    .Single()
                    .MatchComplexPropertyAccess(propertyAccessExpression.Body);


            return propertyPath;
        }

        private static PropertyInfo[] MatchComplexPropertyAccess(
            this Expression parameterExpression, Expression propertyAccessExpression) {
            var propertyPath = MatchPropertyAccess(parameterExpression, propertyAccessExpression);

            return propertyPath;
        }

        private static PropertyInfo[] MatchPropertyAccess(
            this Expression parameterExpression, Expression propertyAccessExpression) {
            var propertyInfos = new List<PropertyInfo>();

            MemberExpression memberExpression;

            do {
                memberExpression = RemoveConvert(propertyAccessExpression) as MemberExpression;

                var propertyInfo = memberExpression?.Member as PropertyInfo;

                if (propertyInfo == null) {
                    return null;
                }

                propertyInfos.Insert(0, propertyInfo);

                propertyAccessExpression = memberExpression.Expression;
            } while (memberExpression.Expression.RemoveConvert() != parameterExpression);

            return propertyInfos.ToArray();
        }

        public static Expression RemoveConvert(this Expression expression) {
            while ((expression != null)
                   && ((expression.NodeType == ExpressionType.Convert)
                       || (expression.NodeType == ExpressionType.ConvertChecked))) {
                expression = RemoveConvert(((UnaryExpression)expression).Operand);
            }

            return expression;
        }

        public static TExpression GetRootExpression<TExpression>(this Expression expression)
            where TExpression : Expression {
            MemberExpression memberExpression;
            while ((memberExpression = expression as MemberExpression) != null) {
                expression = memberExpression.Expression;
            }

            return expression as TExpression;
        }

        public static Expression StripQuotes(this Expression node) {
            while (node.NodeType == ExpressionType.Quote) {
                node = ((UnaryExpression)node).Operand;
            }

            return node;
        }

        public static bool IsConstant(this Expression node, object value) =>
            node.StripQuotes() is ConstantExpression &&
            Equals(value, (node.StripQuotes() as ConstantExpression)?.Value);

        public static MemberInfo GetMemberInfo(this LambdaExpression lambda) {
            var body = lambda.Body.StripQuotes();
            if (body.NodeType.In(new[] { ExpressionType.Convert, ExpressionType.ConvertChecked })) {
                body = ((UnaryExpression)body).Operand;
            }

            if (body.NodeType == ExpressionType.MemberAccess) {
                return ((MemberExpression)body).Member;
            }

            throw new ArgumentException($"{lambda} is not a valid field or property accessor.");
        }

    }

}