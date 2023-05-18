using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DRS.ExpenseManagementSystem.WebAPI.Models
{
    public partial class Department
    {
        public Department()
        {
            Employees = new HashSet<Employee>();
            ExpenseClaims = new HashSet<ExpenseClaim>();
        }
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string? Name { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }
        public virtual ICollection<ExpenseClaim> ExpenseClaims { get; set; }
    }
}
