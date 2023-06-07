using System;
using Lax.Mvc.HtmlTags.Conventions.Elements;

namespace Lax.Mvc.HtmlTags.Conventions {

    public class HtmlConventionLibrary {

        private readonly Cache<string, ServiceBuilder> _services =
            new Cache<string, ServiceBuilder>(key => new ServiceBuilder());

        private readonly ServiceBuilder _defaultBuilder;

        public HtmlConventionLibrary() {
            TagLibrary = new TagLibrary();

            _defaultBuilder = _services[TagConstants.Default];
        }

        public TagLibrary TagLibrary { get; }

        public T Get<T>(string profile = null) {
            profile ??= TagConstants.Default;
            var builder = _services[profile];
            if (builder.Has(typeof(T))) {
                return builder.Build<T>();
            }

            if (profile != TagConstants.Default && _defaultBuilder.Has(typeof(T))) {
                return _defaultBuilder.Build<T>();
            }

            throw new ArgumentOutOfRangeException("T",
                "No service implementation is registered for type " + typeof(T).FullName);
        }

        public void RegisterService<T, TImplementation>(string profile = null) where TImplementation : T, new()
            => RegisterService<T>(() => new TImplementation(), profile);

        public void RegisterService<T>(Func<T> builder, string profile = null) {
            profile ??= TagConstants.Default;
            _services[profile].Add(builder);
        }

        public void Import(HtmlConventionLibrary library) {
            TagLibrary.Import(library.TagLibrary);
            library._services.Each((key, builder) => builder.FillInto(_services[key]));
        }

        public IElementGenerator<T> GeneratorFor<T>() where T : class => ElementGenerator<T>.For(this);

    }

}