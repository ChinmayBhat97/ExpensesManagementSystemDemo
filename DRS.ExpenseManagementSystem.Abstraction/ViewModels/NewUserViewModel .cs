using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DRS.ExpenseManagementSystem.Abstraction.ViewModels
{
    
    public class NewUserViewModel
    {
        public UserInfoViewModel? newUserInfo { get; set; }

        public string? ErrorMessage { get; set; }
    }
}
