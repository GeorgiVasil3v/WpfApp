using Newtonsoft.Json;
using System.Diagnostics;
using WpfApp.Base.Interfaces;
using WpfApp.Base.Models;

namespace WpfApp.Core.Services
{
    internal class TimeZoneService : ITimeZoneService
    {
        #region ITimeZoneService implementation

        public async Task<TimeZoneData?> GetTimeZoneData(string timeZone)
        {
            return await GetRequest<TimeZoneData>(ApiUrls.GetTimeZoneUrl(timeZone));
        }

        async Task<string[]> ITimeZoneService.GetAllTimeZones()
        {
            var result = await GetRequest<string[]>(ApiUrls.AllTimeZones);
            return result ?? Array.Empty<string>();
        }

        #endregion

        #region Helpers

        static async Task<T?> GetRequest<T>(string url)
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri(url),
                };

                using var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(json);
            }
            catch (HttpRequestException requestException)
            {
                Trace.TraceError(requestException.ToString());
            }
            catch (JsonException deserializeException)
            {
                Trace.TraceError(deserializeException.ToString());
            }

            return default;
        }

        #endregion
    }
}
