using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DRS.ExpenseManagementSystem.UI.Models
{
    public partial class ExpenseCategory
    {
        public ExpenseCategory()
        {
            IndividualExpenditures = new HashSet<IndividualExpenditure>();
        }

        public int Id { get; set; }

        [Required(ErrorMessage="Name is required")]
        public string? Name { get; set; }

        public virtual ICollection<IndividualExpenditure> IndividualExpenditures { get; set; }
    }
}
