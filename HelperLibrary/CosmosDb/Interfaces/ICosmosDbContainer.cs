
using Microsoft.Azure.Cosmos;

namespace HelperLibrary.CosmosDb.Interfaces
{
    public interface ICosmosDbContainer
    {
        Container _container { get; }
    }
}
