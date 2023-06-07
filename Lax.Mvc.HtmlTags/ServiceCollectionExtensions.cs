﻿using System;
using Lax.Mvc.HtmlTags.Conventions;
using Microsoft.Extensions.DependencyInjection;

namespace Lax.Mvc.HtmlTags {

    public static class ServiceCollectionExtensions {

        public static void AddHtmlTags(this IServiceCollection services, HtmlConventionLibrary library) =>
            services.AddSingleton(library);

        public static void AddHtmlTags(this IServiceCollection services, params HtmlConventionRegistry[] registries) {
            var library = new HtmlConventionLibrary();
            foreach (var registry in registries) {
                registry.Apply(library);
            }

            services.AddHtmlTags(library);
        }

        public static void AddHtmlTags(this IServiceCollection services, Action<HtmlConventionRegistry> config) {
            var registry = new HtmlConventionRegistry();

            config(registry);

            registry.Defaults();

            services.AddHtmlTags(registry);
        }

    }

}