using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Lax.Mvc.AdminLte.Bootstrap.Grid {

    [OutputElementHint("div")]
    public class Xs5TagHelper : SizedColTagHelper {

        protected override int Size => 5;
        protected override string Type => "xs";

    }

}