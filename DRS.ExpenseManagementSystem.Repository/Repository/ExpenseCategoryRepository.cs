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
    public class ExpenseCategoryRepository : BaseRepository<ExpenseCategory>, IExpenseCategoryRepository
    {
        private ExpensesManagementSystem_UpdatedContext _dbContext;

        public ExpenseCategoryRepository(ExpensesManagementSystem_UpdatedContext dbcontext) : base(dbcontext)
        {
            this._dbContext = dbcontext;
        }

        public   ExpenseCheckViewModel GetByTitleName(string title)
        {
            // return  _dbContext.ExpenseCategories..Where(k => k.Name==title);
            var checkCategory = _dbContext.ExpenseCategories
                .Where(n => n.Name == title).ToList()
                .Select(x => new ExpenseCheckViewModel
                {
                   Name=x.Name,
                }).FirstOrDefault();

            return checkCategory;

            // return await _dbContext.ExpenseClaims.AsQueryable().Where(k => k.Status== claimStatus).ToListAsync();
        }

        public async Task<bool> DeleteById(int Id)
        {
            var category = await _dbContext.ExpenseCategories.FindAsync(Id);

            if (category == null)
            {
                return false;
            }

            category.IsDelete = 1;

            await _dbContext.SaveChangesAsync();

            return true;
        }
    }
}
