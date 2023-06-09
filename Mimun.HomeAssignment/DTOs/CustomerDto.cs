﻿namespace Mimun.HomeAssignment.DTOs
{
    public class CustomerDto
    {
        public int Id { get; set; }

        public string IdNumber { get; set; } = null!;

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;
        public AddressDto Address { get; set; } = null!;

    }

    public class AddressDto
    {
        public int CustomerId { get; set; }
        public string? City { get; set; }
        public string? Street { get; set; }
        public string? HouseNumber { get; set; }
        public string? PostalCode { get; set; }
    }
}
