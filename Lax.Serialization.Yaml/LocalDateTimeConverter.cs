﻿using System;
using System.Globalization;
using NodaTime;
using YamlDotNet.Core;
using YamlDotNet.Core.Events;
using YamlDotNet.Serialization;

namespace Lax.Serialization.Yaml {

    public class LocalDateTimeConverter : IYamlTypeConverter {

        public bool Accepts(Type type) => type == typeof(LocalDateTime);

        public object ReadYaml(IParser parser, Type type) => throw new NotImplementedException();

        public void WriteYaml(IEmitter emitter, object value, Type type) =>
            emitter.Emit(new Scalar(null, null,
                ((LocalDateTime) value).ToString("g", DateTimeFormatInfo.InvariantInfo),
                ScalarStyle.Any, true, false));

    }

}