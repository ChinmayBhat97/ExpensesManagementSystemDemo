using System;
using System.Collections.Generic;

namespace DRS.ExpenseManagementSystem.Abstraction.Models
{
    public partial class Employee
    {
        public Employee()
        {
            ExpensesClaims = new HashSet<ExpensesClaim>();
            Projects = new HashSet<Project>();
        }

        public int EmpId { get; set; }
        public int? UserId { get; set; }
        public string? EmpFirstName { get; set; }
        public string? EmpLastName { get; set; }
        public string? EmpEmailId { get; set; }
        public string? EmpPhoneNumber { get; set; }
        public int? EmpGender { get; set; }
        public string? EmpDesignation { get; set; }
        public string? EmpPanNumber { get; set; }
        public string? EmpAccountNumber { get; set; }
        public string? EmpIfsc { get; set; }
        public string? EmpBankName { get; set; }
        public DateTime? DateofJoining { get; set; }
        public int? DeptId { get; set; }

        public DateTime? CreatedAt { get; set; }

        public virtual Department? Dept { get; set; }
        public virtual UserTable? User { get; set; }
        public virtual ICollection<ExpensesClaim> ExpensesClaims { get; set; }
        public virtual ICollection<Project> Projects { get; set; }
    }
}
