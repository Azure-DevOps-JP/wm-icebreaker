using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Icebreaker.Func.Functions
{
    public class PairingShedulerFunction
    {
        private readonly IHttpClientFactory httpClientFactory;
        private readonly AppSettings appSettings;

        public PairingShedulerFunction(IHttpClientFactory httpClientFactory, IOptions<AppSettings> options)
        {
            this.httpClientFactory = httpClientFactory;
            this.appSettings = options.Value;
        }

        [FunctionName("PairingShedulerFunction")]
        public async Task Run([TimerTrigger("%Icebreaker:PairingStartCRON%")]TimerInfo myTimer, ILogger log)
        {

            HttpClient httpClient = httpClientFactory.CreateClient();

            try
            {
                using (var requestMessage = new HttpRequestMessage(HttpMethod.Get, appSettings.PairingStartUrl))
                {
                    requestMessage.Headers.Add("X-Key", appSettings.PairingStartKey); 

                    var response = await httpClient.SendAsync(requestMessage);
                    response.EnsureSuccessStatusCode();
                }
            }
            catch(Exception ex)
            {
                log.LogError("Failed to pairup: {0}", ex);
            }
        }
    }
}
