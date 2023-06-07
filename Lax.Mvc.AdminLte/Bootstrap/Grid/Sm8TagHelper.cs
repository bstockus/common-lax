using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Lax.Mvc.AdminLte.Bootstrap.Grid {

    [OutputElementHint("div")]
    public class Sm8TagHelper : SizedColTagHelper {

        protected override int Size => 8;
        protected override string Type => "sm";

    }

}