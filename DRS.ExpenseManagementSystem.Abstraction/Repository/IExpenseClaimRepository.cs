using DRS.ExpenseManagementSystem.Abstraction.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DRS.ExpenseManagementSystem.Abstraction.Repository
{
    public interface IExpenseClaimRepository:IBaseRepository<ExpenseClaim>
    {
      
        public Task<List<ExpenseClaim>> GetByClaimState(int claimState);

        // rename to GetByClaim ID
        public Task<ExpenseClaim> GetById(int Id);

            // Pass Emp ID 
        public Task<List<ExpenseClaim>> GetByClaimPeriods(DateTime periodStartDate, DateTime periodEndDate);

        // Pass Emp ID 
        public Task<List<ExpenseClaim>> GetByClaimedDate(DateTime claimedDate);


      


    }
}
