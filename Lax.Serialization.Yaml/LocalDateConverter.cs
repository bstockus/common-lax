using System;
using System.Globalization;
using NodaTime;
using YamlDotNet.Core;
using YamlDotNet.Core.Events;
using YamlDotNet.Serialization;

namespace Lax.Serialization.Yaml {

    public class LocalDateConverter : IYamlTypeConverter {

        public bool Accepts(Type type) => type == typeof(LocalDate);

        public object ReadYaml(IParser parser, Type type) => throw new NotImplementedException();

        public void WriteYaml(IEmitter emitter, object value, Type type) =>
            emitter.Emit(new Scalar(null, null, ((LocalDate) value).ToString("d", DateTimeFormatInfo.InvariantInfo),
                ScalarStyle.Any, true, false));

    }

}