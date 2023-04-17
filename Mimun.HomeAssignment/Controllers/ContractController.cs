using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Mimun.HomeAssignment.DTOs;
using Mimun.HomeAssignment.Extensions;
using Mimun.HomeAssignment.Repository;
using System.Collections.Generic;

namespace Mimun.HomeAssignment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ContractController : ControllerBase
    {
        readonly IContractRepository _contractRepository;
        readonly IMemoryCache _cache;
        public ContractController(IContractRepository contractRepository, IMemoryCache cache)
        {
            _contractRepository = contractRepository;
            _cache = cache;
        }

        [HttpGet]
        public async Task<IActionResult> Get(int customerId)
        {
            string cacheKey = $"contract_{customerId}";
            var contracts = await _cache.GetFromCache<IEnumerable<ContractDto>>(cacheKey, () => _contractRepository.GetContractsByCustomer(customerId));
            if (contracts == null)
            {
                return NotFound();
            }

            return Ok(contracts);
        }
    }
}
