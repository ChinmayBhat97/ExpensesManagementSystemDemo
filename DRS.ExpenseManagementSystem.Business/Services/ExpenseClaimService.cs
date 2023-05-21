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
    public class ExpenseClaimService : BaseService<ExpenseClaim>, IExpenseClaimServices
    {
        private IExpenseClaimRepository repository;

        public ExpenseClaimService(IExpenseClaimRepository repository) : base(repository) 
        {
            this.repository = repository;
        }

        public async Task<List<ExpenseClaim>> GetByEmpId(int empId)
        {
            return await repository.GetByEmpId(empId);
        }

        public async Task<List<ExpenseClaim>> GetByClaimState(int claimState)
        {
            return await repository.GetByClaimState(claimState);
        }

        public async Task<List<ExpenseClaim>> GetByClaimPeriods(DateTime periodStartDate, DateTime periodEndDate)
        {
            return await repository.GetByClaimPeriods(periodStartDate, periodEndDate);
        }

        public async Task<List<ExpenseClaim>> GetByClaimedDate(DateTime claimedDate)
        {
            return await repository.GetByClaimedDate(claimedDate);
        }

        public Task<ExpenseClaim> GetById(int Id)
        {
           return repository.GetById(Id);
        }

       
    }
}
