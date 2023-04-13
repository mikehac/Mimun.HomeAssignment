using System;
using System.Collections.Generic;

namespace Mimun.HomeAssignment.Models;

public partial class ContractType
{
    public int Id { get; set; }

    public string Type { get; set; } = null!;

    public virtual ICollection<Contract> Contracts { get; set; } = new List<Contract>();
}
