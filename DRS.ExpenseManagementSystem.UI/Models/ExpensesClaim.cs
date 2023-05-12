using System;
using System.Collections.Generic;

namespace DRS.ExpenseManagementSystem.WebAPI.Models
{
    public partial class ExpensesClaim
    {
        public ExpensesClaim()
        {
            IndividualExpenditures = new HashSet<IndividualExpenditure>();
        }

        public int ClaimId { get; set; }
        public int? EmpId { get; set; }
        public int? DeptId { get; set; }
        public DateTime? PeriodStart { get; set; }
        public DateTime? PeriodEnd { get; set; }
        public decimal? TotalAmount { get; set; }
        public int? ProjectId { get; set; }
        public DateTime? ClaimingDate { get; set; }
        public int? StatusId { get; set; }
        public string? RemarksManager { get; set; }
        public string? RemarksFinanceManager { get; set; }
        public DateTime? ApprovedByManager { get; set; }
        public DateTime? ApprovedByFinanceManager { get; set; }
        public DateTime? CreatedAt { get; set; }

        public virtual Department? Dept { get; set; }
        public virtual Employee? Emp { get; set; }
        public virtual Project? Project { get; set; }
        public virtual ClaimStatus? Status { get; set; }
        public virtual ICollection<IndividualExpenditure> IndividualExpenditures { get; set; }
    }
}
