using System;
using Lax.Mvc.HtmlTags.Reflection;

namespace Lax.Mvc.HtmlTags.Conventions.Formatting {

    public class DisplayFormatter : IDisplayFormatter {

        private readonly Func<Type, object> _locator;
        private readonly Stringifier _stringifier;

        public DisplayFormatter(Func<Type, object> locator) {
            _locator = locator;
            _stringifier = new Stringifier();
        }

        public string GetDisplay(GetStringRequest request) {
            request.Locator = _locator;
            return _stringifier.GetString(request);
        }

        public string GetDisplay(IAccessor accessor, object target) {
            var request = new GetStringRequest(accessor, target, _locator, null, null);
            return _stringifier.GetString(request);
        }

        public string GetDisplayForValue(IAccessor accessor, object rawValue) {
            var request = new GetStringRequest(accessor, rawValue, _locator, null, null);
            return _stringifier.GetString(request);
        }

    }

}