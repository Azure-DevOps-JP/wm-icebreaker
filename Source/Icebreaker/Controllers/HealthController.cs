using Icebreaker.Infrastrucutre;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Icebreaker.Controllers
{
    public class HealthController : ApiController
    {
        [Route("api/health-check")]
        public async Task<IHttpActionResult> Get()
        {
            TeamsTrafficManagerAvailibilityCheck teamsCheck = new TeamsTrafficManagerAvailibilityCheck();
            var result = await teamsCheck.GetHealthCheckAsync();

            return this.Json(result);
        }
    }
}
