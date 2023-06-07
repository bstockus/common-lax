using Lax.Mvc.HtmlTags.Reflection;

namespace Lax.Mvc.HtmlTags.Conventions.Formatting {

    public interface IDisplayFormatter {

        string GetDisplay(GetStringRequest request);
        string GetDisplay(IAccessor accessor, object target);
        string GetDisplayForValue(IAccessor accessor, object rawValue);

    }

}