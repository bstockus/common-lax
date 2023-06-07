using System;
using Newtonsoft.Json.Linq;

namespace Lax.Data.SharePoint.Rest.FieldConverters {

    public class DecimalFieldConverter : IFieldConverter {

        public object FromSpValue(JToken token) {
            if (token is not JValue value) {
                throw new Exception("Unable to convert JToken to Decimal");
            }

            return value.Type switch {
                JTokenType.Null => null,
                JTokenType.Float => Convert.ToDecimal(value.Value<double>()),
                JTokenType.Integer => Convert.ToDecimal(value.Value<int>()),
                _ => throw new Exception("Unable to convert JToken to Decimal")
            };
        }

        public JToken ToSpValue(object value) {
            var xValue = (decimal?)value;
            return xValue.HasValue ? new JValue(xValue.Value) : JValue.CreateNull();
        }

        public string FieldNameMapper(string originalName) => originalName;

    }

}