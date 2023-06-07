using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Lax.Data.SharePoint.Rest {

    public static class SharePointHelpers {

        public static async Task<IEnumerable<Tuple<string, string>>>
            GetFieldsForList(
                NetworkCredential networkCredential,
                string siteUrl,
                string listGuid) {

            var client = new HttpClient(new HttpClientHandler {
                PreAuthenticate = true,
                UseDefaultCredentials = false,
                UseProxy = false,
                Credentials = networkCredential,
                AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
            });
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Add("Accept", "application/json;odata=verbose");
            client.DefaultRequestHeaders.Add("Accept-Encoding", "gzip, deflate");
            client.DefaultRequestHeaders.Add("Accept-Language", "en-US,en;q=0.9");

            var response =
                await client.GetAsync(
                    $"{siteUrl}_api/web/lists('{listGuid}')/Fields");

            if (!response.IsSuccessStatusCode) {
                throw new Exception(response.StatusCode.ToString());
            }

            var responseText = await response.Content.ReadAsStringAsync();
            var fieldsList = (JObject.Parse(responseText))["d"]["results"] as JArray;

            return fieldsList
                .Select(_ => _ as JObject)
                .Select(_ =>
                    new Tuple<string, string>(_["Title"].Value<string>(), _["EntityPropertyName"].Value<string>()));

        }

        public static async Task<SharePointUserInformation> GetUserById(
            NetworkCredential networkCredential, 
            string siteUrl, 
            int userId,
            CancellationToken cancellationToken = default) {
            
            var client = new HttpClient(new HttpClientHandler {
                PreAuthenticate = true,
                UseDefaultCredentials = false,
                UseProxy = false,
                Credentials = networkCredential,
                AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
            });
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Add("Accept", "application/json;odata=verbose");
            client.DefaultRequestHeaders.Add("Accept-Encoding", "gzip, deflate");
            client.DefaultRequestHeaders.Add("Accept-Language", "en-US,en;q=0.9");

            var response =
                await client.GetAsync(
                    $"{siteUrl}_api/web/getuserbyid('{userId}')", cancellationToken);

            if (!response.IsSuccessStatusCode) {
                throw new Exception(response.StatusCode.ToString());
            }

            var responseText = await response.Content.ReadAsStringAsync();

            var results = JObject.Parse(responseText)["d"] as JObject;

            return new SharePointUserInformation(
                userId,
                results?["LoginName"]?.Value<string>(),
                results?["Title"]?.Value<string>(),
                results?["Email"]?.Value<string>());

        }

        //public static async Task<string> GetListItemEntityTypeFullName(NetworkCredential networkCredential, string siteUrl, string listGuid) {

        //    var client = new HttpClient(new HttpClientHandler {
        //        PreAuthenticate = true,
        //        UseDefaultCredentials = false,
        //        UseProxy = false,
        //        Credentials = networkCredential,
        //        AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
        //    });
        //    client.DefaultRequestHeaders.Accept.Clear();
        //    client.DefaultRequestHeaders.Add("Accept", "application/json;odata=verbose");
        //    client.DefaultRequestHeaders.Add("Accept-Encoding", "gzip, deflate");
        //    client.DefaultRequestHeaders.Add("Accept-Language", "en-US,en;q=0.9");

        //    var response =
        //        await client.GetAsync(
        //            $"{siteUrl}_api/web/lists('{listGuid}')");

        //    if (!response.IsSuccessStatusCode) {
        //        throw new Exception(response.StatusCode.ToString());
        //    }

        //    var responseText = await response.Content.ReadAsStringAsync();
        //    var results = (JObject.Parse(responseText))["d"] as JObject;

        //    return results["ListItemEntityTypeFullName"].Value<string>();

        //}

    }

}