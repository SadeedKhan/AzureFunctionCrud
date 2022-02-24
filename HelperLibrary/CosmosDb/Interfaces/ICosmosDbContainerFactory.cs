using System.Threading.Tasks;

namespace HelperLibrary.CosmosDb.Interfaces
{
    public interface ICosmosDbContainerFactory
    {
        ICosmosDbContainer GetContainer(string containerName);

        Task EnsureDbSetupAsync();
    }
}
