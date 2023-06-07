using System;

namespace Lax.Mvc.HtmlTags {

    internal static class StringExtensions {

        public static bool EqualsIgnoreCase(this string thisString, string otherString) =>
            thisString.Equals(otherString, StringComparison.OrdinalIgnoreCase);

        public static bool IsEmpty(this string stringValue) => string.IsNullOrEmpty(stringValue);

        public static bool IsNotEmpty(this string stringValue) => !string.IsNullOrEmpty(stringValue);

    }

}