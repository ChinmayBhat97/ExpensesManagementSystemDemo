using DRS.ExpenseManagementSystem.Abstraction.Models;
using DRS.ExpenseManagementSystem.Abstraction.Repository;
using DRS.ExpenseManagementSystem.Abstraction.ViewModels;
using DRS.ExpenseManagementSystem.Repository.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DRS.ExpenseManagementSystem.Repository.Repository
{
    public class UserTableRepository : BaseRepository<UserTable>, IUserTableRepository
    {
        private ExpenseManagementSystemContext _dbContext;
        public UserTableRepository(ExpenseManagementSystemContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public UserViewModel GetByUsernameAndPassword(string userName, string password)
        {
            var userTable = _dbContext.UserTables.Include("Employee")
                .Where(n => n.EmployeeId == userName && n.Password == password).ToList()
                .Select(x => new UserViewModel
                {
                    EmployeeId = x.EmployeeId,
                    Password = x.Password
                }).FirstOrDefault();

            return userTable;
        }
    }
}
