using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Mimun.HomeAssignment.DTOs;
using Mimun.HomeAssignment.Extensions;
using Mimun.HomeAssignment.Repository;

namespace Mimun.HomeAssignment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        readonly ICustomerRepository _customerRepository;
        readonly IMemoryCache _cache;
        public CustomerController(ICustomerRepository customerRepository, IMemoryCache cache)
        {
            _customerRepository = customerRepository;
            _cache = cache;
        }

        [HttpGet]
        public async Task<IActionResult> Get(string idNumber, bool customerExistsCheck = false)
        {
            if (customerExistsCheck)
            {
                string customerExistKey = $"customerExist_{idNumber}";
                bool customerExists = await _cache.GetFromCache<bool>(customerExistKey, () => _customerRepository.CustomerExists(idNumber));
                
                return Ok(customerExists);
            }

            CustomerResponse response = await _cache.GetFromCache<CustomerResponse>(idNumber, () => _customerRepository.GetByIdNumber(idNumber));
            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] AddressDto address)
        {
            var updateResult = await _customerRepository.UpdateAddress(address);
            //TODO:Clear Cache because it became out of date after address change

            if (!updateResult)
                return Accepted(new { updated = false });
            return Accepted(new { updated = true });
        }
    }
}
