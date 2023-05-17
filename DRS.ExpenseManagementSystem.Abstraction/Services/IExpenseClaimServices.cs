using DRS.ExpenseManagementSystem.Abstraction.Models;
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
        public Task<List<ExpenseClaim>> GetByClaimState(int claimState);

        public Task<List<ExpenseClaim>> GetByClaimPeriods(DateTime periodStartDate, DateTime periodEndDate);

        public Task<List<ExpenseClaim>> GetByClaimedDate(DateTime claimedDate);

       

    }
}
