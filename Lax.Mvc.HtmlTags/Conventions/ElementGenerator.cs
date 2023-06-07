using System;
using System.Linq.Expressions;
using Lax.Mvc.HtmlTags.Conventions.Elements;
using Lax.Mvc.HtmlTags.Reflection;

namespace Lax.Mvc.HtmlTags.Conventions {

    public class ElementGenerator<T> : IElementGenerator<T> where T : class {

        private readonly ITagGenerator _tags;
        private Lazy<T> _model;

        private ElementGenerator(ITagGenerator tags) => _tags = tags;

        public static ElementGenerator<T> For(HtmlConventionLibrary library, Func<Type, object> serviceLocator = null,
            T model = null) {
            serviceLocator = serviceLocator ?? (Activator.CreateInstance);

            var tags = new TagGenerator(library.TagLibrary, new ActiveProfile(), serviceLocator);

            return new ElementGenerator<T>(tags) {
                Model = model
            };
        }

        public HtmlTag LabelFor(Expression<Func<T, object>> expression, string profile = null, T model = null)
            => Build(expression, ElementConstants.Label, profile, model);

        public HtmlTag InputFor(Expression<Func<T, object>> expression, string profile = null, T model = null)
            => Build(expression, ElementConstants.Editor, profile, model);

        public HtmlTag DisplayFor(Expression<Func<T, object>> expression, string profile = null, T model = null)
            => Build(expression, ElementConstants.Display, profile, model);

        public HtmlTag TagFor(Expression<Func<T, object>> expression, string category, string profile = null,
            T model = null)
            => Build(expression, category, profile, model);

        public T Model {
            get => _model.Value;
            set => _model = new Lazy<T>(() => value);
        }

        public ElementRequest GetRequest(Expression<Func<T, object>> expression, T model = null) =>
            new ElementRequest(expression.ToAccessor()) {
                Model = model ?? Model
            };

        private HtmlTag Build(Expression<Func<T, object>> expression, string category, string profile = null,
            T model = null) {
            var request = GetRequest(expression, model);
            return _tags.Build(request, category, profile);
        }

        private HtmlTag Build(ElementRequest request, string category, string profile = null, T model = null) {
            request.Model = model ?? Model;
            return _tags.Build(request, category, profile: profile);
        }

        // Below methods are tested through the IFubuPage.Show/Edit method tests
        public HtmlTag LabelFor(ElementRequest request, string profile = null, T model = null) =>
            Build(request, ElementConstants.Label, profile, model);

        public HtmlTag InputFor(ElementRequest request, string profile = null, T model = null) =>
            Build(request, ElementConstants.Editor, profile, model);

        public HtmlTag DisplayFor(ElementRequest request, string profile = null, T model = null) =>
            Build(request, ElementConstants.Display, profile, model);

        public HtmlTag TagFor(ElementRequest request, string category, string profile = null, T model = null) =>
            Build(request, category, profile, model);

    }

}