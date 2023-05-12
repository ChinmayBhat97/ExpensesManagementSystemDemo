using System;
using System.Collections.Generic;

namespace DRS.ExpenseManagementSystem.WebAPI.Models
{
    public partial class UserTable
    {
        public UserTable()
        {
            Employees = new HashSet<Employee>();
        }

        public int Id { get; set; }
        public string? EmployeeId { get; set; }
        public string? Password { get; set; }
        public bool? IsAccountLocked { get; set; }
        public bool? IsActive { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }
    }
}
