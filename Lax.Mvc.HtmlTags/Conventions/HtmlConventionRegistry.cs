using System;

namespace Lax.Mvc.HtmlTags.Conventions {

    public class HtmlConventionRegistry : ProfileExpression {

        public HtmlConventionRegistry() : this(new HtmlConventionLibrary()) { }

        private HtmlConventionRegistry(HtmlConventionLibrary library) : base(library, TagConstants.Default) { }

        public void Profile(string profileName, Action<ProfileExpression> configure) {
            var expression = new ProfileExpression(Library, profileName);
            configure(expression);
        }

    }

}