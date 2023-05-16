using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace DRS.ExpenseManagementSystem.UI.Models
{
    public class ExpenseClaimViewModel
    {
        public int Id { get; set; }
        public int? EmpId { get; set; }
        public int? DeptId { get; set; }

        [Display(Name = "Start Date")]
        public DateTime? StartDate { get; set; }

        [Display(Name = "End Date")]
        public DateTime? EndDate { get; set; }

        [Display(Name = "Total Amount")]
        public decimal? TotalAmount { get; set; }
        public int? ProjectId { get; set; }

        [Display(Name = "Date")]
        public DateTime? ClaimRequestDate { get; set; }
        public int? Status { get; set; }

        [Display(Name = "Remarks by Manager")]
        public string? ManagerRemarks { get; set; }

        [Display(Name = "Remarks by Finance Manager")]
        public string? FinanceManagerRemarks { get; set; }

        [Display(Name = "Approved by Manager")]
        public DateTime? ManagerApprovedOn { get; set; }

        [Display(Name = "Approved by Finance Manager")]
        public DateTime? FinanceManagerApprovedOn { get; set; }
    }
}
