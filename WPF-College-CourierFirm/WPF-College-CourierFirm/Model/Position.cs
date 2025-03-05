using System;
using System.Collections.Generic;

namespace WPF_College_CourierFirm.Model;

public partial class Position
{
    public int PositionId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
}
