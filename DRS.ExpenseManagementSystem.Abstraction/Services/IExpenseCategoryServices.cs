using DRS.ExpenseManagementSystem.Abstraction.Models;
using DRS.ExpenseManagementSystem.Abstraction.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DRS.ExpenseManagementSystem.Abstraction.Services
{
    public interface IExpenseCategoryServices : IBaseService<ExpenseCategory>
    {
        ExpenseCategoryViewModel GetByTitle(string title);
        public Task<bool> DeleteById(int Id);
    }
}
