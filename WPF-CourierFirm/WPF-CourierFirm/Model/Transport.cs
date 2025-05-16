using System;
using System.Collections.Generic;

namespace WPF_CourierFrim.Model;

public partial class Transport
{
    public int TransportId { get; set; }

    public string LicensePlate { get; set; } = null!;

    public string Color { get; set; } = null!;

    public string Brand { get; set; } = null!;

    public string Model { get; set; } = null!;

    public short Year { get; set; }

    public string InfoCar => $"{Brand} {Model}, {LicensePlate}, год: {Year}, цвет: {Color}";

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
}
