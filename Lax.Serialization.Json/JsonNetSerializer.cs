using System;
using Newtonsoft.Json;

namespace Lax.Serialization.Json {

    public class JsonNetSerializer<TSettingsProvider> : IJsonNetSerializer<TSettingsProvider>
        where TSettingsProvider : IJsonNetSerializerSettingsProvider {

        private readonly TSettingsProvider _settingsProvider;

        public JsonNetSerializer(
            TSettingsProvider settingsProvider) =>
            _settingsProvider = settingsProvider;

        public string Serialize(object obj) => JsonConvert.SerializeObject(obj, _settingsProvider.Settings);

        public T Deserialize<T>(string value) =>
            JsonConvert.DeserializeObject<T>(value, _settingsProvider.Settings);

        public object Deserialize(string value, Type type) =>
            JsonConvert.DeserializeObject(value, type, _settingsProvider.Settings);

        public object Deserialize(string value) => JsonConvert.DeserializeObject(value, _settingsProvider.Settings);

    }

}