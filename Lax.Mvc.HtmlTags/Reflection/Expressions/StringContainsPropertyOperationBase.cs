using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Lax.Mvc.HtmlTags.Reflection.Expressions {

    public abstract class StringContainsPropertyOperationBase : IPropertyOperation {

        private static readonly MethodInfo IndexOfMethod;
        private readonly bool _negate;

        static StringContainsPropertyOperationBase() =>
            IndexOfMethod =
                ReflectionHelper.GetMethod<string>(s => s.IndexOf("", StringComparison.OrdinalIgnoreCase));

        protected StringContainsPropertyOperationBase(string operation, string description, bool negate) {
            OperationName = operation;
            Text = description;
            _negate = negate;
        }

        public string OperationName { get; }

        public string Text { get; }

        public Func<object, Expression<Func<T, bool>>> GetPredicateBuilder<T>(MemberExpression propertyPath) =>
            valueToCheck => {
                var valueToCheckConstant = Expression.Constant(valueToCheck);
                var indexOfCall =
                    Expression.Call(Expression.Coalesce(propertyPath, Expression.Constant(string.Empty)),
                        IndexOfMethod,
                        valueToCheckConstant,
                        Expression.Constant(StringComparison.OrdinalIgnoreCase));
                var operation = _negate ? ExpressionType.LessThan : ExpressionType.GreaterThanOrEqual;
                var comparison = Expression.MakeBinary(operation, indexOfCall,
                    Expression.Constant(0));
                var lambdaParameter = propertyPath.GetParameter<T>();
                return Expression.Lambda<Func<T, bool>>(comparison, lambdaParameter);
            };

    }

}