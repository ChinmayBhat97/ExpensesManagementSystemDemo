using DRS.ExpenseManagementSystem.Abstraction.Models;
using DRS.ExpenseManagementSystem.Abstraction.Repository;
using DRS.ExpenseManagementSystem.Abstraction.ViewModels;
using DRS.ExpenseManagementSystem.WebAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DRS.ExpenseManagementSystem.Repository.Repository
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        private ExpensesManagementSystem_UpdatedContext _dbContext;
        public UserRepository(ExpensesManagementSystem_UpdatedContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public UserViewModel GetByUsernameAndPassword(string userName, string password)
        {
            var userTable = _dbContext.Users.Include("Employee")
                .Where(n => n.EmployeeCode == userName && n.Password == password).ToList()
                .Select(x => new UserViewModel
                {
                    EmployeeCode = x.EmployeeCode,
                    Password = x.Password
                }).FirstOrDefault();

            return userTable;
        }
    }
}
