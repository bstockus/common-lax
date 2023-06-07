using Newtonsoft.Json;
using NodaTime.Serialization.JsonNet;
using NodaTime.TimeZones;

namespace Lax.Serialization.Json {

    public class NodaTimeJsonNetSerializationSettingsConfiguration : IJsonNetSerializerSettingsConfiguration {

        public void Configure(JsonSerializerSettings settings) =>
            settings.ConfigureForNodaTime(new DateTimeZoneCache(TzdbDateTimeZoneSource.Default));

    }

}