using Newtonsoft.Json;

namespace Lax.Serialization.Json {

    public interface IJsonNetSerializerSettingsProvider {

        JsonSerializerSettings Settings { get; }

    }

}