using FluentValidation;

namespace Lax.Business.Domain.Values {

    public static class ValueValidationExtensions {

        public static void Validate<TAbstractValidator, TValueType>(this TValueType value)
            where TAbstractValidator : AbstractValidator<TValueType>, new()
            where TValueType : Value<TValueType> {
            var validator = new TAbstractValidator();

            validator.ValidateAndThrow(value);
        }

    }

}