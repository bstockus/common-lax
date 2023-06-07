using System;
using Lax.Data.SharePoint.Rest.FieldValues;
using Newtonsoft.Json.Linq;

namespace Lax.Data.SharePoint.Rest.FieldConverters {

    public class LookupFieldConverter : IFieldConverter {

        public object FromSpValue(JToken token) {
            if (token is not JValue value) {
                throw new Exception("Unable to convert JToken to Lookup Field");
            }

            return value.Type switch {
                JTokenType.Null => null,
                JTokenType.Integer => new LookupFieldValue(value.Value<int>()),
                _ => throw new Exception("Unable to convert JToken to Lookup Field")
            };
        }

        public JToken ToSpValue(object value) =>
            value is not LookupFieldValue xValue ? JValue.CreateNull() : new JValue(xValue.Id);

        public string FieldNameMapper(string originalName) => $"{originalName}Id";

    }

}