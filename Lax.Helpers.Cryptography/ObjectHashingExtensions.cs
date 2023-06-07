using System;
using System.IO;
using System.Runtime.Serialization;
using System.Security.Cryptography;

namespace Lax.Helpers.Cryptography {

    /// <summary>
    ///     Extension methods applied to the <see cref="object"/> type.
    /// </summary>
    public static class ObjectHasingExtensions {
        /// <summary>
        ///     Gets a hash of the current instance.
        /// </summary>
        /// <typeparam name="T">
        ///     The type of the Cryptographic Service Provider to use.
        /// </typeparam>
        /// <param name="instance">
        ///     The instance being extended.
        /// </param>
        /// <returns>
        ///     A base 64 encoded string representation of the hash.
        /// </returns>
        public static string GetHash<T>(this object instance) where T : HashAlgorithm, new() {
            var cryptoServiceProvider = new T();
            return ComputeHash(instance, cryptoServiceProvider);
        }

        /// <summary>
        ///     Gets a key based hash of the current instance.
        /// </summary>
        /// <typeparam name="T">
        ///     The type of the Cryptographic Service Provider to use.
        /// </typeparam>
        /// <param name="instance">
        ///     The instance being extended.
        /// </param>
        /// <param name="key">
        ///     The key passed into the Cryptographic Service Provider algorithm.
        /// </param>
        /// <returns>
        ///     A base 64 encoded string representation of the hash.
        /// </returns>
        public static string GetKeyedHash<T>(this object instance, byte[] key) where T : KeyedHashAlgorithm, new() {
            var cryptoServiceProvider = new T { Key = key };
            return ComputeHash(instance, cryptoServiceProvider);
        }

        /// <summary>
        ///     Gets a MD5 hash of the current instance.
        /// </summary>
        /// <param name="instance">
        ///     The instance being extended.
        /// </param>
        /// <returns>
        ///     A base 64 encoded string representation of the hash.
        /// </returns>
        public static string GetMD5Hash(this object instance) => instance.GetHash<MD5CryptoServiceProvider>();

        /// <summary>
        ///     Gets a SHA1 hash of the current instance.
        /// </summary>
        /// <param name="instance">
        ///     The instance being extended.
        /// </param>
        /// <returns>
        ///     A base 64 encoded string representation of the hash.
        /// </returns>
        public static string GetSHA1Hash(this object instance) => instance.GetHash<SHA1CryptoServiceProvider>();

        private static string ComputeHash<T>(object instance, T cryptoServiceProvider) where T : HashAlgorithm, new() {
            var serializer = new DataContractSerializer(instance.GetType());
            using var memoryStream = new MemoryStream();
            serializer.WriteObject(memoryStream, instance);
            cryptoServiceProvider.ComputeHash(memoryStream.ToArray());
            return Convert.ToBase64String(cryptoServiceProvider.Hash ?? Array.Empty<byte>());
        }
    }

}