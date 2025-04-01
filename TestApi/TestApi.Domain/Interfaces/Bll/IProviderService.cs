using TestApi.Domain.Entities;

namespace TestApi.Domain.Interfaces.Bll
{
    public interface IProviderService
    {
        Task<IEnumerable<Provider>> GetProvidersAsync();
        Task<Provider> GetProviderByIdAsync(int id);
    }
}
