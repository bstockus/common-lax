﻿using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Lax.Mvc.AdminLte.Bootstrap {

    [HtmlTargetElement("bs-row")]
    public class BootstrapRowTagHelper : SimpleBootstrapDivTagHelper {

        protected override string ClassAttributes => "row";

    }

}