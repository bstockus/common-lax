﻿using System;
using System.Linq;
using Lax.Helpers.Common;
using Lax.Mvc.HtmlTags.Conventions;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.DependencyInjection;

namespace Lax.Mvc.HtmlTags {

    public abstract class HtmlTagTagHelper : TagHelper {

        public const string ForAttributeName = "for";

        [HtmlAttributeName(ForAttributeName)]
        public ModelExpression For { get; set; }

        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext ViewContext { get; set; }

        protected abstract string Category { get; }

        public override void Process(TagHelperContext context, TagHelperOutput output) {
            if (For == null) {
                throw new InvalidOperationException(
                    "Missing or invalid 'for' attribute value. Specify a valid model expression for the 'for' attribute value.");
            }

            var request = new ElementRequest(new ModelMetadataAccessor(For)) {
                Model = For.Model
            };

            foreach (var contextAttribute in context.AllAttributes) {
                request.AddMetaData(contextAttribute.Name, contextAttribute.Value.As<object>());
            }

            var library = ViewContext.HttpContext.RequestServices.GetService<HtmlConventionLibrary>();

            var tagGenerator = new TagGenerator(library.TagLibrary, new ActiveProfile(),
                t => ViewContext.HttpContext.RequestServices.GetService(t));

            var tag = tagGenerator.Build(request, Category);

            foreach (var attribute in output.Attributes.Where(attr => !attr.Name.StartsWith("asp-"))) {
                tag.Attr(attribute.Name, attribute.Value);
            }

            output.TagName = null;
            output.PreElement.AppendHtml(tag);
        }

    }

}