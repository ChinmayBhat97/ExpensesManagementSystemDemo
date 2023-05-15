using DRS.ExpenseManagementSystem.Abstraction.Models;
using DRS.ExpenseManagementSystem.Abstraction.Services;
using DRS.ExpenseManagementSystem.Business.Services;
using Microsoft.AspNetCore.Mvc;

namespace DRS.ExpenseManagementSystem.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpensesCategoryController : ControllerBase
    {
        private IExpensesCategoryServices expensesCategoryServices;

        public ExpensesCategoryController(IExpensesCategoryServices _expensesCategoryServices)
        {
            this.expensesCategoryServices= _expensesCategoryServices;
        }

        // GET api/<ExpensesCategory Controller>/5
        [HttpGet]
        public async Task<List<ExpensesCategory>> Get()
        {
            return await expensesCategoryServices.GetAllAsync();
        }


        // POST api/<ExpensesCategory Controller>
        [HttpPost]
        public async Task Post(ExpensesCategory expensesCategory)
        {
            await expensesCategoryServices.AddAsync(expensesCategory);
        }

        // PUT api/<ExpensesCategory Controller>/5
        [HttpPut("{id}")]
        public async Task Put(ExpensesCategory expensesCategory)
        {
            await expensesCategoryServices.UpdateAsync(expensesCategory);
        }

    }
}
