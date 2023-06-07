using Newtonsoft.Json;

namespace Lax.Serialization.Json {

    public interface IJsonNetSerializerSettingsConfiguration {

        void Configure(JsonSerializerSettings settings);

    }

}