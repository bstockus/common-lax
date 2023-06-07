using Newtonsoft.Json.Linq;

namespace Lax.Data.SharePoint.Rest.FieldConverters {

    public interface IFieldConverter {

        object FromSpValue(JToken value);

        JToken ToSpValue(object value);

        string FieldNameMapper(string originalName);

    }

}