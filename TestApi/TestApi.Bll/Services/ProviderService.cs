using TestApi.Domain.Entities;
using TestApi.Domain.Interfaces.Bll;
using TestApi.Domain.Interfaces.Repositories;

namespace TestApi.Bll.Services
{
    public class ProviderService : IProviderService
    {
        private readonly IRepository<Provider> _providerRepository;

        public ProviderService(IRepository<Provider> providerRepository)
        {
            _providerRepository = providerRepository;
        }

        public async Task<IEnumerable<Provider>> GetProvidersAsync()
        {
            return await _providerRepository.GetAllAsync();
        }

        public async Task<Provider> GetProviderByIdAsync(int id)
        {
            return await _providerRepository.GetByIdAsync(id);
        }
    }
}