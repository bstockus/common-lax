using System;

namespace Lax.Serialization.Json {

    public interface IJsonSerializier {

        string Serialize(object obj);
        T Deserialize<T>(string value);
        object Deserialize(string value, Type type);
        object Deserialize(string value);

    }

}