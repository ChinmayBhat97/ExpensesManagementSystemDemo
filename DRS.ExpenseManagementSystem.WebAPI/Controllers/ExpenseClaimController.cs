using DRS.ExpenseManagementSystem.Abstraction.Models;
using DRS.ExpenseManagementSystem.Abstraction.Services;
using DRS.ExpenseManagementSystem.Abstraction.ViewModels;
using DRS.ExpenseManagementSystem.Business.Services;
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

        //// GET: api/<ExpenseClaimController>
        //[HttpGet("EmpId")]
        //public async Task<List<ExpenseClaim>> GetAll(int EmpId)
        //{
        //    return await expensesClaimServices.GetByEmpId(EmpId);
        //}

        // GET: api/<ExpenseClaimController>/3
        [HttpGet]
        public async Task<List<ExpenseClaim>> GetAllDetails()
        {
            return await expensesClaimServices.GetAllDetails();
        }



        // GET api/<ExpenseClaimController>/5
        [HttpGet("{Id}")]
        public async Task<ExpenseClaim> GetByID(int Id)
        {
            return await expensesClaimServices.GetByIdAsync(Id);
        }


        // GET api/<ExpenseClaimController>/5
        [HttpGet("ClaimId/{claimId}")]
        public async Task<ExpenseClaim> Details(int claimId)
        {
            return await expensesClaimServices.GetByIdAsync(claimId);
        }

        // GET api/<ExpenseClaimController>/5
        [HttpGet("{claimId}")]
        public async Task<ExpenseClaim> GetByClaimId(int claimId)
        {

            return await expensesClaimServices.GetByClaimId(claimId);
        }

        // GET api/<ExpenseClaimController>/5
        //[HttpGet]
        //public async Task<List<ExpenseClaim>> GetByEmployeeID(int EmpId)
        //{
        //    return await expensesClaimServices.GetByEmpId(EmpId);
        //}


        // GET api/<ExpenseClaimController>/5
        [HttpGet("status/{statusId}")]
        public async Task<List<ExpenseClaim>> GetByStatus(int statusId)
        {
            return await expensesClaimServices.GetByClaimStatus(statusId);
        }


        // GET api/<ExpenseClaimController>/
        [HttpGet("dateStart,dateEnd/{startDate,dateEnd}")]
        public async Task<List<ExpenseClaim>> GetByPeriod(DateTime dateStart, DateTime dateEnd, int empId)
        {
            return await expensesClaimServices.GetByClaimPeriods(dateStart, dateEnd, empId);
        }

        // GET api/<ExpenseClaimController>/
        //[HttpGet("dateRequest/{requestDate}")]
        //public async Task<List<ExpenseClaim>> GetByDate(DateTime date)
        //{
        //    return await expensesClaimServices.GetByClaimedDate(date);
        //}

        // POST api/<ExpenseClaimController>
        [HttpPost]
        public async Task<int> Post(ExpenseClaim expensesClaim)
        {
            return await expensesClaimServices.AddAsync(expensesClaim);
        }

        // PUT api/<ExpenseClaimController>/5
        [HttpPut]
        public async Task Put(ExpenseClaim expensesClaim)
        {
            await expensesClaimServices.UpdateAsync(expensesClaim);
        }
    }
}
