using System;
using System.Globalization;
using System.Reflection;

namespace Lax.Helpers.AssemblyVersions {

    public static class AssemblyVersionExtensions {

        public static DateTime GetBuildDate(this Assembly assembly) {
            const string buildVersionMetadataPrefix = "+build";

            var attribute = assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>();
            if (attribute?.InformationalVersion == null) {
                return default;
            }

            var value = attribute.InformationalVersion;
            var index = value.IndexOf(buildVersionMetadataPrefix, StringComparison.Ordinal);
            if (index <= 0) {
                return default;
            }

            value = value.Substring(index + buildVersionMetadataPrefix.Length);
            return DateTime.TryParseExact(value, "yyyyMMddHHmmss", CultureInfo.InvariantCulture, DateTimeStyles.None,
                out var result)
                ? result
                : default;
        }

    }

}
