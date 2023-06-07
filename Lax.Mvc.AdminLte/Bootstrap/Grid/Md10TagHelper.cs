using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Lax.Mvc.AdminLte.Bootstrap.Grid {

    [OutputElementHint("div")]
    public class Md10TagHelper : SizedColTagHelper {

        protected override int Size => 10;
        protected override string Type => "md";

    }

}