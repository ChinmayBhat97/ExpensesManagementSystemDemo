using DRS.ExpenseManagementSystem.Abstraction.Models;
using DRS.ExpenseManagementSystem.Abstraction.Services;
using Microsoft.AspNetCore.Mvc;

namespace DRS.ExpenseManagementSystem.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpenseClaimController : ControllerBase
    {
        private IExpenseClaimServices expensesClaimServices;

        public ExpenseClaimController(IExpenseClaimServices _expensesClaimServices)
        {
            this.expensesClaimServices = _expensesClaimServices;
        }

        // GET api/<ExpenseClaimController>/5
        [HttpGet("{id}")]
        public async Task<ExpensesClaim> GetByEmployeeID(int id)
        {
            return await expensesClaimServices.GetByEmpId(id); 
        }


        // GET api/<ExpenseClaimController>/5
        [HttpGet("{id}")]
        public async Task<List<ExpensesClaim>> GetByStatus(int id)
        {
            return await expensesClaimServices.GetByClaimState(id);
        }


        // GET api/<ExpenseClaimController>/
        [HttpGet("{dateStart,dateEnd}")]
        public async Task<List<ExpensesClaim>> GetByPeriod(DateTime dateStart, DateTime dateEnd)
        {
            return await expensesClaimServices.GetByClaimPeriods(dateStart,dateEnd);
        }

        // GET api/<ExpenseClaimController>/
        [HttpGet("{dateStart,dateEnd}")]
        public async Task<List<ExpensesClaim>> GetByDate(DateTime date)
        {
            return await expensesClaimServices.GetByClaimedDate(date);
        }

        // POST api/<ExpenseClaimController>
        [HttpPost]
        public async Task Post(ExpensesClaim expensesClaim)
        {
            await expensesClaimServices.AddAsync(expensesClaim);
        }

        // PUT api/<ExpenseClaimController>/5
        [HttpPut("{id}")]
        public async Task Put(ExpensesClaim expensesClaim)
        {
            await expensesClaimServices.UpdateAsync(expensesClaim);
        }

    }
}
