


using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PersonManagerFieldTech.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonManagerFieldTech.Repository
{
    public static class PersonRepoSeviceCollectionExtension
    {
        public static void AddPersonRepoDependencies(this IServiceCollection services)
        {

            services.AddScoped<IPersonRepository, PersonRepository>();
        }
    }
    
}
