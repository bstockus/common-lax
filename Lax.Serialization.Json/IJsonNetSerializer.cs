namespace Lax.Serialization.Json {

    public interface IJsonNetSerializer<TSettingsProvider> : IJsonSerializier
        where TSettingsProvider : IJsonNetSerializerSettingsProvider { }

}