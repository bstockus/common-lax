using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Lax.Mvc.HtmlTags.Reflection.Expressions {

    public class ComposableOrOperation {

        private readonly List<Tuple<IPropertyOperation, MemberExpression, object>> _listOfOperations =
            new List<Tuple<IPropertyOperation, MemberExpression, object>>();

        public void Set<T>(Expression<Func<T, object>> path, object value) {
            //why am I falling into here
            var memberExpression = path.GetMemberExpression(true);
            var operation = new EqualsPropertyOperation();
            _listOfOperations.Add(
                new Tuple<IPropertyOperation, MemberExpression, object>(operation, memberExpression, value));
        }


        public void Set<T>(Expression<Func<T, object>> path, IEnumerable<object> value) {
            var memberExpression = path.GetMemberExpression(true);
            var operation = new CollectionContainsPropertyOperation();
            _listOfOperations.Add(
                new Tuple<IPropertyOperation, MemberExpression, object>(operation, memberExpression, value));
        }

        public Expression<Func<T, bool>> GetPredicateBuilder<T>() {
            if (!_listOfOperations.Any()) {
                throw new Exception(
                    $"You must have at least one operation registered for an 'or' operation (you have {new[] {_listOfOperations.Count}})");
            }

            //the parameter to use
            var lambdaParameter = Expression.Parameter(typeof(T));

            var initialPredicate = Expression.Constant(false);
            Expression builtUpPredicate = initialPredicate;

            foreach (var operation in _listOfOperations) {
                var predBuilder = operation.Item1.GetPredicateBuilder<T>(operation.Item2);
                var predicate = predBuilder(operation.Item3);
                var expPredicate = Rebuild(predicate, lambdaParameter);
                builtUpPredicate = Expression.MakeBinary(ExpressionType.OrElse, builtUpPredicate, expPredicate);
            }

            return Expression.Lambda<Func<T, bool>>(builtUpPredicate, lambdaParameter);
        }

        private Expression Rebuild(Expression exp, ParameterExpression parameter) {
            var lb = (LambdaExpression) exp;
            var targetBody = lb.Body;

            return new RewriteToLambda(parameter).Visit(targetBody);
        }

    }

}