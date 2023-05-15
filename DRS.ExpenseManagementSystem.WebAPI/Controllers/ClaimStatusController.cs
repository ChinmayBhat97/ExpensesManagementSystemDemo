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


    }
}
