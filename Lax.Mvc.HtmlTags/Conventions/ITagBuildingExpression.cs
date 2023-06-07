using System;

namespace Lax.Mvc.HtmlTags.Conventions {

    public interface ITagBuildingExpression {

        CategoryExpression Always { get; }
        CategoryExpression If(Func<ElementRequest, bool> matches);

        void Add(Func<ElementRequest, bool> filter, ITagBuilder builder);
        void Add(ITagBuilderPolicy policy);
        void Add(ITagModifier modifier);

    }

}