using DRS.ExpenseManagementSystem.Abstraction.Models;
using DRS.ExpenseManagementSystem.Abstraction.Repository;
using DRS.ExpenseManagementSystem.Repository.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DRS.ExpenseManagementSystem.Repository.Repository
{
    public class IndividualExpenditureRepository : BaseRepository<IndividualExpenditure>, IIndividualExpenditureRepository
    {
        private ExpenseManagementSystemContext _dbContext;
        public IndividualExpenditureRepository(ExpenseManagementSystemContext dbcontext) : base (dbcontext) 
        {
            this._dbContext = dbcontext;
        }

        public async Task<List<IndividualExpenditure>> GetByBillingDate(DateTime billingDate)
        {
            return await _dbContext.IndividualExpenditures.AsQueryable().Where(a => a.BillingDate==billingDate).ToListAsync();
        }

        public async Task<List<IndividualExpenditure>> GetByCategory(string category)
        {
            return await _dbContext.IndividualExpenditures.AsQueryable().Where(b =>b.Category==category).ToListAsync();
        }

        public async Task<List<IndividualExpenditure>> GetByExpenseCategory(int Id)
        {
            return await _dbContext.IndividualExpenditures.AsQueryable().Where(c =>c.ExpenseCategoryId==Id).ToListAsync();
        }
    }
}
