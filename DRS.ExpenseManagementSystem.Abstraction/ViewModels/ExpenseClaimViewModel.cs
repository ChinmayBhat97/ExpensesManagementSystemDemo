using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DRS.ExpenseManagementSystem.Abstraction.ViewModels
{
    public class ExpenseClaimViewModel
    {
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
    }
}
