using Icebreaker.Common.Models;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Icebreaker.Infrastrucutre
{
    public class TeamsTrafficManagerAvailibilityCheck : IHealthCheckProvider
    {
        public async Task<IEnumerable<HealthCheckItemResult>> GetHealthCheckAsync()
        {
            List<string> hostsToPing = new List<string>()
            {
                "login.botframework.com", "smba.trafficmanager.net", "login.microsoftonline.com",
            };

            List<Task<HealthCheckItemResult>> pingTasks = new List<Task<HealthCheckItemResult>>();

            foreach (var host in hostsToPing)
            {
                pingTasks.Add(PingHost(host));
            }

            var results = await Task.WhenAll<HealthCheckItemResult>(pingTasks.ToArray());
            return results;
        }

        private async Task<HealthCheckItemResult> PingHost(string host, int port = 443)
        {
            HealthCheckItemResult result = new HealthCheckItemResult()
            {
                Description = string.Format("Attempt (outbound) to reach out to {0}", host),
                HealthStatus = HealthStatus.Healthy,
            };

            try
            {
                TcpClient tcpClient = new TcpClient();
                await tcpClient.ConnectAsync(host, port);
                return result;
            }
            catch (Exception ex)
            {
                result.HealthStatus = HealthStatus.Unhealthy;
                result.ErrorMessage = ex.Message;
                return result;
            }
        } 
    }
}
