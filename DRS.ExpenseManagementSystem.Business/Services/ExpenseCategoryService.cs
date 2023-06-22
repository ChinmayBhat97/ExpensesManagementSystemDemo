using DRS.ExpenseManagementSystem.Abstraction.Models;
using DRS.ExpenseManagementSystem.Abstraction.Repository;
using DRS.ExpenseManagementSystem.Abstraction.Services;
using DRS.ExpenseManagementSystem.Abstraction.ViewModels;
using DRS.ExpenseManagementSystem.Repository.Repository;
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

        public async Task<bool> DeleteById(int Id)
        {
            return await expensesCategoryRepository.DeleteById(Id);
        }

        public  ExpenseCategoryViewModel GetByTitle(string title)
        {
            var checkByTitle = expensesCategoryRepository.GetByTitleName(title);
            if (checkByTitle == null)
            {
                return new ExpenseCategoryViewModel
                {
                    IsAuthenticated = false,
                    ErrorMessage = "ERR001"
                };
            }
            else
            {
                return new ExpenseCategoryViewModel
                {
                    IsAuthenticated = true,
                    ExpenseCheckViewModel = checkByTitle
                };
            }
        }
    }
}
