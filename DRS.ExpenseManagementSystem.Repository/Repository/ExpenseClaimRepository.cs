using DRS.ExpenseManagementSystem.Abstraction.Models;
using DRS.ExpenseManagementSystem.Abstraction.Repository;
using DRS.ExpenseManagementSystem.Abstraction.ViewModels;
using DRS.ExpenseManagementSystem.Repository.Repository;
using DRS.ExpenseManagementSystem.WebAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace DRS.ExpenseManagementSystem.Repository
{
    public class ExpenseClaimRepository : BaseRepository<ExpenseClaim>, IExpenseClaimRepository
    {
        private ExpensesManagementSystem_UpdatedContext _dbContext;

        public ExpenseClaimRepository(ExpensesManagementSystem_UpdatedContext dbcontext) : base(dbcontext)
        {
            this._dbContext = dbcontext;
        }


        //public async Task<List<ExpenseClaim>> GetByClaimedDate(DateTime claimedDate)
        //{
        //   return await _dbContext.ExpenseClaims.AsQueryable().Where(m =>m.ClaimRequestDate == claimedDate).ToListAsync();
        //}

        public Task<List<ExpenseClaim>> GetByClaimPeriods(DateTime periodStartDate, DateTime periodEndDate, int empId)
        {
            return _dbContext.ExpenseClaims.AsQueryable().Where(n => n.StartDate == periodStartDate && n.EndDate == periodEndDate && n.EmpId == empId).ToListAsync();
        }

        public async Task<List<ExpenseClaim>> GetByClaimStatus(int claimStatus)
        {
            return await _dbContext.ExpenseClaims.AsQueryable().Where(k => k.Status == claimStatus).ToListAsync();
        }

        public async Task<List<ExpenseClaim>> GetByEmpId(int empId)
        {

            return await _dbContext.ExpenseClaims.AsQueryable().Where(x => x.EmpId == empId).ToListAsync();

        }

        public async Task<ExpenseClaim> GetByClaimId(int claimId)
        {
            return await _dbContext.ExpenseClaims.AsQueryable().Where(x => x.Id == claimId).SingleOrDefaultAsync();

        }

        public async Task<List<ExpenseClaim>> GetByClaimantEmpId(int empId)
        {
            return await _dbContext.ExpenseClaims.AsQueryable().Where(x => x.EmpId == empId).ToListAsync();
        }

        public async Task<List<ExpenseClaim>> GetByClaimStatusManager(int claimStatus)
        {
            return await _dbContext.ExpenseClaims.AsQueryable().Where(x => x.Status == claimStatus).ToListAsync();
        }

        public async Task<List<ExpenseClaim>> GetByClaimIdManager(int claimId)
        {
            return await _dbContext.ExpenseClaims.AsQueryable().Where(x => x.Id == claimId).ToListAsync();
        }

       
    }
}
