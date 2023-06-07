using System.Linq;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Lax.Mvc.AdminLte.Bootstrap.Extensions {

    public static class TagHelperAttributeListExtensions {

        public static bool RemoveAll(this TagHelperAttributeList attributeList, params string[] attributeNames) =>
            attributeNames.Aggregate(false, (current, name) => attributeList.RemoveAll(name) || current);

        public static void AddAriaAttribute(this TagHelperAttributeList attributeList, string attributeName,
            object value) =>
            attributeList.Add("aria-" + attributeName, value);

        public static void AddDataAttribute(this TagHelperAttributeList attributeList, string attributeName,
            object value) =>
            attributeList.Add("data-" + attributeName, value);

    }

}