using DRS.ExpenseManagementSystem.Abstraction.Models;
using DRS.ExpenseManagementSystem.Abstraction.Services;
using DRS.ExpenseManagementSystem.Business.Services;
using Microsoft.AspNetCore.Mvc;

namespace DRS.ExpenseManagementSystem.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class ClaimStatusController : ControllerBase
    {
        private IClaimStatusService claimStatusService;


        public ClaimStatusController(IClaimStatusService _claimStatusService)
        {
            this.claimStatusService = _claimStatusService;
        }

        // GET api/<Claim status Controller>/5
        [HttpGet]
        public async Task<List<ClaimStatus>> Get()
        {
            return await claimStatusService.GetAllAsync();
        }


        // POST api/<Claim status Controller>
        [HttpPost]
        public async Task Post(ClaimStatus claimStatus)
        {
            await claimStatusService.AddAsync(claimStatus);
        }

        // PUT api/<Claim status Controller>/5
        [HttpPut("{id}")]
        public async Task Put(ClaimStatus claimStatus)
        {
            await claimStatusService.UpdateAsync(claimStatus);
        }
    }
}
