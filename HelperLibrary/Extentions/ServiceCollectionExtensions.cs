using HelperLibrary.CosmosDb;
using HelperLibrary.CosmosDb.Interfaces;
using HelperLibrary.CosmosDb.Repository;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;

namespace HelperLibrary.Extentions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCosmosDb(this IServiceCollection services,
                                                     string endpointUrl,
                                                     string primaryKey,
                                                     string databaseName,
                                                     List<ContainerInfo> containers)
        {
            CosmosClient client = new CosmosClient(endpointUrl, primaryKey);
            CosmosDbContainerFactory cosmosDbClientFactory = new CosmosDbContainerFactory(client, databaseName, containers);
            services.AddSingleton<ICosmosDbContainerFactory>(cosmosDbClientFactory);

            return services;
        }
    }
}
