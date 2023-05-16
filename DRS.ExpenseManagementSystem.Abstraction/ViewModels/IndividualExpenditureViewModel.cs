using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DRS.ExpenseManagementSystem.Abstraction.ViewModels
{
    public class IndividualExpenditureViewModel
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
    }
}
