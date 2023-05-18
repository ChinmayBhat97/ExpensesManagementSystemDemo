
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DRS.ExpenseManagementSystem.WebAPI.Models
{
    public partial class ClaimStatus
    {
        public ClaimStatus()
        {
            ExpenseClaims = new HashSet<ExpenseClaim>();
        }
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string? Name { get; set; }

        public virtual ICollection<ExpenseClaim> ExpenseClaims { get; set; }
    }
}
