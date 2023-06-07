using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Lax.Mvc.AdminLte.Bootstrap.Grid {

    [OutputElementHint("div")]
    public class Lg11TagHelper : SizedColTagHelper {

        protected override int Size => 11;
        protected override string Type => "lg";

    }

}