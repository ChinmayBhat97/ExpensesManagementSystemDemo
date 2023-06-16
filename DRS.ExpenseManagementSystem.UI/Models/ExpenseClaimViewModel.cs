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
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = false)]
        public DateTime? StartDate { get; set; }
        [Required]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = false)]
        public DateTime? EndDate { get; set; }
        [Required]
        public decimal? TotalAmount { get; set; }
        [Required]
        public int? ProjectId { get; set; }
        [Required]
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
        public List<IFormFile>? ExpenseProof { get; set; }
        public List<IndividualExpenditureViewModel> IndividualExpenditures { get; set; }
        public Department Dept { get; set; }
        public Project Project { get; set; }
        public Employee Emp { get; set; }
        public ClaimStatus StatusNavigation { get; set; }
        public string? Manager { get; set; }
        public string? FManager { get; set; }
    }
}
