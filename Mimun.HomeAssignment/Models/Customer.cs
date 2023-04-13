using System;
using System.Collections.Generic;

namespace Mimun.HomeAssignment.Models;

public partial class Customer
{
    public int Id { get; set; }

    public string IdNumber { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string? City { get; set; }

    public string? Street { get; set; }

    public string? HouseNumber { get; set; }

    public string? PostalCode { get; set; }

    public virtual ICollection<Contract> Contracts { get; set; } = new List<Contract>();
}
