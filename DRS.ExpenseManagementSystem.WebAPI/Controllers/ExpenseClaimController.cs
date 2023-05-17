using DRS.ExpenseManagementSystem.Abstraction.Models;
using DRS.ExpenseManagementSystem.Abstraction.Services;
using Microsoft.AspNetCore.Mvc;
using NuGet.ContentModel;

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
        public async Task<ExpenseClaim> Get(int id)
        {
            return await expensesClaimServices.GetByIdAsync(id);
        }

        // GET api/<ExpenseClaimController>/5
        [HttpGet("{employeeId}")]
        public async Task<List<ExpenseClaim>> GetByEmployeeID(int employeeId)
        {
            return await expensesClaimServices.GetByEmpId(employeeId); 
        }


        // GET api/<ExpenseClaimController>/5
        [HttpGet("{statusId}")]
        public async Task<List<ExpenseClaim>> GetByStatus(int statusId)
        {
            return await expensesClaimServices.GetByClaimState(statusId);
        }


        // GET api/<ExpenseClaimController>/
        [HttpGet("{dateStart,dateEnd}")]
        public async Task<List<ExpenseClaim>> GetByPeriod(DateTime dateStart, DateTime dateEnd)
        {
            return await expensesClaimServices.GetByClaimPeriods(dateStart,dateEnd);
        }

        // GET api/<ExpenseClaimController>/
        [HttpGet("{dateStart,dateEnd}")]
        public async Task<List<ExpenseClaim>> GetByDate(DateTime date)
        {
            return await expensesClaimServices.GetByClaimedDate(date);
        }

        // POST api/<ExpenseClaimController>
        [HttpPost]
        public async Task Post(ExpenseClaim expensesClaim)
        {
            await expensesClaimServices.AddAsync(expensesClaim);
        }

        // PUT api/<ExpenseClaimController>/5
        [HttpPut("{id}")]
        public async Task Put(ExpenseClaim expensesClaim)
        {
            await expensesClaimServices.UpdateAsync(expensesClaim);
        }


       

    }
}
