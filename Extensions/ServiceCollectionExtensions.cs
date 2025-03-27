using Microsoft.Extensions.DependencyInjection;
using EventManagementApp.Data.Repositories;
using EventManagementApp.Services;

namespace EventManagementApp.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            // Repositories
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IEventRepository, EventRepository>();
            services.AddScoped<IParticipantRepository, ParticipantRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            // Services
            services.AddScoped<IEventService, EventService>();
            services.AddScoped<IParticipantService, ParticipantService>();

            return services;
        }
    }
}
