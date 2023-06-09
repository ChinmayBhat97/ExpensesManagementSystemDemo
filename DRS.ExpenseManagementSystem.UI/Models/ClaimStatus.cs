﻿using System;
using System.Collections.Generic;

namespace DRS.ExpenseManagementSystem.UI.Models
{
    public partial class ClaimStatus
    {
        public ClaimStatus()
        {
            ExpenseClaims = new HashSet<ExpenseClaim>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }
        public int IsDelete { get; set; }

        public virtual ICollection<ExpenseClaim> ExpenseClaims { get; set; }
    }
}
