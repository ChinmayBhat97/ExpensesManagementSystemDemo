using DRS.ExpenseManagementSystem.Abstraction.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DRS.ExpenseManagementSystem.Abstraction.Repository
{
    public interface IIndividualExpenditureRepository : IBaseRepository<IndividualExpenditure>
    {
        public Task<IndividualExpenditure> GetByBillingDate(DateTime billingDate);

        public Task<IndividualExpenditure> GetByCategory(string category);

        public Task<IndividualExpenditure> GetByExpenseCategory(int Id);

    }
}
