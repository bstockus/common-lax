using System;
using Newtonsoft.Json.Linq;

namespace Lax.Data.SharePoint.Rest.FieldConverters {

    public class BooleanFieldConverter : IFieldConverter {

        public object FromSpValue(JToken token) {
            if (token is not JValue value) {
                throw new Exception("Unable to parse token into boolean");
            }

            return value.Type switch {
                JTokenType.Null => null,
                JTokenType.Boolean => value.Value<bool>(),
                _ => throw new Exception("Unable to parse token into boolean")
            };
        }

        public JToken ToSpValue(object value) {
            var boolValue = (bool?) value;
            return boolValue.HasValue ? new JValue(boolValue.Value) : JValue.CreateNull();
        }

        public string FieldNameMapper(string originalName) => originalName;

    }

}