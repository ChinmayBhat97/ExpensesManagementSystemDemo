using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DRS.ExpenseManagementSystem.WebAPI.Models
{
    public partial class Project
    {
        public Project()
        {
            ExpenseClaims = new HashSet<ExpenseClaim>();
        }
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int? EmpId { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? Client { get; set; }

        public virtual Employee? Emp { get; set; }
        public virtual ICollection<ExpenseClaim> ExpenseClaims { get; set; }
    }
}
