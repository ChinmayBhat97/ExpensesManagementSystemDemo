using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DRS.ExpenseManagementSystem.Abstraction.ViewModels
{
    public class EmployeeViewModel
    {
        public int EmpId { get; set; }
        public int? UserId { get; set; }
        public string? EmpFirstName { get; set; }
        public string? EmpLastName { get; set; }
        public string? EmpEmailId { get; set; }
        public string? EmpPhoneNumber { get; set; }
        public int? EmpGender { get; set; }
        public string? EmpDesignation { get; set; }
        public string? EmpPanNumber { get; set; }
        public string? EmpAccountNumber { get; set; }
        public string? EmpIfsc { get; set; }
        public string? EmpBankName { get; set; }
        public DateTime? DateofJoining { get; set; }
        public int? DeptId { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
