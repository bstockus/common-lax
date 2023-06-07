using System;
using System.Linq;
using Lax.Data.SharePoint.Rest.FieldValues;
using Newtonsoft.Json.Linq;

namespace Lax.Data.SharePoint.Rest.FieldConverters {

    public class TaxonomyArrayFieldConverter : IFieldConverter {

        public object FromSpValue(JToken token) {
            if (token is not JObject obj) {
                throw new Exception("Unable to convert JToken to TaxonomyFieldValue array");
            }

            var taxonomyFieldConverter = new TaxonomyFieldConverter();

            if (obj["results"] is not JArray results) {
                return Array.Empty<TaxonomyFieldValue>();
            }

            return results
                .Select(_ => (taxonomyFieldConverter.FromSpValue(_) as TaxonomyFieldValue))
                .ToArray();

        }

        public JToken ToSpValue(object value) => throw new NotImplementedException();

        public string FieldNameMapper(string originalName) => originalName;

    }

}