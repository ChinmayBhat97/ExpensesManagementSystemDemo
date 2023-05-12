using System;
using System.Collections.Generic;

namespace DRS.ExpenseManagementSystem.Abstraction.Models;

public partial class User
{
    public int Id { get; set; }

    public string? EmployeeCode { get; set; }

    public string? Password { get; set; }

    public bool? IsAccountLocked { get; set; }

    public bool? IsActive { get; set; }

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
}
