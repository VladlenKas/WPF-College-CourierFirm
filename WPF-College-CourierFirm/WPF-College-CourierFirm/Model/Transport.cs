using System;
using System.Collections.Generic;

namespace WPF_College_CourierFirm.Model;

public partial class Transport
{
    public int TransportId { get; set; }

    public string LicensePlate { get; set; } = null!;

    public string Color { get; set; } = null!;

    public string Brand { get; set; } = null!;

    public string Model { get; set; } = null!;

    public short Year { get; set; }

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
}
