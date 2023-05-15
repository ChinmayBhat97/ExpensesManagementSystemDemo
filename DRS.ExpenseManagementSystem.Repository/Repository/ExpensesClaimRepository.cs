using DRS.ExpenseManagementSystem.Abstraction.Models;
using DRS.ExpenseManagementSystem.Abstraction.Repository;
using DRS.ExpenseManagementSystem.Repository.DatabaseContext;
using DRS.ExpenseManagementSystem.Repository.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace DRS.ExpenseManagementSystem.Repository
{
    public class ExpensesClaimRepository : BaseRepository<ExpensesClaim> , IExpensesClaimRepository
    {
        private ExpenseManagementSystemContext _dbContext;

        public ExpensesClaimRepository(ExpenseManagementSystemContext dbcontext) : base(dbcontext) 
        {
            this._dbContext = dbcontext;
        }

        public async Task<List<ExpensesClaim>> GetByClaimedDate(DateTime claimedDate)
        {
           return await _dbContext.ExpensesClaims.AsQueryable().Where(m =>m.ClaimingDate == claimedDate).ToListAsync();
        }

        public Task<List<ExpensesClaim>> GetByClaimPeriods(DateTime periodStartDate, DateTime periodEndDate)
        {
            return  _dbContext.ExpensesClaims.AsQueryable().Where(n => n.PeriodStart == periodStartDate && n.PeriodEnd ==periodEndDate).ToListAsync();
        }

        public async Task<List<ExpensesClaim>> GetByClaimState(int claimState)
        {
            return await _dbContext.ExpensesClaims.AsQueryable().Where(k =>k.StatusId== claimState).ToListAsync();
        }

        public async Task<ExpensesClaim> GetByEmpId(int empId)
        {
           
            return await _dbContext.ExpensesClaims.AsQueryable().Where(x => x.EmpId == empId).FirstOrDefaultAsync();

        }
    }
}
