using System;
using System.Collections.Generic;

namespace DRS.ExpenseManagementSystem.Abstraction.Models
{
    public partial class ExpenseCategory
    {
        public ExpenseCategory()
        {
            IndividualExpenditures = new HashSet<IndividualExpenditure>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }

        public virtual ICollection<IndividualExpenditure> IndividualExpenditures { get; set; }
    }
}
