using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace DRS.ExpenseManagementSystem.UI.Models
{
    public partial class IndividualExpenditure
    {
        public int Id { get; set; }
        public int? ClaimId { get; set; }
        public DateTime? ExpenditureDate { get; set; }
        public string? Description { get; set; }
        public string? Category { get; set; }
        public int? ExpenseCategoryId { get; set; }
        public decimal? Amount { get; set; }
        public string? Comments { get; set; }
        public string? ReceiptNumber { get; set; }

        public string? AttachmentPath { get; set; }
        public IFormFile? ExpenseProof { get; set; }
        public bool? IsApproved { get; set; }
        public string? FinanceManagerRemarks { get; set; }
        public virtual ExpenseClaim? Claim { get; set; }
        public virtual ExpenseCategory? ExpenseCategory { get; set; }
    }
}
