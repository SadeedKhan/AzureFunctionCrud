using HelperLibrary.CosmosDb.Interfaces;
using HelperLibrary.Models.Base;
using HelperLibrary.Repositories.Interfaces.Base;
using Microsoft.Azure.Cosmos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelperLibrary.Repositories.Implementation.Base
{
    public abstract class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly ICosmosDbContainerFactory _cosmosDbContainerFactory;

        public abstract string ContainerName { get; }

        public abstract string GenerateId(T entity);

        public abstract PartitionKey ResolvePartitionKey(string entityId);

        private readonly Container _container;

        public Repository(ICosmosDbContainerFactory cosmosDbContainerFactory)
        {
            this._cosmosDbContainerFactory = cosmosDbContainerFactory ?? throw new ArgumentNullException(nameof(ICosmosDbContainerFactory));
            this._container = this._cosmosDbContainerFactory.GetContainer(ContainerName)._container;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            var queryString = @"Select * from c";
            var query = _container.GetItemQueryIterator<T>(new QueryDefinition(queryString));
            var results = new List<T>();
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();
                results.AddRange(response.ToList());
            }
            return results;
        }

        public async Task<T> GetByIdAsync(string id)
        {
            try
            {
                var response = await _container.ReadItemAsync<T>(id, ResolvePartitionKey(id));
                return response.Resource;
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }
        }

        public async Task InsertAsync(T obj)
        {
            obj.Id = GenerateId(obj);
            await _container.CreateItemAsync<T>(obj, ResolvePartitionKey(obj.Id));
        }

        public async Task UpdateAsync(string id, T obj)
        {
            await _container.UpsertItemAsync(obj, ResolvePartitionKey(obj.Id));
        }

        public async Task DeleteAsync(string id)
        {
            await _container.DeleteItemAsync<T>(id, ResolvePartitionKey(id));
        }
    }
}
