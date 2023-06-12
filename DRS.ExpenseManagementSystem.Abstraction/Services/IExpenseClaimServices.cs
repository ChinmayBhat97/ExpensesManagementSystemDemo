using DRS.ExpenseManagementSystem.Abstraction.Models;
using DRS.ExpenseManagementSystem.Abstraction.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DRS.ExpenseManagementSystem.Abstraction.Services
{
    public interface IExpenseClaimServices : IBaseService<ExpenseClaim>
    {
        public Task<List<ExpenseClaim>> GetByEmpId(int empId);
        public Task<List<ExpenseClaim>> GetByClaimStatus(int claimStatus);

        public Task<ExpenseClaim> GetByClaimId(int claimId);

        public Task<List<ExpenseClaim>> GetByClaimPeriods(DateTime periodStartDate, DateTime periodEndDate, int empId);

        public Task<List<ExpenseClaim>> GetAllDetails();
        public Task<List<ExpenseClaim>> GetDetailsByEmpId(int empId);

        //public Task<List<ExpenseClaim>> GetByClaimedDate(DateTime claimedDate);

        //Manager
        public Task<List<ExpenseClaim>> GetByClaimantEmpId(int empId);
        public Task<List<ExpenseClaim>> GetByClaimStatusManager(int claimStatus);
        public Task<List<ExpenseClaim>> GetByClaimIdManager(int Id);


        public Task<List<ExpenseClaim>> GetByManagerId(int EmpId);
    }
}
