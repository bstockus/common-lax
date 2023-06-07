using System;

namespace Lax.Helpers.Common {

    public static class DateTimeExtensions {

        public static string ToIso8601String(this DateTime value) =>
            value.ToUniversalTime().ToString("o");

    }

}