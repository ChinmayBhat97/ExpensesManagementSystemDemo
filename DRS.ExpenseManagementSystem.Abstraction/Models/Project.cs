using System;
using System.Collections.Generic;

namespace DRS.ExpenseManagementSystem.Abstraction.Models
{
    public partial class Project
    {
        public Project()
        {
            ExpenseClaims = new HashSet<ExpenseClaim>();
        }

        public int Id { get; set; }
        public int? EmpId { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? Client { get; set; }

        //public virtual Department? Dept { get; set; }
        public virtual Employee? Emp { get; set; }
        public int IsDelete { get; set; }
        public virtual ICollection<ExpenseClaim> ExpenseClaims { get; set; }

     //   public virtual ICollection<Department> Departments { get; set; }
    }
}
