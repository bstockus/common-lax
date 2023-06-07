using System;
using Newtonsoft.Json.Linq;

namespace Lax.Data.SharePoint.Rest.FieldConverters {

    public class DoubleFieldConverter : IFieldConverter {

        public object FromSpValue(JToken token) {
            if (token is not JValue value) {
                throw new Exception("Unable to convert JToken to Double");
            }

            return value.Type switch {
                JTokenType.Null => null,
                JTokenType.Float => Convert.ToDouble(value.Value<double>()),
                JTokenType.Integer => Convert.ToDouble(value.Value<int>()),
                _ => throw new Exception("Unable to convert JToken to Double")
            };
        }

        public JToken ToSpValue(object value) {
            var xValue = (double?)value;
            return xValue.HasValue ? new JValue(xValue.Value) : JValue.CreateNull();
        }

        public string FieldNameMapper(string originalName) => originalName;

    }

}