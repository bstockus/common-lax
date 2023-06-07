using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Lax.Mvc.AdminLte.Bootstrap.Grid {

    [OutputElementHint("div")]
    public class Sm3TagHelper : SizedColTagHelper {

        protected override int Size => 3;
        protected override string Type => "sm";

    }

}