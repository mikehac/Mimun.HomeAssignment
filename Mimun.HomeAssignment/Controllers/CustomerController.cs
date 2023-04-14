using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<IActionResult> Get(string idNumber) 
        {
            var response = await _customerRepository.GetByIdNumber(idNumber);
            return Ok(response);
        }
    }
}
