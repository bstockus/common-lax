using System;

namespace Lax.Mvc.AdminLte.Bootstrap.Attributes {

    public class DisplayValueAttribute : Attribute {

        public DisplayValueAttribute(string name) => Name = name;

        public string Name { get; set; }

    }

}