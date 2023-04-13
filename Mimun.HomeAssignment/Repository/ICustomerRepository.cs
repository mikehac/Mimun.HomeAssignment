using Mimun.HomeAssignment.DTOs;

namespace Mimun.HomeAssignment.Repository
{
    public interface ICustomerRepository
    {
        Task<CustomerResponse> GetByIdNumber(string idNumber);
    }
}
