using System;
using System.Collections.Generic;

namespace WPF_CourierFirm.Model;

public partial class StatusDelivery
{
    public int StatusDeliveryId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Delivery> Deliveries { get; set; } = new List<Delivery>();
}
