using System;
using System.Collections.Generic;

namespace DRS.ExpenseManagementSystem.Abstraction.Models
{
    public partial class IndividualExpenditure
    {
        public int Id { get; set; }
        public int? ClaimId { get; set; }
        public DateTime? BillingDate { get; set; }
        public string? Description { get; set; }
        public string? Category { get; set; }
        public int? ExpenseCategoryId { get; set; }
        public decimal? Amount { get; set; }
        public string? PurposeofClaim { get; set; }
        public string? ReceiptNumber { get; set; }
        public string? InvoiceDocument { get; set; }

        public virtual ExpensesClaim? Claim { get; set; }
        public virtual ExpensesCategory? ExpenseCategory { get; set; }
    }
}
