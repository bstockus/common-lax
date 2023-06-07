using System;
using Newtonsoft.Json.Linq;

namespace Lax.Data.SharePoint.Rest.FieldConverters {

    public class DateTimeFieldConverter : IFieldConverter {

        public object FromSpValue(JToken token) {
            if (token is not JValue value) {
                throw new Exception("Unable to convert JToken to DateTime");
            }

            return value.Type switch {
                JTokenType.Null => null,
                JTokenType.Date => value.Value<DateTime>(),
                _ => throw new Exception("Unable to convert JToken to DateTime")
            };
        }

        public JToken ToSpValue(object value) {
            var xValue = (DateTime?)value;
            return xValue.HasValue ? new JValue(xValue.Value) : JValue.CreateNull();
        }

        public string FieldNameMapper(string originalName) => originalName;

    }

}