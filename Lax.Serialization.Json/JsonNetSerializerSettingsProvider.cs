using Newtonsoft.Json;

namespace Lax.Serialization.Json {

    public abstract class JsonNetSerializerSettingsProvider : IJsonNetSerializerSettingsProvider {

        public JsonSerializerSettings Settings {
            get {
                var settings = new JsonSerializerSettings();
                return BuildSettings(settings);
            }
        }

        protected abstract JsonSerializerSettings BuildSettings(JsonSerializerSettings settings);

    }

}