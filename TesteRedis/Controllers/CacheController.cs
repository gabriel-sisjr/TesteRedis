using Domain.Interfaces.Cache;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace TesteRedis.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CacheController : ControllerBase
    {
        private readonly ICacheService _cacheService;
        public CacheController(ICacheService cacheService) => _cacheService = cacheService;

        [HttpGet]
        public async Task<IActionResult> Get(string key) => Ok(await _cacheService.GetFromCacheAsync<Retorno>(key));
    }
}
