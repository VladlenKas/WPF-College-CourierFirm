using System;
using System.Collections.Generic;

namespace WPF_CourierFirm.Model;

public partial class Order
{
    public int OrderId { get; set; }

    public int OrganisationId { get; set; }

    public int RateId { get; set; }

    public int ContentId { get; set; }

    public string ReceivingAddress { get; set; } = null!;

    public string DeliveryAddress { get; set; } = null!;

    public DateTime DatetimeCreation { get; set; }

    public DateTime? DatetimeCompletion { get; set; }

    public string FullnameClient { get; set; } = null!;

    public string PhoneClient { get; set; } = null!;

    public virtual Content Content { get; set; } = null!;

    public virtual ICollection<Delivery> Deliveries { get; set; } = new List<Delivery>();

    public virtual Organisation Organisation { get; set; } = null!;

    public virtual Rate Rate { get; set; } = null!;

}
