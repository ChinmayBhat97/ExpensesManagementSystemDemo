﻿using System;

namespace DRS.ExpenseManagementSystem.UI.Models
{
    public class IndividualExpenditureViewModel
    {
        public int Id { get; set; }
        public DateTime? ExpenditureDate { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public decimal? Amount { get; set; }
        public string Comments { get; set; }
        public string ReceiptNumber { get; set; }
        public string AttachmentPath { get; set; }
        public bool IsApproved { get; set; }
        public string FinanceManagerRemarks { get; set; }
    }
}