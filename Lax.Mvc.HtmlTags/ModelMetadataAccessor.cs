﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using Lax.Mvc.HtmlTags.Reflection;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace Lax.Mvc.HtmlTags {

    public class ModelMetadataAccessor : IAccessor {

        public ModelMetadata ModelMetadata { get; }
        public ModelExpression ModelExpression { get; }

        public ModelMetadataAccessor(ModelExpression modelExpression) {
            ModelMetadata = modelExpression.Metadata;
            ModelExpression = modelExpression;
        }

        public Type PropertyType => ModelMetadata.ModelType;
        public PropertyInfo InnerProperty => ModelMetadata.ContainerType.GetProperty(ModelMetadata.PropertyName);
        public Type DeclaringType => ModelMetadata.ContainerType;
        public string Name => ModelMetadata.PropertyName;
        public Type OwnerType => ModelMetadata.ContainerType;

        public void SetValue(object target, object propertyValue) => throw new NotImplementedException();

        public object GetValue(object target) => ModelExpression.Model;

        public IAccessor GetChildAccessor<T>(Expression<Func<T, object>> expression) =>
            throw new NotImplementedException();

        public string[] PropertyNames => ModelExpression.Name.Split('.');

        public Expression<Func<T, object>> ToExpression<T>() => throw new NotImplementedException();

        public IAccessor Prepend(PropertyInfo property) => throw new NotImplementedException();

        public IEnumerable<IValueGetter> Getters() => throw new NotImplementedException();

    }

}