using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace API_Gateway.Services
{
    public class HttpClientWrapper : IHttpClientWrapper
    {
        private readonly AppSettingsConfiguration _appSettingsConfiguration;

        public HttpClientWrapper(IOptions<AppSettingsConfiguration> appSettingOptions)
        {
            _appSettingsConfiguration = appSettingOptions.Value;
        }

        public async Task<string> CallApiWithParams(string apiUrl, string param, string paramName)
        {
            using (var client = new HttpClient())
            { 
                client.BaseAddress = new Uri(_appSettingsConfiguration.EncryptionServiceBaseUrl);

                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var builder = new UriBuilder(apiUrl);
                builder.Query = $"{paramName}='{param}'";

                var responseMessage = await client.GetAsync(builder.Uri);

                if (responseMessage.IsSuccessStatusCode)
                {
                    var response = await responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
                    return JsonConvert.DeserializeObject<string>(response) ?? string.Empty;
                }

                return string.Empty;
            }
        }
    }
}
