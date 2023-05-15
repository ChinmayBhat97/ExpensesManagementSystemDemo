using System;
using System.Collections.Generic;

namespace DRS.ExpenseManagementSystem.Abstraction.Models
{
public partial class Department
{
        public Department()
        {
            Employees = new HashSet<Employee>();
            ExpensesClaims = new HashSet<ExpensesClaim>();
        }

        public int DeptId { get; set; }
        public string? DepartementName { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }
        public virtual ICollection<ExpensesClaim> ExpensesClaims { get; set; }
    }
}
