using System;
using Lax.Data.SharePoint.Rest.FieldValues;
using Newtonsoft.Json.Linq;

namespace Lax.Data.SharePoint.Rest.FieldConverters {

    public class TaxonomyFieldConverter : IFieldConverter {

        public object FromSpValue(JToken token) {
            if (token is not JObject obj) {
                throw new Exception("Unable to convert JToken to TaxonomyFieldValue");
            }

            var label = obj["Label"]?.Value<string>();
            var termGuid = obj["TermGuid"]?.Value<string>();
            var wssId = obj["WssId"]?.Value<int>() ?? 0;
            return new TaxonomyFieldValue(label, termGuid, wssId);

        }

        public JToken ToSpValue(object value) => throw new NotImplementedException();

        public string FieldNameMapper(string originalName) => originalName;

    }

}