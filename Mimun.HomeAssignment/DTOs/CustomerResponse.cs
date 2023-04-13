namespace Mimun.HomeAssignment.DTOs
{
    public class CustomerResponse
    {
        public CustomerDto Customer { get; set; }
        public List<ContractDto> Contracts { get; set; }
    }
}
