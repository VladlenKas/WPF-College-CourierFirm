using System;
using System.Collections.Generic;

namespace WPF_CourierFirm.Model;

public partial class Rate
{
    public int RateId { get; set; }

    public string Name { get; set; } = null!;

    public int Cost { get; set; }

    public string Description { get; set; } = null!;

    public sbyte IsDeleted { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
