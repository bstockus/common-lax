using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using System.Web;

namespace Lax.Helpers.SSRSReports {

    public static class SsrsReportRunner {

        public static async Task<byte[]> RenderReportAsPdf(SSRSConfiguration options, string reportPath, object reportParams) {

            var configurationOptions = options;

            var reportParamsFields = reportParams.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);

            var reportUrl = $"{configurationOptions.ReportServerUrl}{reportPath}" +
                            $"{reportParamsFields.Aggregate("", (s, info) => $"{s}&{EncodeParameter(info, reportParams)}")}&rs:Format=PDF&rc:EmbedFonts=None";

            var httpClientHandler = new HttpClientHandler {
                Credentials = string.IsNullOrWhiteSpace(configurationOptions.ReportUserName)
                    ? CredentialCache.DefaultNetworkCredentials
                    : new NetworkCredential(
                        configurationOptions.ReportUserName,
                        configurationOptions.ReportPassword,
                        configurationOptions.ReportDomain)
            };

            using var httpClient = new HttpClient(httpClientHandler);
            return await httpClient.GetByteArrayAsync(reportUrl);
        }

        private static string EncodeParameter(PropertyInfo info, object reportParams) {
            if (info.PropertyType != typeof(string[])) {
                return $"{info.Name}={HttpUtility.UrlEncode((string) info.GetValue(reportParams).ToString())}";
            }

            var values = info.GetValue(reportParams) as string[];
            var urlValues = (values ?? Array.Empty<string>())
                .Aggregate("", (x, y) => $"{x}&{info.Name}={HttpUtility.UrlEncode(y)}")
                .TrimEnd('&').TrimStart('&');
            return urlValues;

        }

    }

}
