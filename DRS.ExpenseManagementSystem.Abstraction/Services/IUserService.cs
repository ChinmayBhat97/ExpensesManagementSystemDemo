using DRS.ExpenseManagementSystem.Abstraction.Models;
using DRS.ExpenseManagementSystem.Abstraction.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DRS.ExpenseManagementSystem.Abstraction.Services
{
    public interface IUserService:IBaseService<User>
    {
        public Task<List<User>> GetByRoleAsync(int role);
        AuthenticationViewModel Authenticate(string UserName, string Password);
    }
}
