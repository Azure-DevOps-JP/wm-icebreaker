using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Icebreaker.Func.Functions
{
    public class HealthCheckFunction
    {
        private readonly IHttpClientFactory httpClientFactory;
        private readonly AppSettings appSettings;

        public HealthCheckFunction(IHttpClientFactory httpClientFactory, IOptions<AppSettings> options)
        {
            this.httpClientFactory = httpClientFactory;
            this.appSettings = options.Value;
        }


        [FunctionName("HealthCheckFunction")]
        public async Task RunAsync([TimerTrigger("%Icebreaker:HealthCheckCRON%")]TimerInfo myTimer, ILogger log)
        {
            HttpClient httpClient = httpClientFactory.CreateClient();

            try
            {
                using (var requestMessage = new HttpRequestMessage(HttpMethod.Get, appSettings.HealthCheckUrl))
                {
                    var response = await httpClient.SendAsync(requestMessage);
                    response.EnsureSuccessStatusCode();
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    log.LogInformation("Azure Bot Service dependencies healthcheck: {HealthCheckResult}", jsonResponse);
                }
            }
            catch (Exception ex)
            {
                log.LogError("Failed to pairup: {0}", ex);
            }
        }
    }
}
