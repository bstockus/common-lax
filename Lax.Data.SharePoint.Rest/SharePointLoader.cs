using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Lax.Data.SharePoint.Rest.FieldConverters;
using Newtonsoft.Json.Linq;

namespace Lax.Data.SharePoint.Rest {

    public class SharePointLoader<TEntity> where TEntity : Entity {

        private readonly EntityContentTypeMap<TEntity> _entityContentTypeMap;

        public SharePointLoader(EntityContentTypeMap<TEntity> entityContentTypeMap) {
            _entityContentTypeMap = entityContentTypeMap;
            
        }

        public async Task UpdateItem(
            NetworkCredential networkCredential, 
            TEntity entity, 
            IEnumerable<Expression<Func<TEntity, object>>> fields = null,
            CancellationToken cancellationToken = default) {

            var client = CreateHttpClient(networkCredential);

            var formDigestValue = await GetFormDigest(client, cancellationToken);

            client.DefaultRequestHeaders.Add("X-RequestDigest", formDigestValue);

            var listItemEntityTypeFullName = await GetListItemEntityTypeFullName(client, cancellationToken);

            var request = new JObject();

            var metadataObject = new JObject { { "type", listItemEntityTypeFullName } };
            request.Add("__metadata", metadataObject);

            var entityId = entity.Id;

            var fieldNamesToInclude = new List<string>();
            if (fields != null) {
                fieldNamesToInclude =
                    fields.Select(_ => (LambdaHelpers.GetMemberInfo(_) as PropertyInfo)?.Name).ToList();
            }

            foreach (var fieldBuilder in _entityContentTypeMap.FieldBuilders) {

                var fieldInfo = fieldBuilder.AsFieldInfo();

                if (fieldInfo.InternalName.Equals("ID")) {
                    continue;
                }

                var fieldConverter = FieldConverterRegistry.GetFieldConverterForMember(fieldInfo.PropertyExpression);

                var propertyInfo = LambdaHelpers.GetMemberInfo(fieldInfo.PropertyExpression) as PropertyInfo;

                Debug.Assert(propertyInfo != null, nameof(propertyInfo) + " != null");

                if (fields != null && !fieldNamesToInclude.Contains(propertyInfo.Name)) {
                    continue;
                }

                var propertyValue = propertyInfo.GetValue(entity);

                request.Add(fieldConverter.FieldNameMapper(fieldInfo.InternalName),
                    fieldConverter.ToSpValue(propertyValue));

            }

            var requestText = request.ToString();

            var requestContent = new StringContent(requestText);
            requestContent.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json;odata=verbose");
            
            client.DefaultRequestHeaders.Add("IF-MATCH", "*");
            client.DefaultRequestHeaders.Add("X-HTTP-Method", "MERGE");

            var response = await client.PostAsync(
                $"{_entityContentTypeMap.SiteUrl}_api/web/lists('{_entityContentTypeMap.ListGuid}')/Items({entityId})",
                requestContent, cancellationToken);

            if (!response.IsSuccessStatusCode) {
                throw new Exception(response.StatusCode.ToString());
            }
        }

        public async Task<TEntity> AddItem(
            NetworkCredential networkCredential, 
            TEntity entity,
            CancellationToken cancellationToken = default) {

            var client = CreateHttpClient(networkCredential);

            var formDigestValue = await GetFormDigest(client, cancellationToken);

            client.DefaultRequestHeaders.Add("X-RequestDigest", formDigestValue);

            var listItemEntityTypeFullName = await GetListItemEntityTypeFullName(client, cancellationToken);

            var request = new JObject();

            var metadataObject = new JObject {{"type", listItemEntityTypeFullName}};
            request.Add("__metadata", metadataObject);

            foreach (var fieldBuilder in _entityContentTypeMap.FieldBuilders) {

                var fieldInfo = fieldBuilder.AsFieldInfo();

                if (fieldInfo.InternalName.Equals("ID")) {
                    continue;
                }

                var fieldConverter = FieldConverterRegistry.GetFieldConverterForMember(fieldInfo.PropertyExpression);

                var propertyInfo = LambdaHelpers.GetMemberInfo(fieldInfo.PropertyExpression) as PropertyInfo;

                Debug.Assert(propertyInfo != null, nameof(propertyInfo) + " != null");
                var propertyValue = propertyInfo.GetValue(entity);

                request.Add(fieldConverter.FieldNameMapper(fieldInfo.InternalName),
                    fieldConverter.ToSpValue(propertyValue));

            }

            var requestText = request.ToString();

            var requestContent = new StringContent(requestText);
            requestContent.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json;odata=verbose");

            var response = await client.PostAsync(
                $"{_entityContentTypeMap.SiteUrl}_api/web/lists('{_entityContentTypeMap.ListGuid}')/Items",
                requestContent, cancellationToken);

            if (!response.IsSuccessStatusCode) {
                throw new Exception(response.StatusCode.ToString());
            }

            var responseText = await response.Content.ReadAsStringAsync(cancellationToken);

            var newEntity = ParseEntityResponse(responseText);

            return newEntity;

        }

        public async Task DeleteItem(
            NetworkCredential networkCredential,
            TEntity entity,
            CancellationToken cancellationToken = default) {

            var client = CreateHttpClient(networkCredential);

            var formDigestValue = await GetFormDigest(client, cancellationToken);

            client.DefaultRequestHeaders.Add("X-RequestDigest", formDigestValue);

            var entityId = entity.Id;

            

            var requestContent = new StringContent("");
            requestContent.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json;odata=verbose");

            client.DefaultRequestHeaders.Add("IF-MATCH", "*");
            client.DefaultRequestHeaders.Add("X-HTTP-Method", "DELETE");

            var response = await client.PostAsync(
                $"{_entityContentTypeMap.SiteUrl}_api/web/lists('{_entityContentTypeMap.ListGuid}')/Items({entityId})",
                requestContent, cancellationToken);

            if (!response.IsSuccessStatusCode) {
                throw new Exception(response.StatusCode.ToString());
            }

        }

        public async Task<IEnumerable<TEntity>> FetchAll(
            NetworkCredential networkCredential,
            CancellationToken cancellationToken = default) {

            var client = CreateHttpClient(networkCredential);

            var itemIds = await GetListItems(client, cancellationToken);

            var itemStrings = new List<string>();

            var itemsRemaining = itemIds.ToList();

            while (itemsRemaining.Any()) {

                var currentItems = itemsRemaining.Take(100);
                itemsRemaining = itemsRemaining.Skip(100).ToList();

                itemStrings.AddRange(await GetBatchedListItems(
                    client,
                    currentItems,
                    cancellationToken));

            }

            return itemStrings.Select(ParseEntityResponse).ToList();

        }

        private static HttpClient CreateHttpClient(ICredentials networkCredential) {
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
            return client;
        }

        private TEntity ParseEntityResponse(string itemResponse) {

            var itemResponsesObject = JObject.Parse(itemResponse)
                ["d"] as JObject;


            Debug.Assert(itemResponsesObject != null, nameof(itemResponsesObject) + " != null");

            var itemValues =
                itemResponsesObject.Properties().ToDictionary(_ => _.Name, _ => _.Value);

            var newObject = Activator.CreateInstance<TEntity>();

            foreach (var fieldBuilder in _entityContentTypeMap.FieldBuilders) {

                var fieldInfo = fieldBuilder.AsFieldInfo();

                var fieldConverter =
                    FieldConverterRegistry.GetFieldConverterForMember(fieldInfo.PropertyExpression);

                var propertyInfo = LambdaHelpers.GetMemberInfo(fieldInfo.PropertyExpression) as PropertyInfo;

                //var fieldName = fieldInfo.EntityPropertyName ?? XmlConvert.EncodeName(listFields[fieldInfo.InternalName] ?? "");

                var fieldValue = itemValues[fieldConverter.FieldNameMapper(fieldInfo.InternalName)];

                Debug.Assert(propertyInfo != null, nameof(propertyInfo) + " != null");

                propertyInfo.SetValue(newObject, fieldConverter.FromSpValue(fieldValue));

            }

            return newObject;
        }

        private async Task<IEnumerable<int>> GetListItems(
            HttpClient client,
            CancellationToken cancellationToken = default) {
            var response = await client.GetAsync(
                $"{_entityContentTypeMap.SiteUrl}_api/web/lists('{_entityContentTypeMap.ListGuid}')/Items?$top=5000&$skiptoken=Paged=TRUE&$select=ID",
                cancellationToken);

            if (!response.IsSuccessStatusCode) {
                throw new Exception(response.StatusCode.ToString());
            }

            var responseText = await response.Content.ReadAsStringAsync(cancellationToken);


            return !(JObject.Parse(responseText)["d"]?["results"] is JArray itemsList)
                ? Array.Empty<int>()
                : itemsList.Where(_ => _ != null).Select(_ => ((_ as JObject)?["ID"] ?? 0).Value<int>());
        }

        private async Task<string> GetListItem(
            HttpClient client,
            int id,
            CancellationToken cancellationToken = default) {

            var response =
                await client.GetAsync(
                    $"{_entityContentTypeMap.SiteUrl}_api/web/lists('{_entityContentTypeMap.ListGuid}')/Items({id})",
                    cancellationToken);

            if (!response.IsSuccessStatusCode) {
                throw new Exception(response.StatusCode.ToString());
            }

            return await response.Content.ReadAsStringAsync(cancellationToken);

        }

        private async Task<IEnumerable<string>> GetBatchedListItems(
            HttpClient client,
            IEnumerable<int> ids,
            CancellationToken cancellationToken = default) {

            var batchId = $"batch_{Guid.NewGuid().ToString()}";


            var batchRequest = new StringBuilder();

            foreach (var id in ids) {
                batchRequest.AppendLine($"--{batchId}");
                batchRequest.AppendLine("Content-Type: application/http");
                batchRequest.AppendLine("Content-Transfer-Encoding: binary");
                batchRequest.AppendLine("");
                batchRequest.AppendLine(
                    $"GET {_entityContentTypeMap.SiteUrl}_api/web/lists('{_entityContentTypeMap.ListGuid}')/Items({id}) HTTP/1.1");
                batchRequest.AppendLine("Accept: application/json;odata=verbose");
                batchRequest.AppendLine("");

            }

            batchRequest.AppendLine($"--{batchId}--");

            var requestContent = new StringContent(batchRequest.ToString());

            var formDigestValue = await GetFormDigest(client, cancellationToken);

            requestContent.Headers.ContentType = MediaTypeHeaderValue.Parse($"multipart/mixed; boundary=\"{batchId}\"");
            requestContent.Headers.TryAddWithoutValidation("X-RequestDigest", formDigestValue);

            var response = await client.PostAsync($"{_entityContentTypeMap.SiteUrl}_api/$batch", requestContent, cancellationToken);

            if (!response.IsSuccessStatusCode) {
                throw new Exception(response.StatusCode.ToString());
            }

            var responseContents = await response.Content.ReadAsStringAsync();

            
            var boundaryContentType = response.Content.Headers.ContentType.Parameters.FirstOrDefault(_ => _.Name.Equals("boundary"))?.Value ?? "";

            var splitResponseContents = responseContents.Split($"--{boundaryContentType}--")[0].Split($"--{boundaryContentType}\r\n").Skip(1);


            return (splitResponseContents.Select(splitResponseContent => splitResponseContent.Split("\r\n\r\n"))
                .Where(sectionSplits => sectionSplits[1].StartsWith("HTTP/1.1 200 OK\r\n"))
                .Select(sectionSplits => sectionSplits[2])).ToList();

        }


        //private async Task<Dictionary<string, string>> GetListFields(
        //    HttpClient client,
        //    CancellationToken cancellationToken = default) {
        //    var response =
        //        await client.GetAsync(
        //            $"{_entityContentTypeMap.SiteUrl}_api/web/lists('{_entityContentTypeMap.ListGuid}')/Fields",
        //            cancellationToken);

        //    if (!response.IsSuccessStatusCode) {
        //        throw new Exception(response.StatusCode.ToString());
        //    }

        //    var responseText = await response.Content.ReadAsStringAsync();

        //    if (!((JObject.Parse(responseText))["d"]?["results"] is JArray fieldsList)) {
        //        return new Dictionary<string, string>();
        //    }

        //    return fieldsList.Select(_ => _ as JObject).ToDictionary(_ => _["StaticName"].Value<string>(),
        //        _ => _["EntityPropertyName"].Value<string>());

        //}

        private async Task<string> GetListItemEntityTypeFullName(
            HttpClient client,
            CancellationToken cancellationToken = default) {

            var response =
                await client.GetAsync(
                    $"{_entityContentTypeMap.SiteUrl}_api/web/lists('{_entityContentTypeMap.ListGuid}')",
                    cancellationToken);

            if (!response.IsSuccessStatusCode) {
                throw new Exception(response.StatusCode.ToString());
            }

            var responseText = await response.Content.ReadAsStringAsync(cancellationToken);
            var results = JObject.Parse(responseText)["d"] as JObject;

            return results?["ListItemEntityTypeFullName"]?.Value<string>() ?? "";

        }

        private async Task<string> GetFormDigest(
            HttpClient client,
            CancellationToken cancellationToken = default) {

            var response =
                await client.PostAsync(
                    $"{_entityContentTypeMap.SiteUrl}_api/contextinfo", new StringContent(""), 
                    cancellationToken);

            if (!response.IsSuccessStatusCode) {
                throw new Exception(response.StatusCode.ToString());
            }

            var responseText = await response.Content.ReadAsStringAsync(cancellationToken);
            var results = JObject.Parse(responseText)["d"] as JObject;

            return (results?["GetContextWebInformation"] as JObject)?["FormDigestValue"]?.Value<string>();

        }


    }

}