using System;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace Lax.Data.SharePoint.Rest.FieldConverters {

    public class IntegerArrayFieldConverter : IFieldConverter {

        public object FromSpValue(JToken token) {
            if (token is not JObject { HasValues: true } obj || !obj.ContainsKey("results")) {
                return Array.Empty<int>();
            }

            var integerFieldConverter = new NullableIntegerFieldConverter();
            return obj["results"] is not JArray results
                ? Array.Empty<int>()
                : results.Select(_ => (integerFieldConverter.FromSpValue(_) as int?) ?? 0).ToArray();
        }

        public JToken ToSpValue(object value) {
            var xValue = (object[])value;
            var array = new JArray();

            var nullableIntegerFieldConverter = new NullableIntegerFieldConverter();

            foreach (var o in xValue) {
                array.Add(nullableIntegerFieldConverter.ToSpValue(o));
            }

            return array;

        }

        public string FieldNameMapper(string originalName) => originalName;

    }

}