using System;
using System.Reflection;

namespace Lax.Mvc.HtmlTags.Reflection.Expressions {

    public class StringNotEqualPropertyOperation : CaseInsensitiveStringMethodPropertyOperation {

        private static readonly MethodInfo Method =
            ReflectionHelper.GetMethod<string>(s => s.Equals("", StringComparison.CurrentCulture));

        public StringNotEqualPropertyOperation()
            : base(Method, true) { }

        public override string OperationName => "DoesNotEqual";

        public override string Text => "is not";

    }

}