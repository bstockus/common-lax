using System.Collections.Generic;
using System.Linq;
using YamlDotNet.Serialization;

namespace Lax.Serialization.Yaml {

    public class StandardYamlSerializer : IYamlSerializer {

        private readonly IEnumerable<IYamlTypeConverter> _typeConverters;

        private readonly Serializer _serializer;

        public StandardYamlSerializer(
            IEnumerable<IYamlTypeConverter> typeConverters) {
            var yamlTypeConverters = typeConverters.ToList();
            _typeConverters = yamlTypeConverters;

            var serializationBuilder = new SerializerBuilder();

            foreach (var typeConverter in yamlTypeConverters) {
                serializationBuilder.WithTypeConverter(typeConverter);
            }

            _serializer = (Serializer) serializationBuilder.Build();
        }

        public string Serialize(object obj) => _serializer.Serialize(obj);

    }

}