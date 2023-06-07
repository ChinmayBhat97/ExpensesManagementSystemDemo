using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DRS.ExpenseManagementSystem.UI.Models
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
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? StartDate { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? EndDate { get; set; }
        public string? Client { get; set; }
       
        public virtual Employee? Emp { get; set; }
        public virtual ICollection<ExpenseClaim> ExpenseClaims { get; set; }
    }
}
