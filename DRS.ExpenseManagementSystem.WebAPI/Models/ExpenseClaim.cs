using System;
using System.Collections.Generic;

namespace DRS.ExpenseManagementSystem.WebAPI.Models
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
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public decimal? TotalAmount { get; set; }
        public int? ProjectId { get; set; }
        public DateTime? ClaimRequestDate { get; set; }
        public int? Status { get; set; }
        public string? ManagerRemarks { get; set; }
        public string? FinanceManagerRemarks { get; set; }
        public DateTime? ManagerApprovedOn { get; set; }
        public DateTime? FinanceManagerApprovedOn { get; set; }

        public virtual Department? Dept { get; set; }
        public virtual Employee? Emp { get; set; }
        public virtual Project? Project { get; set; }
        public virtual ClaimStatus? StatusNavigation { get; set; }
        public virtual ICollection<IndividualExpenditure> IndividualExpenditures { get; set; }
    }
}
