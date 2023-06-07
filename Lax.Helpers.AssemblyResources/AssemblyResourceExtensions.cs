using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Lax.Helpers.AssemblyResources {

    public static class AssemblyResourceExtensions {

        public static async Task<string> GetAssemblyResource(this Assembly assembly, string resourceName) {
            var fullResourceName = assembly
                .GetManifestResourceNames()
                .Single(str => str.EndsWith(resourceName));

            await using var stream = assembly.GetManifestResourceStream(fullResourceName);
            if (stream == null) {
                throw new NullReferenceException("Stream was null");
            }

            using var streamReader = new StreamReader(stream);
            return await streamReader.ReadToEndAsync();
        }

        public static async Task<byte[]> GetAssemblyResourceAsBytes(this Assembly assembly, string resourceName) {
            var fullResourceName = assembly
                .GetManifestResourceNames()
                .Single(str => str.EndsWith(resourceName));

            await using var stream = assembly.GetManifestResourceStream(fullResourceName);
            if (stream == null) {
                throw new NullReferenceException("Stream was null");
            }

            await using var memoryStream = new MemoryStream();
            await stream.CopyToAsync(memoryStream);
            return memoryStream.ToArray();
        }

    }

}