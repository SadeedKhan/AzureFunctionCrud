using HelperLibrary.CosmosDb.Interfaces;
using Microsoft.Azure.Cosmos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HelperLibrary.CosmosDb.Repository
{
    public class CosmosDbContainerFactory : ICosmosDbContainerFactory
    {
        private readonly CosmosClient _cosmosClient;
        private readonly string _databaseName;
        private readonly List<ContainerInfo> _containers;

        public CosmosDbContainerFactory(CosmosClient cosmosClient,
                                   string databaseName,
                                   List<ContainerInfo> containers)
        {
            _databaseName = databaseName;
            _containers = containers;
            _cosmosClient = cosmosClient;
        }

        public ICosmosDbContainer GetContainer(string containerName)
        {
            //if (_containers.Where(x => x.Name == containerName) == null)
            //{
            EnsureDbSetupAsync().Wait();
            //}

            return new CosmosDbContainer(_cosmosClient, _databaseName, containerName);
        }

        public async Task EnsureDbSetupAsync()
        {
            DatabaseResponse database = await _cosmosClient.CreateDatabaseIfNotExistsAsync(_databaseName);

            foreach (ContainerInfo container in _containers)
            {
                await database.Database.CreateContainerIfNotExistsAsync(container.Name, $"{container.PartitionKey}");
            }
        }
    }
}
