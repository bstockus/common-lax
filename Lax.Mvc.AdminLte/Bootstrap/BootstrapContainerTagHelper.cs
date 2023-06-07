﻿using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Lax.Mvc.AdminLte.Bootstrap {

    [HtmlTargetElement("bs-container")]
    public class BootstrapContainerTagHelper : SimpleBootstrapDivTagHelper {

        protected override string ClassAttributes => "container";

    }

}