using DRS.ExpenseManagementSystem.Abstraction.Models;
using DRS.ExpenseManagementSystem.Abstraction.Repository;
using DRS.ExpenseManagementSystem.Abstraction.Services;
using DRS.ExpenseManagementSystem.Abstraction.ViewModels;
using DRS.ExpenseManagementSystem.Repository.Repository;
using System;
using System.Collections.Generic;
using System.Data;
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

        public async Task<List<ExpenseClaim>> GetByEmpId(int EmpId)
        {
            return await repository.GetByEmpId(EmpId);
        }

        public async Task<List<ExpenseClaim>> GetByClaimStatus(int claimStatus)
        {
            return await repository.GetByClaimStatus(claimStatus);
        }

        public async Task<List<ExpenseClaim>> GetByClaimPeriods(DateTime periodStartDate, DateTime periodEndDate, int empId)
        {
            return await repository.GetByClaimPeriods(periodStartDate, periodEndDate, empId);
        }

        public async Task<ExpenseClaim> GetByClaimId(int Id)
        {
            return await repository.GetByClaimId(Id);
        }

        //  Manager
        public async Task<List<ExpenseClaim>> GetByClaimantEmpId(int empId)
        {
            return await repository.GetByClaimantEmpId(empId);
        }

        public async Task<List<ExpenseClaim>> GetByClaimStatusManager(int claimStatus)
        {
            return await repository.GetByClaimStatusManager(claimStatus);
        }

        public async Task<List<ExpenseClaim>> GetByClaimIdManager(int claimId)
        {
            return await repository.GetByClaimIdManager(claimId);
        }
        public async Task<List<ExpenseClaim>> GetAllDetails()
        {
            return await repository.GetAllDetails();
        }
        public async Task<List<ExpenseClaim>> GetDetailsByEmpId(int empId)
        {
            return await repository.GetDetailsByEmpId(empId);
        }

        public async Task<List<ExpenseClaim>> GetByManagerId(int EmpId)
        {
            return await repository.GetByManagerId(EmpId);
        }
    }
}
