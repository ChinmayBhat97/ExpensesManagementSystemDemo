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
    public class ExpensesCategoryService : BaseService<ExpensesCategory> , IExpensesCategoryServices
    {
        private IExpensesCategoryRepository expensesCategoryRepository;

        public ExpensesCategoryService(IExpensesCategoryRepository repository) : base (repository) 
        {
                this.expensesCategoryRepository = repository;
        }
    }
}
