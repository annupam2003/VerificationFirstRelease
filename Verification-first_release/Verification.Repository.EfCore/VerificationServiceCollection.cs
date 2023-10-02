using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using Verification.Data.EfCore;
using Verification.EntityModel;
using Verification.Mapper;
using Verification.DomainModel;
using Verification.Repository.EfCore.Master;

namespace Verification.Repository.EfCore
{
    public static class VerificationServiceCollection
    {
        public static IServiceCollection DataBaseConnections(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<VerificationDbContext>(option => option.UseSqlServer(configuration.GetConnectionString("EfCore")));

            services.AddScoped<IEntityMapper<StateZone, StateZoneModel>, MStateZone>();
            services.AddScoped<IStateZoneRepository, StateZoneRepository>();

            services.AddScoped<IEntityMapper<State, StateModel>, MState>();
            services.AddScoped<IStateRepository, StateRepository>();

            services.AddScoped<IEntityMapper<AreaZone, AreaZoneModel>, MAreaZone>();
            services.AddScoped<IAreaZoneRepository, AreaZoneRepository>();

            services.AddScoped<IEntityMapper<Area, AreaModel>, MArea>();
            services.AddScoped<IAreaRepository, AreaRepository>();

            return services;
        }
    }
}
