using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DRS.ExpenseManagementSystem.WebAPI.Models
{
    public partial class ExpenseCategory
    {
        public ExpenseCategory()
        {
            IndividualExpenditures = new HashSet<IndividualExpenditure>();
        }
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string? Name { get; set; }

        public virtual ICollection<IndividualExpenditure> IndividualExpenditures { get; set; }
    }
}
