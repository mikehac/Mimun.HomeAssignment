using System;
using System.Collections.Generic;

namespace Mimun.HomeAssignment.Models;

public partial class Package
{
    public int Id { get; set; }

    public int PackageTypeId { get; set; }

    public string PackageName { get; set; } = null!;

    public int Amount { get; set; }

    public int? TotalUsed { get; set; }

    public int ContractId { get; set; }
}
