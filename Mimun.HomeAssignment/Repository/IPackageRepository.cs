using Mimun.HomeAssignment.DTOs;

namespace Mimun.HomeAssignment.Repository
{
    public interface IPackageRepository
    {
        Task<IEnumerable<PackageDto>> GetPackageByContractId(int contractId);
    }
}
