using System;
using System.Collections.Generic;

namespace Mimun.HomeAssignment.Models;

public partial class PackageType
{
    public int Id { get; set; }

    public string? TypeName { get; set; }

    public virtual ICollection<Package> Packages { get; set; } = new List<Package>();
}
