using DRS.ExpenseManagementSystem.Abstraction.Models;
using DRS.ExpenseManagementSystem.Abstraction.Services;
using DRS.ExpenseManagementSystem.Business.Services;
using Microsoft.AspNetCore.Mvc;

namespace DRS.ExpenseManagementSystem.WebAPI.Controllers
{
    public class IndividualExpenditureController : Controller
    {
        private IIndividualExpenditureServices individualExpenditureServices;

        public IndividualExpenditureController(IIndividualExpenditureServices _individualExpenditureServices)
        {
            this.individualExpenditureServices = _individualExpenditureServices;
        }

        // GET api/<ImageController>/5
        [HttpGet("{id}")]
        public async Task<IndividualExpenditure> GetById(int id)
        {
            return await individualExpenditureServices.GetByIdAsync(id);
        }

        // GET api/<ImageController>/5
        [HttpGet("{date}")]
        public async Task<List<IndividualExpenditure>> GetByPurchaseDate(DateTime date)
        {
            return await individualExpenditureServices.GetByBillingDate(date);
        }

        // GET api/<ImageController>/5
        [HttpGet("{category}")]
        public async Task<List<IndividualExpenditure>> GetByCategoryType(string category)
        {
            return await individualExpenditureServices.GetByCategory(category);
        }

        // GET api/<ImageController>/5
        [HttpGet("{category}")]
        public async Task<List<IndividualExpenditure>> GetByCategoryExpense(int id)
        {
            return await individualExpenditureServices.GetByExpenseCategory(id);
        }


        // POST api/<Individual Expenditure Controller>
        [HttpPost]
        public async Task Post(IndividualExpenditure individualExpenditure)
        {
             await individualExpenditureServices.AddAsync(individualExpenditure);
        }

        // PUT api/<Individual Expenditure Controller>/5
        [HttpPut("{id}")]
        public async Task Put(IndividualExpenditure individualExpenditure)
        {
            await individualExpenditureServices.UpdateAsync(individualExpenditure);
        }



    }

}
