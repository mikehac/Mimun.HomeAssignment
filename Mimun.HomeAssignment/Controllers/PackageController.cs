using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mimun.HomeAssignment.Repository;

namespace Mimun.HomeAssignment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PackageController : ControllerBase
    {
        readonly IPackageRepository _packageRepository;

        public PackageController(IPackageRepository packageRepository)
        {
            _packageRepository = packageRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get(int packageId)
        {
            var packages = await _packageRepository.GetPackageByContractId(packageId);
            if (packages == null)
            {
                return NotFound();
            }

            return Ok(packages);
        }
    }
}
