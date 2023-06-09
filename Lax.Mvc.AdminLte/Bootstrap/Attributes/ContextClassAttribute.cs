﻿using System;
using System.Reflection;
using Lax.Mvc.AdminLte.Bootstrap.Extensions;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Lax.Mvc.AdminLte.Bootstrap.Attributes {

    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class ContextClassAttribute : Attribute {

        public ContextClassAttribute(Type type) => Type = type;

        public ContextClassAttribute() { }

        public ContextClassAttribute(string key) => Key = key;

        /// <summary>
        ///     Custom key which will be used to store the context in context items. If set the context will not be accessible
        ///     without the key
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        ///     Type which will be used as key in context items
        /// </summary>
        public Type Type { get; set; }

        public static void SetContext(object target, TagHelperContext context) {
            if (target == null) {
                throw new ArgumentNullException(nameof(target));
            }

            if (context == null) {
                throw new ArgumentNullException(nameof(context));
            }

            var targetType = target.GetType();
            var attr = targetType.GetTypeInfo().GetCustomAttribute<ContextClassAttribute>();
            if (attr == null) {
                return;
            }

            if (string.IsNullOrEmpty(attr.Key)) {
                context.SetContextItem(attr.Type ?? targetType, target);
            } else {
                context.SetContextItem(attr.Key, target);
            }
        }

    }

}