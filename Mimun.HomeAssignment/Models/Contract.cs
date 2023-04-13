using System;
using System.Collections.Generic;

namespace Mimun.HomeAssignment.Models;

public partial class Contract
{
    public int Id { get; set; }

    public string ContractNumber { get; set; } = null!;

    public string Name { get; set; } = null!;

    public int? TypeId { get; set; }

    public int CustomerId { get; set; }

    public virtual Customer Customer { get; set; } = null!;

    public virtual ICollection<Package> Packages { get; set; } = new List<Package>();

    public virtual ContractType? Type { get; set; }
}
