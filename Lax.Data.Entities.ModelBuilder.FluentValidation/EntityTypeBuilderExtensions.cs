using FluentValidation;
using FluentValidation.Validators;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lax.Data.Entities.ModelBuilder.FluentValidation {

    public static class EntityTypeBuilderExtensions {

        public static void UseValidatorConfiguration<TEntity>(
            this EntityTypeBuilder<TEntity> entity,
            IValidator<TEntity> validator) where TEntity : class {
            var validatorDescriptor = validator.CreateDescriptor();

            foreach (var member in validatorDescriptor.GetMembersWithValidators()) {
                var memberName = member.Key;


                var builder = entity.Property(memberName);

                foreach (var propertyValidator in member) {
                    switch (propertyValidator.Validator) {
                        case ILengthValidator lengthValidator:
                            builder.HasMaxLength(lengthValidator.Max);
                            break;
                        case INotNullValidator _:
                        case INotEmptyValidator _:
                            builder.IsRequired();
                            break;
                    }
                }
            }
        }

        //public static void UseValidatorConfiguration<TEntity>(
        //    this ReferenceOwnershipBuilder entity,
        //    IValidator<TEntity> validator) where TEntity : class {

        //    var validatorDescriptor = validator.CreateDescriptor();

        //    foreach (var member in validatorDescriptor.GetMembersWithValidators()) {

        //        var memberName = member.Key;


        //        var builder = entity.Property(memberName);

        //        foreach (var propertyValidator in member) {


        //            switch (propertyValidator) {
        //                case LengthValidator lengthValidator:
        //                    builder.HasMaxLength(lengthValidator.Max);
        //                    break;
        //                case NotNullValidator _:
        //                case NotEmptyValidator _:
        //                    builder.IsRequired();
        //                    break;
        //            }

        //        }

        //    }

        //}

    }

}