namespace Mimun.HomeAssignment.DTOs
{
    public class ContractDto
    {
        public int Id { get; set; }

        public string ContractNumber { get; set; } = null!;

        public string Name { get; set; } = null!;

        public int? TypeId { get; set; }

        public int CustomerId { get; set; }
    }
}
