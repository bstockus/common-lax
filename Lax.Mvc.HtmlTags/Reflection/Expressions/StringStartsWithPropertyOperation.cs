using System;
using System.Reflection;

namespace Lax.Mvc.HtmlTags.Reflection.Expressions {

    public class StringStartsWithPropertyOperation : CaseInsensitiveStringMethodPropertyOperation {

        private static readonly MethodInfo Method =
            ReflectionHelper.GetMethod<string>(s => s.StartsWith("", StringComparison.CurrentCulture));

        public StringStartsWithPropertyOperation()
            : base(Method) { }

        public override string Text => "starts with";

    }

}