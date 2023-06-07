namespace Lax.Helpers.EnumerationsMapping {

    public static class EnumerationMapper {

        private static readonly EnumerationMapperCache Cache = new EnumerationMapperCache();

        public static TDestination MapEnumerationValue<TSource, TDestination>(TSource sourceValue) =>
            Cache.GetEnumerationMapping<TSource, TDestination>(sourceValue);

    }

}