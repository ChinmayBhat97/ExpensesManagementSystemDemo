using DRS.ExpenseManagementSystem.Abstraction.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DRS.ExpenseManagementSystem.Abstraction.Repository
{
    public interface IExpensesClaimRepository:IBaseRepository<ExpensesClaim>
    {
        public Task<ExpensesClaim> GetByEmpId(int empId);
        public Task<List<ExpensesClaim>> GetByClaimState(int claimState);

        public Task<List<ExpensesClaim>> GetByClaimPeriods(DateTime periodStartDate, DateTime periodEndDate);

        public Task<List<ExpensesClaim>> GetByClaimedDate(DateTime claimedDate);
    }
}
