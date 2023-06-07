using System;
using System.Reflection;

namespace Lax.Mvc.HtmlTags.Reflection.Expressions {

    public class StringDoesNotStartWithPropertyOperation : CaseInsensitiveStringMethodPropertyOperation {

        private static readonly MethodInfo Method =
            ReflectionHelper.GetMethod<string>(s => s.StartsWith("", StringComparison.CurrentCulture));

        public StringDoesNotStartWithPropertyOperation()
            : base(Method, true) { }

        public override string OperationName => "DoesNotStartWith";

        public override string Text => "does not start with";

    }

}