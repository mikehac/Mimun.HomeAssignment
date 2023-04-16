using Mimun.HomeAssignment.DTOs;

namespace Mimun.HomeAssignment.Repository
{
    public interface ICustomerRepository
    {
        Task<bool> CustomerExists(string idNumber);
        Task<CustomerResponse> GetByIdNumber(string idNumber);
        Task<bool> UpdateAddress(AddressDto address);
    }
}
