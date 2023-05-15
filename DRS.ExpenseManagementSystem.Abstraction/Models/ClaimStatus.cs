using System;
using System.Collections.Generic;

namespace DRS.ExpenseManagementSystem.Abstraction.Models
{
    public partial class ClaimStatus
    {
        public ClaimStatus()
        {
            ExpensesClaims = new HashSet<ExpensesClaim>();
        }

        public int Id { get; set; }
        public string? ClaimState { get; set; }

        public virtual ICollection<ExpensesClaim> ExpensesClaims { get; set; }
    }
}
