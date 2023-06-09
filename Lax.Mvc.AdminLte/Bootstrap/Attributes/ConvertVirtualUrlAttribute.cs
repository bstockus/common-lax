using System;
using System.Linq;
using Lax.Mvc.AdminLte.Bootstrap.Extensions;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;

namespace Lax.Mvc.AdminLte.Bootstrap.Attributes {

    /// <summary>
    ///     Indicates that a property might contain a virtual Url that has to be converted into a absolute path
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class ConvertVirtualUrlAttribute : Attribute {

        public static void ConvertUrls(object target, IActionContextAccessor accessor) {
            if (target == null) {
                throw new ArgumentNullException(nameof(target));
            }

            var decoratedProperties = target.GetType().GetProperties()
                .Where(pI => pI.HasCustomAttribute<ConvertVirtualUrlAttribute>()).ToList();
            if (!decoratedProperties.Any()) {
                return;
            }

            if (accessor == null) {
                throw new ArgumentNullException(nameof(accessor));
            }

            var urlHelper = new UrlHelper(accessor.ActionContext);
            foreach (var property in decoratedProperties) {
                if (property.PropertyType != typeof(string)) {
                    throw new Exception("Decorated property must be a string");
                }

                property.SetValue(target, urlHelper.Content((string) property.GetValue(target)));
            }
        }

    }

}