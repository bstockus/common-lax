using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Lax.Mvc.AdminLte.Bootstrap.Grid {

    [OutputElementHint("div")]
    public class Xs1TagHelper : SizedColTagHelper {

        protected override int Size => 1;
        protected override string Type => "xs";

    }

}