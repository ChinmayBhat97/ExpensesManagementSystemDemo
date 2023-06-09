﻿using System;
using System.Collections.Generic;

namespace DRS.ExpenseManagementSystem.Abstraction.Models
{
    public partial class User
    {
        public User()
        {
            Employees = new HashSet<Employee>();
        }

        public int Id { get; set; }
        public string? EmployeeCode { get; set; }
        public string? Password { get; set; }
        public bool? IsAccountLocked { get; set; }
        public bool? IsActive { get; set; }
        public int? Role { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }
    }
}
