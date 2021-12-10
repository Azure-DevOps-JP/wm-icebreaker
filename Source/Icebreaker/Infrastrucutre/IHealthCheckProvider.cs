using Icebreaker.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Icebreaker.Infrastrucutre
{
    public interface IHealthCheckProvider
    {
        Task<IEnumerable<HealthCheckItemResult>> GetHealthCheckAsync();
    }
}
