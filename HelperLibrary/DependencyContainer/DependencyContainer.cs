using HelperLibrary.CosmosDb.Interfaces;
using HelperLibrary.CosmosDb.Repository;
using HelperLibrary.Repositories.Implementation;
using HelperLibrary.Repositories.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace HelperLibrary.DependencyContainer
{
    public class DependencyContainer
    {
        public static void RegisterBaetotiApiRepositories(IServiceCollection services)
        {
            ServiceProvider provider = services.BuildServiceProvider();


            #region Scoped
            services.AddTransient<IEmployeeRepository, EmployeeRepository>();
            services.AddTransient<ICosmosDbContainer, CosmosDbContainer>();
            services.AddTransient<ICosmosDbContainerFactory, CosmosDbContainerFactory>();
            #endregion

        }
    }
}
