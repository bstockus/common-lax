﻿namespace Lax.Mvc.HtmlTags.Conventions.Elements {

    public class ElementTagOverride<T> : IElementTagOverride where T : IElementBuilder, new() {

        public ElementTagOverride(string category, string profile) {
            Category = category ?? TagConstants.Default;
            Profile = profile ?? TagConstants.Default;
        }

        public string Category { get; }
        public string Profile { get; }
        public IElementBuilder Builder() => new T();

    }

}