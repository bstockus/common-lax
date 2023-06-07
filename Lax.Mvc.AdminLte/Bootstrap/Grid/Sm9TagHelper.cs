using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Lax.Mvc.AdminLte.Bootstrap.Grid {

    [OutputElementHint("div")]
    public class Sm9TagHelper : SizedColTagHelper {

        protected override int Size => 9;
        protected override string Type => "sm";

    }

}