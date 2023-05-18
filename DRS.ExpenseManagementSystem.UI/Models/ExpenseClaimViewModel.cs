﻿namespace DRS.ExpenseManagementSystem.UI.Models
{
    public class ExpenseClaimViewModel
    {
        public int Id { get; set; }
        public int? EmpId { get; set; }
        public int? DeptId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public decimal? TotalAmount { get; set; }
        public int? ProjectId { get; set; }
        public DateTime? ClaimRequestDate { get; set; }
        public int? Status { get; set; }
        public string? ManagerRemarks { get; set; }
        public string? FinanceManagerRemarks { get; set; }
        public DateTime? ManagerApprovedOn { get; set; }
        public DateTime? FinanceManagerApprovedOn { get; set; }
    }
}