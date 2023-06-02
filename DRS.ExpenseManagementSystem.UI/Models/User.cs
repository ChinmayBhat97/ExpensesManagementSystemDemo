using System;
using System.Collections.Generic;

namespace DRS.ExpenseManagementSystem.UI.Models
{
    public partial class User
    {
        public User(string role, string v)
        {
            Employees = new HashSet<Employee>();
        }

        public int Id { get; set; }
        public string? EmployeeCode { get; set; }
        public string? Password { get; set; }
        public bool? IsAccountLocked { get; set; }
        public bool? IsActive { get; set; }
        public int? Role { get; set; }
        public Employee Employee { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }
    }
}
