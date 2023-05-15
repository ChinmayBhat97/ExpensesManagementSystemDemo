using DRS.ExpenseManagementSystem.Abstraction.Models;
using DRS.ExpenseManagementSystem.Abstraction.Repository;
using DRS.ExpenseManagementSystem.Abstraction.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DRS.ExpenseManagementSystem.Business.Services
{
    public class ExpensesClaimService : BaseService<ExpensesClaim>, IExpensesClaimServices
    {
        private IExpensesClaimRepository repository;

        public ExpensesClaimService(IExpensesClaimRepository repository) : base(repository) 
        {
            this.repository = repository;
        }

        public async Task<ExpensesClaim> GetByEmpId(int empId)
        {
            return await repository.GetByEmpId(empId);
        }

        public async Task<List<ExpensesClaim>> GetByClaimState(int claimState)
        {
            return await repository.GetByClaimState(claimState);
        }

        public async Task<List<ExpensesClaim>> GetByClaimPeriods(DateTime periodStartDate, DateTime periodEndDate)
        {
            return await repository.GetByClaimPeriods(periodStartDate, periodEndDate);
        }

        public async Task<List<ExpensesClaim>> GetByClaimedDate(DateTime claimedDate)
        {
            return await repository.GetByClaimedDate(claimedDate);
        }

     
    }
}
