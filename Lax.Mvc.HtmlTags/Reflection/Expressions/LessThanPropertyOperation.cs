﻿using System.Linq.Expressions;

namespace Lax.Mvc.HtmlTags.Reflection.Expressions {

    public class LessThanPropertyOperation : BinaryComparisonPropertyOperation {

        public LessThanPropertyOperation()
            : base(ExpressionType.LessThan) { }

        public override string OperationName => "LessThan";

        public override string Text => "less than";

    }

}