using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Lax.Mvc.AdminLte.Bootstrap.Grid {

    [OutputElementHint("div")]
    public class Sm4TagHelper : SizedColTagHelper {

        protected override int Size => 4;
        protected override string Type => "sm";

    }

}