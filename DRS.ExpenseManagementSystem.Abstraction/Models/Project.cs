using System;
using System.Collections.Generic;

namespace DRS.ExpenseManagementSystem.Abstraction.Models
{
    public partial class Project
    {
        public Project()
        {
            ExpensesClaims = new HashSet<ExpensesClaim>();
        }

        public int Id { get; set; }
        public int? EmpId { get; set; }
        public string? ProjectName { get; set; }
        public DateTime? ProjectStartDate { get; set; }
        public DateTime? ProjectEndDate { get; set; }
        public string? ClientName { get; set; }

        public virtual Employee? Emp { get; set; }
        public virtual ICollection<ExpensesClaim> ExpensesClaims { get; set; }
    }
}
