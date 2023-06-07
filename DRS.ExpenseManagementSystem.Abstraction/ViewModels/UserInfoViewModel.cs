using DRS.ExpenseManagementSystem.Abstraction.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DRS.ExpenseManagementSystem.Abstraction.ViewModels
{
    public class UserInfoViewModel
    {
        public int? Id { get; set; }
        public string? EmployeeCode { get; set; }

        public static implicit operator UserInfoViewModel(AddUserViewModel v)
        {
            throw new NotImplementedException();
        }
    }
}
