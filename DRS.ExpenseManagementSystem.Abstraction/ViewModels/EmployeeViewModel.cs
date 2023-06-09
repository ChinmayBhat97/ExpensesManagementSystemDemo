﻿using DRS.ExpenseManagementSystem.Abstraction.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DRS.ExpenseManagementSystem.Abstraction.ViewModels
{
    public class EmployeeViewModel
    {
        public int Id { get; set; }
        public int? EmpId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? City { get; set; }
        public int? Gender { get; set; }
        public string? Designation { get; set; }
        public string? PanNumber { get; set; }
        public string? AccountNumber { get; set; }
        public string? Ifsc { get; set; }
        public string? BankName { get; set; }
        public DateTime? DateOfJoining { get; set; }
        public int? DeptId { get; set; }
        public DateTime? CreatedAt { get; set; }

        public Department? dept { get; set; }
    }
}
