using System;
using System.Collections.Generic;

namespace WPF_CourierFrim.Model;

public partial class Organisation
{
    public int OrganisationId { get; set; }

    public string Name { get; set; } = null!;

    public string Address { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public string Email { get; set; } = null!;

    public sbyte IsDeleted { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
