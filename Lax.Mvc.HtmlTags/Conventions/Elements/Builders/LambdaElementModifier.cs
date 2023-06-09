using System;

namespace Lax.Mvc.HtmlTags.Conventions.Elements.Builders {

    // Tested through HtmlConventionRegistry
    public class LambdaElementModifier : LambdaTagModifier, IElementModifier {

        public LambdaElementModifier(Func<ElementRequest, bool> matcher, Action<ElementRequest> modify)
            : base(matcher, modify) { }

        public LambdaElementModifier(Action<ElementRequest> modify) : base(modify) { }

        public string ConditionDescription { get; set; }
        public string ModifierDescription { get; set; }

    }

}