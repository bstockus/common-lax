using System;
using System.Reflection;

namespace Lax.Mvc.HtmlTags.Reflection.Expressions {

    public class StringDoesNotEndWithPropertyOperation : CaseInsensitiveStringMethodPropertyOperation {

        private static readonly MethodInfo Method =
            ReflectionHelper.GetMethod<string>(s => s.EndsWith("", StringComparison.CurrentCulture));

        public StringDoesNotEndWithPropertyOperation()
            : base(Method, true) { }

        public override string OperationName => "DoesNotEndWith";

        public override string Text => "does not end with";

    }

}