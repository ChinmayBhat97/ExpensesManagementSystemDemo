using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DRS.ExpenseManagementSystem.Abstraction.ViewModels
{
    public class AuthenticationViewModel
    {
        public UserViewModel? userDetails { get; set; }
        public bool IsAuthenticated { get; set; }
        public string? ErrorMessage { get; set; }
    }
}
