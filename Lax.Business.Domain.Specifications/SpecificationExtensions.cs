namespace Lax.Business.Domain.Specifications {

    public static class SpecificationExtensions {

        public static ISpecification<T> And<T>(this ISpecification<T> specification,
            ISpecification<T> otherSpecification) => new AndSpecification<T>(specification, otherSpecification);

        public static ISpecification<T> Or<T>(this ISpecification<T> specification,
            ISpecification<T> otherSpecification) => new OrSpecification<T>(specification, otherSpecification);

        public static ISpecification<T> Not<T>(this ISpecification<T> specification) =>
            new NotSpecification<T>(specification);

    }

}