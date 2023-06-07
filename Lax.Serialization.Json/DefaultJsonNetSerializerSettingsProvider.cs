using System.Collections.Generic;
using Newtonsoft.Json;

namespace Lax.Serialization.Json {

    public class DefaultJsonNetSerializerSettingsProvider : IJsonNetSerializerSettingsProvider {

        public DefaultJsonNetSerializerSettingsProvider(
            IEnumerable<IJsonNetSerializerSettingsConfiguration> settingsConfigurations) {
            var jsonSerializerSettings = new JsonSerializerSettings();

            foreach (var settingsConfiguration in settingsConfigurations) {
                settingsConfiguration.Configure(jsonSerializerSettings);
            }

            Settings = jsonSerializerSettings;
        }

        public JsonSerializerSettings Settings { get; }

    }

}