using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Lax.Helpers.Common;

namespace Lax.Mvc.HtmlTags.Conventions.Formatting {

    public class Stringifier {

        private readonly List<PropertyOverrideStrategy> _overrides = new List<PropertyOverrideStrategy>();
        private readonly List<StringifierStrategy> _strategies = new List<StringifierStrategy>();

        private Func<GetStringRequest, string> FindConverter(GetStringRequest request) {
            if (request.PropertyType.IsNullable()) {
                return request.RawValue == null
                    ? r => string.Empty
                    : FindConverter(request.GetRequestForNullableType());
            }

            if (request.PropertyType.IsArray) {
                if (request.RawValue == null) {
                    return r => string.Empty;
                }

                return r => r.RawValue == null
                    ? string.Empty
                    : r.RawValue.As<Array>().OfType<object>().Select(GetString).Join(", ");
            }

            var strategy = _strategies.FirstOrDefault(x => x.Matches(request));
            return strategy == null ? ToString : strategy.StringFunction;
        }

        private static string ToString(GetStringRequest value) => value.RawValue?.ToString() ?? string.Empty;

        public string GetString(GetStringRequest request) {
            if (request?.RawValue == null || request.RawValue as string == string.Empty) {
                return string.Empty;
            }

            var propertyOverride = _overrides.FirstOrDefault(o => o.Matches(request.Property));

            return propertyOverride != null
                ? propertyOverride.StringFunction(request)
                : FindConverter(request)(request);
        }


        public string GetString(object rawValue) {
            if (rawValue == null || (rawValue as string) == string.Empty) {
                return string.Empty;
            }

            return GetString(new GetStringRequest(null, rawValue, null, null, null));
        }


        public void AddStrategy(StringifierStrategy strategy) => _strategies.Add(strategy);

        public class PropertyOverrideStrategy {

            public Func<PropertyInfo, bool> Matches;
            public Func<GetStringRequest, string> StringFunction;

        }

    }

}