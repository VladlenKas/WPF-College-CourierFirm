using System;
using System.Collections.Generic;

namespace WPF_CourierFrim.Model;

public partial class EmployeeDelivery
{
    public int EmployeeDeliveryId { get; set; }

    public int EmployeeId { get; set; }

    public int DeliveryId { get; set; }

    public virtual Delivery Delivery { get; set; } = null!;

    public virtual Employee Employee { get; set; } = null!;
}
