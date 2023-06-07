using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Lax.Mvc.AdminLte.Bootstrap.Grid {

    [OutputElementHint("div")]
    public class Md7TagHelper : SizedColTagHelper {

        protected override int Size => 7;
        protected override string Type => "md";

    }

}