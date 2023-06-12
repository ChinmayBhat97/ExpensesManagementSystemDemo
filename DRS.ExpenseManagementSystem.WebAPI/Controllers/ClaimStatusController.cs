using DRS.ExpenseManagementSystem.Abstraction.Models;
using DRS.ExpenseManagementSystem.Abstraction.Services;
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

        // GET api/<ClaimStatusController>/5
        [HttpGet("{id}")]
        public async Task<ClaimStatus> GetByClaimStatusId(int id)
        {
            return await claimStatusService.GetByIdAsync(id);
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

        [HttpPost("{role}")]
        public async Task<List<ClaimStatus>> StatusByRole(int role)
        {
            return await claimStatusService.StatusByRole(role);
        }
    }
}
