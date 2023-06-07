namespace Lax.Data.SharePoint.Rest.FieldValues {

    public class TaxonomyFieldValue {

        public string Label { get; }
        public string TermGuid { get; }
        public int WssId { get; }

        public TaxonomyFieldValue(string label, string termGuid, int wssId) {
            Label = label;
            TermGuid = termGuid;
            WssId = wssId;
        }

    }

}