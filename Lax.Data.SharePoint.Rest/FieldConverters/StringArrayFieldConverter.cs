using System;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace Lax.Data.SharePoint.Rest.FieldConverters {

    public class StringArrayFieldConverter : IFieldConverter {

        public object FromSpValue(JToken token) {
            if (token is not JObject obj) {

                if (token is not JValue value) {
                    throw new Exception("Unable to convert JToken to string array");
                }

                return Array.Empty<string>();

            }

            var stringFieldConverter = new StringFieldConverter();

            return obj["results"] is not JArray results
                ? Array.Empty<string>()
                : results.Select(_ => (stringFieldConverter.FromSpValue(_) as string)).ToArray();
        }

        public JToken ToSpValue(object value) {
            var xValue = (object[])value;
            var array = new JArray();

            var stringFieldConverter = new StringFieldConverter();

            foreach (var o in xValue) {
                array.Add(stringFieldConverter.ToSpValue(o));
            }

            return array;
        }

        public string FieldNameMapper(string originalName) => originalName;

    }

}