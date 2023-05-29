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
        [HttpGet("{claimId}")]
        public async Task<List<IndividualExpenditure>> GetByClaimId(int claimId)
        {
            return await individualExpenditureServices.GetByClaimID(claimId);

        }



        // POST api/<ExpenseClaimController>
        [HttpPost]
        public async Task Post(List<IndividualExpenditure> individualExpenditures)
        {
            await individualExpenditureServices.SaveIndividualExpenseDb(individualExpenditures);
        }


        // PUT api/<ExpenseClaimController>/5
        [HttpPut]
        public async Task Put(List<IndividualExpenditure> individualExpenditures)
        {
            await individualExpenditureServices.UpdateIndividualExpenses(individualExpenditures);
        }

    }
}

