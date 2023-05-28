using DRS.ExpenseManagementSystem.Abstraction.Models;
using DRS.ExpenseManagementSystem.Abstraction.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DRS.ExpenseManagementSystem.Abstraction.Repository
{
    public interface IExpenseCategoryRepository : IBaseRepository<ExpenseCategory>
    {
     //   ExpenseCategoryViewModel CheckTitle(string name);

        public ExpenseCheckViewModel GetByTitleName(string title);
    }
}
