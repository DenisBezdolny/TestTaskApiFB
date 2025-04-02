using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TestApi.DTOs;
using TestApi.Domain.Interfaces.Bll;


namespace TestApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProvidersController : ControllerBase
    {
        private readonly IProviderService _providerService;
        private readonly IMapper _mapper;

        public ProvidersController(IProviderService providerService, IMapper mapper)
        {
            _providerService = providerService;
            _mapper = mapper;
        }

        // GET: api/providers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProviderDto>>> GetProviders()
        {
            var providers = await _providerService.GetProvidersAsync();
            var providerDtos = _mapper.Map<IEnumerable<ProviderDto>>(providers);
            return Ok(providerDtos);
        }

        // GET: api/providers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProviderDto>> GetProvider(int id)
        {
            var provider = await _providerService.GetProviderByIdAsync(id);
            if (provider == null)
                return NotFound();
            var providerDto = _mapper.Map<ProviderDto>(provider);
            return Ok(providerDto);
        }
    }
}
