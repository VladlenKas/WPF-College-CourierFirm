using System;
using System.Collections.Generic;

namespace WPF_CourierFrim.Model;

public partial class Employee
{
    public int EmployeeId { get; set; }

    public int? TransportId { get; set; }

    public int PostId { get; set; }

    public string Firstname { get; set; } = null!;

    public string Lastname { get; set; } = null!;

    public string? Patronymic { get; set; }

    public string Fullname => $"{Lastname} {Firstname} {Patronymic}"; 

    public DateOnly Birthday { get; set; }

    public string Passport { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public string Login { get; set; } = null!;

    public string Password { get; set; } = null!;

    public sbyte IsDeleted { get; set; }

    public virtual ICollection<EmployeeDelivery> EmployeeDeliveries { get; set; } = new List<EmployeeDelivery>();

    public virtual Post Post { get; set; } = null!;

    public virtual Transport? Transport { get; set; }
}
