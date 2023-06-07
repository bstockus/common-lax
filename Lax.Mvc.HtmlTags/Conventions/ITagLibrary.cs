namespace Lax.Mvc.HtmlTags.Conventions {

    public interface ITagLibrary {

        ITagPlan PlanFor(ElementRequest subject, string profile = null, string category = null);

    }

}