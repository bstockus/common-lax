using System;
using Autofac;
using Lax.Mvc.HtmlTags.Conventions;

namespace Lax.Mvc.HtmlTags {

    public static class ContainerBuilderExtensions {

        public static ContainerBuilder RegisterHtmlTags(
            this ContainerBuilder builder,
            HtmlConventionLibrary library) {
            builder.Register(_ => library).As<HtmlConventionLibrary>().SingleInstance();

            return builder;
        }

        public static ContainerBuilder RegisterHtmlTags(
            this ContainerBuilder builder,
            params HtmlConventionRegistry[] registries) {
            var library = new HtmlConventionLibrary();
            foreach (var registry in registries) {
                registry.Apply(library);
            }

            builder.RegisterHtmlTags(library);

            return builder;
        }

        public static ContainerBuilder RegisterHtmlTags(
            this ContainerBuilder builder,
            Action<HtmlConventionRegistry> config) {
            var registry = new HtmlConventionRegistry();

            config(registry);

            registry.Defaults();

            builder.RegisterHtmlTags(registry);

            return builder;
        }

    }

}