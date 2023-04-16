using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mimun.HomeAssignment.DTOs;
using Mimun.HomeAssignment.Repository;

namespace Mimun.HomeAssignment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        readonly ICustomerRepository _customerRepository;

        public CustomerController(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get(string idNumber, bool customerExistsCheck = false)
        {
            if (customerExistsCheck)
            {
                bool customerExists = await _customerRepository.CustomerExists(idNumber);
                return Ok(customerExists);
            }

            var response = await _customerRepository.GetByIdNumber(idNumber);
            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] AddressDto address)
        {
            var updateResult = await _customerRepository.UpdateAddress(address);
            if (!updateResult)
                return Accepted(new { updated = false });
            return Accepted(new { updated = true });
        }
    }
}
