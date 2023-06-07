using System;
using Lax.Mvc.HtmlTags.Reflection;

namespace Lax.Mvc.HtmlTags.Conventions.Elements.Builders {

    public class AddClassForAttributeModifier<T> : IElementModifier where T : Attribute {

        private readonly string _className;

        public AddClassForAttributeModifier(string className) => _className = className;

        public bool Matches(ElementRequest token) => token.Accessor.HasAttribute<T>();

        public void Modify(ElementRequest request) => request.CurrentTag.AddClass(_className);

    }

}