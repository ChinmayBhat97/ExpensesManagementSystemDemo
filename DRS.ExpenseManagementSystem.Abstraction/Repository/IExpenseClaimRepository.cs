using DRS.ExpenseManagementSystem.Abstraction.Models;
using DRS.ExpenseManagementSystem.Abstraction.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DRS.ExpenseManagementSystem.Abstraction.Repository
{
    public interface IExpenseClaimRepository : IBaseRepository<ExpenseClaim>
    {
        public Task<List<ExpenseClaim>> GetByClaimPeriods(DateTime periodStartDate, DateTime periodEndDate, int empId);
        public Task<List<ExpenseClaim>> GetByClaimStatus(int claimStatus);
        public Task<ExpenseClaim> GetByClaimId(int Id);
        public Task<List<ExpenseClaim>> GetByEmpId(int empId);
        public Task<List<ExpenseClaim>> GetAllDetails();

        //public Task<List<ExpenseClaim>> GetByClaimedDate(DateTime claimedDate);

        //Manager
        public Task<List<ExpenseClaim>> GetByClaimantEmpId(int empId);
        public Task<List<ExpenseClaim>> GetByClaimStatusManager(int claimStatus);
        public Task<List<ExpenseClaim>> GetByClaimIdManager(int claimId);

        //Finance Manager
        //public Task<List<ExpenseClaim>> GetByClaimantEmpId(int empId);
        //public Task<List<ExpenseClaim>> GetByClaimStatusManager(int claimStatus);
        //public Task<ExpenseClaim> GetByClaimIdManager(int Id);
    }
}
