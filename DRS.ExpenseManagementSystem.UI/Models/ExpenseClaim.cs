using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DRS.ExpenseManagementSystem.UI.Models
{
    public partial class ExpenseClaim
    {
        public ExpenseClaim()
        {
            IndividualExpenditures = new HashSet<IndividualExpenditure>();
        }

        public int Id { get; set; }
        public int? EmpId { get; set; }
        public int? DeptId { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = false)]
        public DateTime? StartDate { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = false)]
        public DateTime? EndDate { get; set; }
        public decimal? TotalAmount { get; set; }
        public int? ProjectId { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = false)]
        public DateTime? ClaimRequestDate { get; set; }
        public int? Status { get; set; }

        public int? StatusManager { get; set; }
        public string? ManagerRemarks { get; set; }
        public string? FinanceManagerRemarks { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = false)]
        public DateTime? ManagerApprovedOn { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = false)]
        public DateTime? FinanceManagerApprovedOn { get; set; }
        public string? Manager { get; set; }
        public string? FManager { get; set; }
        public virtual Department? Dept { get; set; }
        public virtual Employee? Emp { get; set; }
        public virtual Project? Project { get; set; }
        public virtual ClaimStatus? StatusNavigation { get; set; }
        public virtual ICollection<IndividualExpenditure> IndividualExpenditures { get; set; }
    }
}
