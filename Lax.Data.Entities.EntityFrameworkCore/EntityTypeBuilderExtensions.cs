using System;
using FluentValidation;
using FluentValidation.Validators;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lax.Data.Entities.EntityFrameworkCore {

    public static class EntityTypeBuilderExtensions {

        public static EntityTypeBuilder<TEntity> FromValidator<TEntity>(
            this EntityTypeBuilder<TEntity> entityTypeBuilder,
            Action<InlineValidator<TEntity>> validatorBuilder) where TEntity : class {
            var inlineValidator = new InlineValidator<TEntity>();

            validatorBuilder(inlineValidator);

            var validatorDescriptor = inlineValidator.CreateDescriptor();

            foreach (var member in validatorDescriptor.GetMembersWithValidators()) {
                var memberName = member.Key;

                var builder = entityTypeBuilder.Property(memberName);

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


            return entityTypeBuilder;
        }

    }

}