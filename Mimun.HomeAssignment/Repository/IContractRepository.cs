
using Mimun.HomeAssignment.DTOs;

namespace Mimun.HomeAssignment.Repository
{
    public interface IContractRepository
    {
        Task<IEnumerable<ContractDto>> GetContractsByCustomer(int customerId);
    }
}
