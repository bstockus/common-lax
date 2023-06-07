namespace Lax.Business.Domain.Specifications {

    public class OrSpecification<T> : CompositeSpecification<T> {

        public ISpecification<T> LeftSpecification { get; }
        public ISpecification<T> RightSpecification { get; }

        public OrSpecification(ISpecification<T> left, ISpecification<T> right) {
            LeftSpecification = left;
            RightSpecification = right;
        }

        public override bool IsSatisfiedBy(T o) => LeftSpecification.IsSatisfiedBy(o)
                                                   || RightSpecification.IsSatisfiedBy(o);

    }

}