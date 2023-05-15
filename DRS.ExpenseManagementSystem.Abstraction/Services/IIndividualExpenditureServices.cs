using DRS.ExpenseManagementSystem.Abstraction.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DRS.ExpenseManagementSystem.Abstraction.Services
{
    public interface IIndividualExpenditureServices : IBaseService<IndividualExpenditure>
    {
        public Task<List<IndividualExpenditure>> GetByBillingDate(DateTime billingDate);

        public Task<List<IndividualExpenditure>> GetByCategory(string category);

        public Task<List<IndividualExpenditure>> GetByExpenseCategory(int Id);
    }
}
