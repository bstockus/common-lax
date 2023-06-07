using Lax.Mvc.HtmlTags.Conventions.Elements;
using Lax.Mvc.HtmlTags.Conventions.Elements.Builders;

namespace Lax.Mvc.HtmlTags.Conventions {

    public static class HtmlConventionRegistryExtensions {

        public static void Defaults(this HtmlConventionRegistry registry) {
            registry.Editors.BuilderPolicy<CheckboxBuilder>();

            registry.Editors.Always.BuildBy<TextboxBuilder>();

            registry.Editors.Modifier<AddNameModifier>();

            registry.Editors.Modifier<AddIdModifier>();

            registry.Editors.NamingConvention(new DotNotationElementNamingConvention());

            registry.Displays.Always.BuildBy<SpanDisplayBuilder>();

            registry.Labels.Always.BuildBy<DefaultLabelBuilder>();
        }

    }

}