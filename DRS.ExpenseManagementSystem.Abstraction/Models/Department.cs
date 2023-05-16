using System;
using System.Collections.Generic;

namespace DRS.ExpenseManagementSystem.Abstraction.Models
{
    public partial class Department
    {
        public Department()
        {
            Employees = new HashSet<Employee>();
            ExpenseClaims = new HashSet<ExpenseClaim>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }
        public virtual ICollection<ExpenseClaim> ExpenseClaims { get; set; }
    }
}
