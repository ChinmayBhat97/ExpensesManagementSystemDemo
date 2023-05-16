using DRS.ExpenseManagementSystem.Abstraction.Models;
using DRS.ExpenseManagementSystem.Abstraction.Repository;
using DRS.ExpenseManagementSystem.Abstraction.Services;
using DRS.ExpenseManagementSystem.Abstraction.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DRS.ExpenseManagementSystem.Business.Services
{
    public class UserTableService : BaseService<UserTable>, IUserTableService
    {
        private IUserTableRepository userTableRepository;
        public UserTableService(IUserTableRepository repository) : base(repository)
        {
        }

        public AuthenticationViewModel Authenticate(string userName, string password)
        {
            var userTable = userTableRepository.GetByUsernameAndPassword( userName, password);
            if (userTable == null)
            {
                return new AuthenticationViewModel
                {
                    IsAuthenticated = false,
                    ErrorMessage = "ERR001"
                };
            }
            else
            {
                return new AuthenticationViewModel
                {
                    IsAuthenticated = true,
                    
                };
            }
        }
    }
}
