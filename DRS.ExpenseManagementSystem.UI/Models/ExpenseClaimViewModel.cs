using DRS.ExpenseManagementSystem.UI.Models;
using System.ComponentModel.DataAnnotations;

namespace DRS.ExpenseManagementSystem.UI.Models
{
    public class ExpenseClaimViewModel
    {
        public int Id { get; set; }

        [Required]
        public int? EmpId { get; set; }
        [Required]
        public int? DeptId { get; set; }
        [Required]
        public DateTime? StartDate { get; set; }
        [Required]
        public DateTime? EndDate { get; set; }
        [Required]
        public decimal? TotalAmount { get; set; }
        [Required]
        public int? ProjectId { get; set; }
        [Required]
        public DateTime? ClaimRequestDate { get; set; }
        public int? Status { get; set; }
        public string? ManagerRemarks { get; set; }
        public string? FinanceManagerRemarks { get; set; }
        public DateTime? ManagerApprovedOn { get; set; }
        public DateTime? FinanceManagerApprovedOn { get; set; }
        public List<IFormFile> ExpenseProof { get; set; }
        public List<IndividualExpenditureViewModel> IndividualExpenditures { get; set; }

        public IndividualExpenditure individualExpenditure { get; set; }
    }
}
