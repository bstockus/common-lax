using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Lax.Data.SharePoint.Rest {

    public static class LambdaHelpers {

        public static MemberInfo GetMemberInfo(LambdaExpression lambda) {
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