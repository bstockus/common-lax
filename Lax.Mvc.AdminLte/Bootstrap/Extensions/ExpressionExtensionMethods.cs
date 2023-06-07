using System.Collections.Generic;
using System.Linq.Expressions;

namespace Lax.Mvc.AdminLte.Bootstrap.Extensions {

    public static class ExpressionExtensionMethods {

        public static MemberExpression GetMemberName(this Expression body) {
            var candidates = new Queue<Expression>();
            candidates.Enqueue(body);
            while (candidates.Count > 0) {
                var expr = candidates.Dequeue();
                switch (expr) {
                    case MemberExpression memberExpression:
                        return memberExpression;
                    case UnaryExpression unaryExpression:
                        candidates.Enqueue(unaryExpression.Operand);
                        break;
                    case BinaryExpression binaryExpression:
                        candidates.Enqueue(binaryExpression.Left);
                        candidates.Enqueue(binaryExpression.Right);
                        break;
                    case MethodCallExpression methodCallExpression:
                        foreach (var argument in methodCallExpression.Arguments) {
                            candidates.Enqueue(argument);
                        }

                        break;
                    case LambdaExpression lambdaExpression:
                        candidates.Enqueue(lambdaExpression.Body);
                        break;
                }
            }

            return null;
        }

    }

}