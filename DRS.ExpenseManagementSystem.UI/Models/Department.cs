using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DRS.ExpenseManagementSystem.UI.Models
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

        public virtual ICollection<Department> Departments { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }
        public virtual ICollection<ExpenseClaim> ExpenseClaims { get; set; }
    }
}
