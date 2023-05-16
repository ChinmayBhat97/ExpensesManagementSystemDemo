using DRS.ExpenseManagementSystem.Abstraction.Models;
using DRS.ExpenseManagementSystem.Abstraction.Repository;
using DRS.ExpenseManagementSystem.WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DRS.ExpenseManagementSystem.Repository.Repository
{
    public class ExpenseCategoryRepository : BaseRepository<ExpenseCategory>, IExpenseCategoryRepository
    {
        private ExpensesManagementSystem_UpdatedContext _dbContext;

        public ExpenseCategoryRepository(ExpensesManagementSystem_UpdatedContext dbcontext) : base(dbcontext)
        {
            this._dbContext = dbcontext;
        }


    }
}
