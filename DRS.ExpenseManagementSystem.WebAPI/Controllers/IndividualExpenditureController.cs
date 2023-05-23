//using DRS.ExpenseManagementSystem.Abstraction.Models;
//using DRS.ExpenseManagementSystem.Abstraction.Services;
//using DRS.ExpenseManagementSystem.Business.Services;
//using Microsoft.AspNetCore.Mvc;

//namespace DRS.ExpenseManagementSystem.WebAPI.Controllers
//{
//    public class IndividualExpenditureController : Controller
//    {
//        private IIndividualExpenditureServices individualExpenditureServices;

//        public IndividualExpenditureController(IIndividualExpenditureServices _individualExpenditureServices)
//        {
//            this.individualExpenditureServices = _individualExpenditureServices;
//        }

//        // GET api/<ImageController>/5
//        [HttpGet("{id}")]
//        public async Task<IndividualExpenditure> GetById(int id)
//        {
//            return await individualExpenditureServices.GetByIdAsync(id);
//        }

//        // GET api/<ImageController>/5
//        [HttpGet("{date}")]
//        public async Task<List<IndividualExpenditure>> GetByPurchaseDate(DateTime date)
//        {
//            return await individualExpenditureServices.GetByBillingDate(date);
//        }

//        // GET api/<ImageController>/5
//        [HttpGet("{category}")]
//        public async Task<List<IndividualExpenditure>> GetByCategoryType(string category)
//        {
//            return await individualExpenditureServices.GetByCategory(category);
//        }

//        // GET api/<ImageController>/5
//        [HttpGet("{category}")]
//        public async Task<List<IndividualExpenditure>> GetByCategoryExpense(int id)
//        {
//            return await individualExpenditureServices.GetByExpenseCategory(id);
//        }


//        // POST api/<Individual Expenditure Controller>
//        [HttpPost]
//        public async Task Post(IndividualExpenditure individualExpenditure)
//        {
//             await individualExpenditureServices.AddAsync(individualExpenditure);
//        }

//        // PUT api/<Individual Expenditure Controller>/5
//        [HttpPut("{id}")]
//        public async Task Put(IndividualExpenditure individualExpenditure)
//        {
//            await individualExpenditureServices.UpdateAsync(individualExpenditure);
//        }



//    }

//}

using DRS.ExpenseManagementSystem.Abstraction.Models;
using DRS.ExpenseManagementSystem.Abstraction.Services;
using DRS.ExpenseManagementSystem.Business.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DRS.ExpenseManagementSystem.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class IndividualExpenditureController : ControllerBase
    {
        private IIndividualExpenditureServices individualExpenditureServices;

        public IndividualExpenditureController(IIndividualExpenditureServices _individualExpenditureServices)
        {
            this.individualExpenditureServices = _individualExpenditureServices;
        }

        // GET api/individualexpenditure/5
        //[HttpGet("{id}")]
        //public async Task<List<IndividualExpenditure>> GetByClaimId(int claimId)
        //{
        //    return await individualExpenditureServices.GetByClaimId(claimId);
            
        //}

        // GET api/individualexpenditure/purchasedate?date=2023-05-17
        //[HttpGet("purchasedate")]
        //public async Task<ActionResult<List<IndividualExpenditure>>> GetByPurchaseDate(DateTime date)
        //{
        //    var expenditures = await individualExpenditureServices.GetByBillingDate(date);
        //    return expenditures;
        //}

        // GET api/individualexpenditure/category?type=categoryName
        //[HttpGet("category")]
        //public async Task<ActionResult<List<IndividualExpenditure>>> GetByCategoryType(string type)
        //{
        //    var expenditures = await individualExpenditureServices.GetByCategory(type);
        //    return expenditures;
        //}

        // GET api/individualexpenditure/expensecategory?id=5
        //[HttpGet("expensecategory")]
        //public async Task<ActionResult<List<IndividualExpenditure>>> GetByCategoryExpense(int id)
        //{
        //    return await individualExpenditureServices.GetByExpenseCategory(id);
            
        //}

        //// POST api/individualexpenditure
        //[HttpPost]
        //public async Task<ActionResult> Post(IndividualExpenditure individualExpenditure)
        //{
        //  return  await individualExpenditureServices.AddAsync(individualExpenditure);
        //    //return CreatedAtAction(nameof(GetById), new { id = individualExpenditure.Id }, individualExpenditure);
        //}

        // POST api/<ExpenseClaimController>
        [HttpPost]
        public async Task Post(IndividualExpenditure individualExpenditure)
        {
            await individualExpenditureServices.AddAsync(individualExpenditure);
        }


        // PUT api/<ExpenseClaimController>/5
        [HttpPut("{id}")]
        public async Task Put(IndividualExpenditure individualExpenditure)
        {
            await individualExpenditureServices.UpdateAsync(individualExpenditure);
        }

        //// PUT api/individualexpenditure/5
        //[HttpPut("{id}")]
        //public async Task<ActionResult> Put(int id, IndividualExpenditure individualExpenditure)
        //{
        //    if (id != individualExpenditure.Id)
        //    {
        //        return BadRequest();
        //    }

        //    await individualExpenditureServices.UpdateAsync(individualExpenditure);

        //    return NoContent();
        //}
    }
}

