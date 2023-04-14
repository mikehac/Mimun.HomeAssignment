using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mimun.HomeAssignment.Repository;

namespace Mimun.HomeAssignment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContractController : ControllerBase
    {
        readonly IContractRepository _contractRepository;
        public ContractController(IContractRepository contractRepository)
        {
            _contractRepository = contractRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get(int customerId)
        {
            var contracts = await _contractRepository.GetContractsByCustomer(customerId);
            if (contracts == null)
            {
                return NotFound();
            }

            return Ok(contracts);
        }
    }
}
