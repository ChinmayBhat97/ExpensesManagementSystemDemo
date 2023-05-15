using System;
using System.Collections.Generic;

namespace DRS.ExpenseManagementSystem.UI.Models
{
    public partial class ExpensesCategory
    {
        public ExpensesCategory()
        {
            IndividualExpenditures = new HashSet<IndividualExpenditure>();
        }

        public int Id { get; set; }
        public string? CategoryName { get; set; }

        public virtual ICollection<IndividualExpenditure> IndividualExpenditures { get; set; }
    }
}
