using Lax.Mvc.AdminLte.Bootstrap.Attributes;

namespace Lax.Mvc.AdminLte.Bootstrap.Navigation {

    public enum NavbarPosition {

        [DisplayValue("navbar-fixed-top")] FixedTop,

        [DisplayValue("navbar-fixed-bottom")] FixedBottom,

        [DisplayValue("navbar-static-top")] StaticTop,

        [DisplayValue("navbar-static-bottom")] StaticBottom

    }

}