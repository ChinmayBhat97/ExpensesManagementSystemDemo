using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DRS.ExpenseManagementSystem.UI.Models
{
    public partial class Employee
    {
        public Employee()
        {
            ExpenseClaims = new HashSet<ExpenseClaim>();
            Projects = new HashSet<Project>();
        }

        public int Id { get; set; }
        public int? EmpId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? City { get; set; }
        public int? Gender { get; set; }
        public string? Designation { get; set; }
        public string? PanNumber { get; set; }
        public string? AccountNumber { get; set; }
        public string? Ifsc { get; set; }
        public string? BankName { get; set; }
        
        public DateTime? DateOfJoining { get; set; }
        public int? DeptId { get; set; }
        public DateTime? CreatedAt { get; set; }

        public virtual Department? Dept { get; set; }
        public virtual User Emp { get; set; }
        public virtual ICollection<ExpenseClaim> ExpenseClaims { get; set; }
        public virtual ICollection<Project> Projects { get; set; }
    }
}
