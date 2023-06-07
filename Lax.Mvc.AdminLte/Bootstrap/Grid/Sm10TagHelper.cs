using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Lax.Mvc.AdminLte.Bootstrap.Grid {

    [OutputElementHint("div")]
    public class Sm10TagHelper : SizedColTagHelper {

        protected override int Size => 10;
        protected override string Type => "sm";

    }

}