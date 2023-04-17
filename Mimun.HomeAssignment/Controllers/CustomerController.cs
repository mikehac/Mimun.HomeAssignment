using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Mimun.HomeAssignment.DTOs;
using Mimun.HomeAssignment.Extensions;
using Mimun.HomeAssignment.Repository;
using Mimun.HomeAssignment.Services;

namespace Mimun.HomeAssignment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CustomerController : ControllerBase
    {
        readonly ICustomerRepository _customerRepository;
        readonly IAuthService _authService;
        readonly IMemoryCache _cache;
        public CustomerController(ICustomerRepository customerRepository, IAuthService authService, IMemoryCache cache)
        {
            _customerRepository = customerRepository;
            _authService = authService;
            _cache = cache;
        }

        [HttpGet("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(string idNumber)
        {
            string customerExistKey = $"customerExist_{idNumber}";
            bool customerExists = await _cache.GetFromCache<bool>(customerExistKey, () => _customerRepository.CustomerExists(idNumber));
            if (!customerExists)
            {
                return NotFound();
            }

            string token = _authService.GetToken(idNumber);
            return Ok(new { customerExist = customerExists, token });
        }

        [HttpGet]
        public async Task<IActionResult> Get(string idNumber, bool customerExistsCheck = false)
        {
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
