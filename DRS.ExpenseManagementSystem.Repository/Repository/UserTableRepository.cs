using DRS.ExpenseManagementSystem.Abstraction.Models;
using DRS.ExpenseManagementSystem.Abstraction.Repository;
using DRS.ExpenseManagementSystem.Repository.DatabaseContext;
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
    }
}
