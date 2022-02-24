using HelperLibrary.CosmosDb.Interfaces;
using HelperLibrary.Models;
using HelperLibrary.Repositories.Implementation.Base;
using HelperLibrary.Repositories.Interfaces;
using Microsoft.Azure.Cosmos;
using System;
using System.Collections.Generic;
using System.Text;

namespace HelperLibrary.Repositories.Implementation
{
    public class EmployeeRepository : Repository<EmployeeDetails>, IEmployeeRepository
    {
        public override string ContainerName { get; } = "EmployeeDetails";

        public override string GenerateId(EmployeeDetails entity) => $"{entity.LastName}:{Guid.NewGuid()}";

        public override PartitionKey ResolvePartitionKey(string entityId) => new PartitionKey(entityId.Split(':')[0]);
        public EmployeeRepository(ICosmosDbContainerFactory factory) : base(factory)
        {

        }
    }
}
