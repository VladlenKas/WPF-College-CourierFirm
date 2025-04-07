using System;
using System.Collections.Generic;

namespace WPF_CourierFrim.Model;

public partial class Delivery
{
    public int DeliveryId { get; set; }

    public int OrderId { get; set; }

    public int StatusDeliveryId { get; set; }

    public sbyte? PaymentMethod { get; set; }

    public DateTime? DatetimeReceiving { get; set; }

    public DateTime? DatetimePresentation { get; set; }

    public virtual ICollection<EmployeeDelivery> EmployeeDeliveries { get; set; } = new List<EmployeeDelivery>();

    public virtual Order Order { get; set; } = null!;

    public virtual StatusDelivery StatusDelivery { get; set; } = null!;
}
