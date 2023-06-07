using System;
using System.Reflection;

namespace Lax.Mvc.HtmlTags.Reflection.Expressions {

    public class StringEndsWithPropertyOperation : CaseInsensitiveStringMethodPropertyOperation {

        private static readonly MethodInfo Method =
            ReflectionHelper.GetMethod<string>(s => s.EndsWith("", StringComparison.CurrentCulture));

        public StringEndsWithPropertyOperation()
            : base(Method) { }

        public override string Text => "ends with";

    }

}