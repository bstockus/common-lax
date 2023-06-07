using Lax.Helpers.Cryptography;

namespace Lax.Business.Bus.Caching {

    public class BusRequestCacheEntry {

        public string RequestCacheKey { get; }
        public object Result { get; }

        public static string GenerateRequestCacheKey(
            object request) =>
            $"{request.GetType().FullName}-{request.GetSHA1Hash()}";

        public BusRequestCacheEntry(
            string requestCacheKey,
            object result) {
            RequestCacheKey = requestCacheKey;
            Result = result;
        }

    }

}