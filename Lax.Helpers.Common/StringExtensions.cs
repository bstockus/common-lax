using System;
using System.Linq;

namespace Lax.Helpers.Common {

    public static class StringExtensions {

        public static string GetTrailingNumbers(this string input) =>
            new string(input.Where(char.IsDigit).ToArray());

        public static bool EqualsWithNull(this string left, string right) =>
            (left == null && right == null) || (left != null && right != null && left.Equals(right));

        public static string TakeFirst(this string value, int numberOfCharactersToTake)
            => value.Take(numberOfCharactersToTake).Aggregate("", (a, b) => a + b);

        public static string FixedLength(this string value, int length) =>
            value.Substring(0, Math.Min(length, value.Length)).PadRight(length, ' ');

    }

}