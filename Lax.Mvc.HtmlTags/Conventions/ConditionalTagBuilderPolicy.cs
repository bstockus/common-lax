using System;

namespace Lax.Mvc.HtmlTags.Conventions {

    public class ConditionalTagBuilderPolicy : ITagBuilderPolicy {

        private readonly Func<ElementRequest, bool> _filter;
        private readonly ITagBuilder _builder;

        public ConditionalTagBuilderPolicy(Func<ElementRequest, bool> filter, Func<ElementRequest, HtmlTag> builder) {
            _filter = filter;
            _builder = new LambdaTagBuilder(builder);
        }

        public ConditionalTagBuilderPolicy(Func<ElementRequest, bool> filter, ITagBuilder builder) {
            _filter = filter;
            _builder = builder;
        }


        public bool Matches(ElementRequest subject) => _filter(subject);

        public ITagBuilder BuilderFor(ElementRequest subject) => _builder;

    }

}