using HelperLibrary.Models.Base;
using Microsoft.Azure.Cosmos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HelperLibrary.Repositories.Interfaces.Base
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(string id);
        Task InsertAsync(T obj);
        Task UpdateAsync(string id, T obj);
        Task DeleteAsync(string id);

        //CosmosDb Extentions
        string ContainerName { get; }
        string GenerateId(T entity);
        PartitionKey ResolvePartitionKey(string entityId);
    }
}
