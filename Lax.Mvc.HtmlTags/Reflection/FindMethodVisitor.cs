using System.Linq.Expressions;
using System.Reflection;

namespace Lax.Mvc.HtmlTags.Reflection {

    public sealed class FindMethodVisitor : ExpressionVisitor {

        public FindMethodVisitor(Expression expression) => Visit(expression);

        public MethodInfo Method { get; private set; }

        protected override Expression VisitMethodCall(MethodCallExpression m) {
            Method = m.Method;
            return m;
        }

    }

}