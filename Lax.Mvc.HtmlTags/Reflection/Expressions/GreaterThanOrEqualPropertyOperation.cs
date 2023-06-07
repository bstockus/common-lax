using System.Linq.Expressions;

namespace Lax.Mvc.HtmlTags.Reflection.Expressions {

    public class GreaterThanOrEqualPropertyOperation : BinaryComparisonPropertyOperation {

        public GreaterThanOrEqualPropertyOperation()
            : base(ExpressionType.GreaterThanOrEqual) { }

        public override string OperationName => "GreaterThanOrEqual";

        public override string Text => "greater than or equal to";

    }

}