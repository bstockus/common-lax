namespace Lax.Mvc.HtmlTags.Reflection.Expressions {

    public interface IArguments {

        T Get<T>(string propertyName);
        bool Has(string propertyName);

    }

}