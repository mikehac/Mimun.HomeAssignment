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
    public class PackageController : ControllerBase
    {
        readonly IPackageRepository _packageRepository;
        readonly IMemoryCache _cache;

        public PackageController(IPackageRepository packageRepository, IMemoryCache cache)
        {
            _packageRepository = packageRepository;
            _cache = cache;
        }

        [HttpGet]
        public async Task<IActionResult> Get(int packageId)
        {
            string cacheKey = $"package_{packageId}";
            var packages = await _cache.GetFromCache<IEnumerable<PackageDto>>(cacheKey, () => _packageRepository.GetPackageByContractId(packageId));
            if (packages == null)
            {
                return NotFound();
            }

            return Ok(packages);
        }
    }
}
