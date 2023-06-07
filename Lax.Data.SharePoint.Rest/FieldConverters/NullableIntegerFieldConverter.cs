using System;
using Newtonsoft.Json.Linq;

namespace Lax.Data.SharePoint.Rest.FieldConverters {

    public class NullableIntegerFieldConverter : IFieldConverter {

        public object FromSpValue(JToken token) {
            if (token is not JValue value) {
                throw new Exception("Unable to convert JToken to Nullable Integer");
            }

            return value.Type switch {
                JTokenType.Null => null,
                JTokenType.Float => Convert.ToInt32(value.Value<double>()),
                JTokenType.Integer => Convert.ToInt32(value.Value<int>()),
                _ => throw new Exception("Unable to convert JToken to Nullable Integer")
            };
        }

        public JToken ToSpValue(object value) {
            var xValue = (int?)value;
            return xValue.HasValue ? new JValue(xValue.Value) : JValue.CreateNull();
        }

        public string FieldNameMapper(string originalName) => originalName;

    }

}