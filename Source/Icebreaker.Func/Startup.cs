using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

[assembly: FunctionsStartup(typeof(Icebreaker.Func.Startup))]

namespace Icebreaker.Func
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddOptions<AppSettings>()
                .Configure<IConfiguration>((settings, configuration) =>
                {
                    configuration.GetSection("Icebreaker").Bind(settings);
                });
            builder.Services.AddHttpClient();
        }
    }
}
