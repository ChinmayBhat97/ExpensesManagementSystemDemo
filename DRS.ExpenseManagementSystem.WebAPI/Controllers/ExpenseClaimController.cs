using DRS.ExpenseManagementSystem.Abstraction.Models;
using DRS.ExpenseManagementSystem.Abstraction.Services;
using Microsoft.AspNetCore.Mvc;

namespace DRS.ExpenseManagementSystem.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpenseClaimController : ControllerBase
    {
        private IExpensesClaimServices expensesClaimServices;

        public ExpenseClaimController(IExpensesClaimServices _expensesClaimServices)
        {
            this.expensesClaimServices = _expensesClaimServices;
        }

        // GET api/<ExpenseClaimController>/5
        [HttpGet("{id}")]
        public async Task<ExpensesClaim> GetByEmpID(int id)
        {
            return await expensesClaimServices.GetByEmpId(id); 
        }


        // GET api/<ExpenseClaimController>/5
        [HttpGet("{id}")]
        public async Task<List<ExpensesClaim>> GetByClaimState(int id)
        {
            return await expensesClaimServices.GetByClaimState(id);
        }


        // GET api/<ExpenseClaimController>/
        [HttpGet("{dateStart,dateEnd}")]
        public async Task<List<ExpensesClaim>> GetByClaimPeriods(DateTime dateStart, DateTime dateEnd)
        {
            return await expensesClaimServices.GetByClaimPeriods(dateStart,dateEnd);
        }

        // GET api/<ExpenseClaimController>/
        [HttpGet("{dateStart,dateEnd}")]
        public async Task<List<ExpensesClaim>> GetByClaimedDate(DateTime date)
        {
            return await expensesClaimServices.GetByClaimedDate(date);
        }


        // POST api/<AssetController>
        //[HttpPost]
        //public async Task Post(ExpensesClaim expensesClaim)
        //{
        //    await expensesClaimServices.AddNewClaim(expensesClaim);
        //}

        
    }
}
