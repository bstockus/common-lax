using System;
using Newtonsoft.Json.Linq;

namespace Lax.Data.SharePoint.Rest.FieldConverters {

    public class StringFieldConverter : IFieldConverter {

        public object FromSpValue(JToken token) {
            if (token is not JValue value) {
                throw new Exception("Unable to convert JToken to string");
            }

            return value.Type switch {
                JTokenType.Null => null,
                JTokenType.String => value.Value<string>(),
                _ => throw new Exception("Unable to convert JToken to string")
            };
        }

        public JToken ToSpValue(object value) {
            var xValue = (string)value;
            return xValue != null ? new JValue(xValue) : JValue.CreateNull();
        }

        public string FieldNameMapper(string originalName) => originalName;

    }

}