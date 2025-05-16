using System;
using System.Collections.Generic;

namespace WPF_CourierFrim.Model;

public partial class Content
{
    public int ContentId { get; set; }

    public int ContentTypeId { get; set; }

    public string Description { get; set; } = null!;

    public decimal Weight { get; set; }

    public virtual ContentType ContentType { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
