using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Mimun.HomeAssignment.DTOs;
using Mimun.HomeAssignment.Models;

namespace Mimun.HomeAssignment.Repository
{
    public class ContractRepository : IContractRepository
    {
        private readonly MimunDbContext _context;
        private readonly IMapper _mapper;

        public ContractRepository(MimunDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ContractDto>> GetContractsByCustomer(int customerId)
        {
            var contracts = await _context.Contracts.Where(x => x.CustomerId == customerId).ToListAsync();

            return _mapper.Map<List<ContractDto>>(contracts);
        }
    }
}
