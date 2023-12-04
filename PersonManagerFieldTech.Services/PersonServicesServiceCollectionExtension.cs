using Microsoft.Extensions.DependencyInjection;
using PersonManagerFieldTech.Repository.Interfaces;
using PersonManagerFieldTech.Repository;
using PersonManagerFieldTech.Services.Services.Interfaces;
using PersonManagerFieldTech.Services.Services;

namespace PersonManagerFieldTech.Services
{
    public static class PersonServicesServiceCollectionExtension
    {
        public static void AddPersonServiceDependencies(this IServiceCollection services)
        {
            services.AddScoped<IPersonService, PersonService>();
        }
    }
}