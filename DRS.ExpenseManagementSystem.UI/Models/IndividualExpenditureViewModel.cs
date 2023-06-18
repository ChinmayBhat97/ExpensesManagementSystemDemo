using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DRS.ExpenseManagementSystem.UI.Models
{
    public class IndividualExpenditureViewModel
    {
        public int Id { get; set; }
        public int ClaimId { get; set; }
        [Required]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = false)]
        public DateTime? ExpenditureDate { get; set; }
        [Required]
        public string? Description { get; set; }
        [Required]
        public string? Category { get; set; }
        [Required]
        public int? ExpenseCategoryId { get; set; }
        [Required]
        public decimal? Amount { get; set; }
        [Required]
        public string? Comments { get; set; }
        [Required]
        public string? ReceiptNumber { get; set; }
        [Required]
        public string? AttachmentPath { get; set; }
        public int IsDelete { get; set; }
        
        public bool? IsApproved { get; set; }
        public string? FinanceManagerRemarks { get; set; }
        public virtual ExpenseClaim? Claim { get; set; }
        public virtual ExpenseCategory? ExpenseCategory { get; set; }
        public IFormFile? ExpenseProof { get; set; }
    }
}
