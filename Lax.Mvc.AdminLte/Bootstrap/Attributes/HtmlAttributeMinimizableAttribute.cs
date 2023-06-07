using System;
using System.Linq;
using System.Reflection;
using Lax.Mvc.AdminLte.Bootstrap.Extensions;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Lax.Mvc.AdminLte.Bootstrap.Attributes {

    [AttributeUsage(AttributeTargets.Property)]
    public class HtmlAttributeMinimizableAttribute : Attribute {

        public static void FillMinimizableAttributes(object target, TagHelperContext context) {
            if (target == null) {
                throw new ArgumentNullException(nameof(target));
            }

            if (context == null) {
                throw new ArgumentNullException(nameof(context));
            }

            var minimizableProperties =
                target.GetType()
                    .GetProperties()
                    .Where(pI => pI.GetCustomAttribute<HtmlAttributeMinimizableAttribute>() != null);
            foreach (var property in minimizableProperties) {
                var attributeName = property.GetHtmlAttributeName();
                if (!context.AllAttributes.ContainsName(attributeName)) {
                    continue;
                }

                var attribute = context.AllAttributes[attributeName];
                if (attribute.Value is bool) {
                    property.SetValue(target, attribute.Value);
                } else if (attribute.ValueStyle == HtmlAttributeValueStyle.Minimized) {
                    property.SetValue(target, true);
                } else {
                    property.SetValue(target, !(attribute.Value ?? "").ToString().Equals("false"));
                }
            }
        }

    }

}