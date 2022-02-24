using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.IO;
using HelperLibrary.CosmosDb;
using HelperLibrary.Extentions;
using HelperLibrary.DependencyContainer;
using Microsoft.Azure.WebJobs.Hosting;
using Microsoft.Azure.WebJobs;

[assembly: WebJobsStartup(typeof(AzureFunctionCrud.Startup))]
namespace AzureFunctionCrud
{
    public class Startup : IWebJobsStartup
    {
        public  void Configure(IWebJobsBuilder builder)
        {
            ConfigureServices(builder.Services);
        }

        public void ConfigureServices(IServiceCollection services)
        {
            // Configurations
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile($"local.settings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();

            // Use a singleton Configuration throughout the application
            services.AddSingleton<IConfiguration>(configuration);

            // Bind database-related bindings
            CosmosDbSettings cosmosDbConfig = configuration.GetSection("CosmosDb").Get<CosmosDbSettings>();
            // register CosmosDB client and data repositories
            services.AddCosmosDb(cosmosDbConfig.EndpointUrl,
                                 cosmosDbConfig.PrimaryKey,
                                 cosmosDbConfig.DatabaseName,
                                 cosmosDbConfig.Containers);

            DependencyContainer.RegisterBaetotiApiRepositories(services);

        }

    }
}
