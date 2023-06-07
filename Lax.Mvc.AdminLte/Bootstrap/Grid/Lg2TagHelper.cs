using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Lax.Mvc.AdminLte.Bootstrap.Grid {

    [OutputElementHint("div")]
    public class Lg2TagHelper : SizedColTagHelper {

        protected override int Size => 2;
        protected override string Type => "lg";

    }

}