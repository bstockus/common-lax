using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Lax.Mvc.AdminLte.Bootstrap.Grid {

    [OutputElementHint("div")]
    public class Sm12TagHelper : SizedColTagHelper {

        protected override int Size => 12;
        protected override string Type => "sm";

    }

}