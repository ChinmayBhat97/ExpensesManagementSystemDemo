using DRS.ExpenseManagementSystem.Abstraction.Models;
using DRS.ExpenseManagementSystem.Abstraction.Repository;
using DRS.ExpenseManagementSystem.Abstraction.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DRS.ExpenseManagementSystem.Business.Services
{
    public class ExpenseCategoryService : BaseService<ExpenseCategory> , IExpenseCategoryServices
    {
        private IExpenseCategoryRepository expensesCategoryRepository;

        public ExpenseCategoryService(IExpenseCategoryRepository repository) : base (repository) 
        {
                this.expensesCategoryRepository = repository;
        }
    }
}
