using System;
using System.Linq;
using System.Linq.Expressions;

namespace Lax.Mvc.HtmlTags.Reflection.Expressions {

    public class RewriteToLambda : ExpressionVisitor {

        private readonly ParameterExpression _parameter;

        public RewriteToLambda(ParameterExpression parameter) => _parameter = parameter;

        protected override Expression VisitBinary(BinaryExpression exp) {
            var a = VisitMember((MemberExpression) exp.Left);
            return Expression.Equal(a, exp.Right);
        }

        protected override Expression VisitMember(MemberExpression m) {
            Expression exp = null;
            switch (m.Expression.NodeType) {
                case ExpressionType.Parameter:
                    //c.IsThere
                    exp = Expression.MakeMemberAccess(_parameter, m.Member);
                    break;
                case ExpressionType.MemberAccess: {
                    //c.Thing.IsThere

                    //rewrite c.Thing
                    var intermediate = VisitMember((MemberExpression) m.Expression);

                    //now combine 'c.Thing' with '.IsThere'
                    exp = Expression.MakeMemberAccess(intermediate, m.Member);
                    break;
                }
            }

            return exp ?? throw new InvalidOperationException();
        }

        protected override Expression VisitMethodCall(MethodCallExpression exp) {
            if (!exp.Method.IsStatic) {
                return Expression.Call(_parameter, exp.Method, exp.Arguments.First());
            }

            var aa = exp.Arguments.Skip(1).First();
            if (aa.NodeType != ExpressionType.Constant) {
                aa = VisitMember((MemberExpression) exp.Arguments.Skip(1).First());
            }

            //if second arg is a constant of our type swap other wise continue down the rabbit hole
            var args = new[] {exp.Arguments.First(), aa};
            return Expression.Call(exp.Method, args);
        }

    }

}