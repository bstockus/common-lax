using System;
using System.Linq;
using Lax.Data.SharePoint.Rest.FieldValues;
using Newtonsoft.Json.Linq;

namespace Lax.Data.SharePoint.Rest.FieldConverters {

    public class PersonArrayFieldConverter : IFieldConverter {

        public object FromSpValue(JToken token) {
            if (token is not JObject obj) {
                throw new Exception("Unable to convert JToken to Person Field array");
            }

            var personFieldConverter = new PersonFieldConverter();

            return obj["results"] is not JArray results
                ? Array.Empty<PersonFieldValue>()
                : results.Select(_ => personFieldConverter.FromSpValue(_)).ToArray();
        }

        public JToken ToSpValue(object value) {
            var xValue = (object[])value;
            var array = new JArray();

            var personFieldConverter = new PersonFieldConverter();

            foreach (var o in xValue) {
                array.Add(personFieldConverter.ToSpValue(o));
            }

            return array;
        }

        public string FieldNameMapper(string originalName) => $"{originalName}Id";

    }

}