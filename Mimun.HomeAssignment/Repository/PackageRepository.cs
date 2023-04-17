using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Mimun.HomeAssignment.DTOs;
using Mimun.HomeAssignment.Models;

namespace Mimun.HomeAssignment.Repository
{
    public class PackageRepository : IPackageRepository
    {
        private readonly MimunDbContext _context;
        private readonly IMapper _mapper;

        public PackageRepository(MimunDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PackageDto>> GetPackageByContractId(int contractId)
        {
            var packages = await _context.Packages.Where(x => x.ContractId == contractId)
                .Include(x => x.PackageType)
                .ToListAsync();

            return _mapper.Map<List<PackageDto>>(packages);
        }
    }
}
