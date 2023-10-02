using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Verification.Tracker
{
    public static class TrackerServiceCollection
    {
        public static IServiceCollection TrackerService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<ITracker, Track>();
            return services;
        }
    }
}
