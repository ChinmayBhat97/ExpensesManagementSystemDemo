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
        public Task<List<ExpensesClaim>> GetByEmpId(int empId);
        public Task<ClaimStatus> GetByClaimState(int claimState);

        public Task<ClaimStatus> GetByClaimPeriods(DateTime periodStartDate, DateTime periodEndDate);

        public Task<ClaimStatus> GetByClaimedDate(DateTime claimedDate);
    }
}
