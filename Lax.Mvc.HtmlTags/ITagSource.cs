using System.Collections.Generic;

namespace Lax.Mvc.HtmlTags {

    public interface ITagSource {

        IEnumerable<HtmlTag> AllTags();

    }

}