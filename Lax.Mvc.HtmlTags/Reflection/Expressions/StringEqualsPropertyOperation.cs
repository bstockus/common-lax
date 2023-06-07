using System;
using System.Reflection;

namespace Lax.Mvc.HtmlTags.Reflection.Expressions {

    public class StringEqualsPropertyOperation : CaseInsensitiveStringMethodPropertyOperation {

        private static readonly MethodInfo Method =
            ReflectionHelper.GetMethod<string>(s => s.Equals("", StringComparison.CurrentCulture));

        public StringEqualsPropertyOperation()
            : base(Method) { }

        public override string Text => "is";

    }

}