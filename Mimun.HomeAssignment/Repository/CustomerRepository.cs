using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Mimun.HomeAssignment.DTOs;
using Mimun.HomeAssignment.Models;

namespace Mimun.HomeAssignment.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly MimunDbContext _context;
        private readonly IMapper _mapper;

        public CustomerRepository(MimunDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CustomerResponse> GetByIdNumber(string idNumber)
        {
            CustomerResponse response = new CustomerResponse();
            var customer = await _context.Customers.SingleOrDefaultAsync(x => x.IdNumber == idNumber);
            if (customer != null)
            {
                response.Customer = _mapper.Map<CustomerDto>(customer);
                var contracts = await _context.Contracts.Where(x => x.CustomerId == customer.Id)
                    .Include(x => x.Type)
                    .ToArrayAsync();
                if (contracts.Any())
                {
                    response.Contracts = _mapper.Map<List<ContractDto>>(contracts);
                }
            }

            return response;
        }
    }
}
