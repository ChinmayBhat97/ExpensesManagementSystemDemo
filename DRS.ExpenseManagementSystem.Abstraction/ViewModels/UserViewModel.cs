using DRS.ExpenseManagementSystem.Abstraction.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DRS.ExpenseManagementSystem.Abstraction.ViewModels
{
    public class UserViewModel
    {
        public int? Id { get; set; }
        public string? EmployeeCode { get; set; }
        public string? Password { get; set; }
        public bool? IsAccountLocked { get; set; }
        public bool? IsActive { get; set; }
        public int Role { get; set; }


       
        public int? EmpId { get; set; }
       public string? FirstName { get; set; }

       public string? Designation { get; set; }
        public List<Employee>? employee { get; set; }

        

    }
}
