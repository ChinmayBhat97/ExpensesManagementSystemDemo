
using DRS.ExpenseManagementSystem.Abstraction.Models;
using DRS.ExpenseManagementSystem.Abstraction.Services;
using DRS.ExpenseManagementSystem.Business.Services;
using Microsoft.AspNetCore.Mvc;

namespace DRS.ExpenseManagementSystem.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private IUserService userTableService;

        public UserController(IUserService _userTableService)
        {
            this.userTableService = _userTableService;
        }

        // GET: api/<ExpenseClaimController>
        [HttpGet]
        public async Task<List<User>> GetAll()
        {
            return await userTableService.GetAllAsync();
        }


        // GET api/user/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<User>> Get(int id)
        //{
        //    var user = await userTableService.GetByIdAsync(id);
        //    if (user == null)
        //    {
        //        return NotFound();
        //    }
        //    return user;
        //}

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public async Task<User> GetByUserId(int id)
        {
            return await userTableService.GetByIdAsync(id);
        }

        // POST api/user
        //[HttpPost]
        //public async Task<ActionResult> Post(User userTable)
        //{
        //    await userTableService.AddAsync(userTable);
        //    return CreatedAtAction(nameof(Get), new { id = userTable.Id }, userTable);
        //}

        // POST api/<UserController>
        [HttpPost]
        public async Task Post(User user)
        {
            await userTableService.AddAsync(user);
        }

        // PUT api/user/5
        //[HttpPut("{id}")]
        //public async Task<ActionResult> Put(int id, User userTableToUpdate)
        //{
        //    if (id != userTableToUpdate.Id)
        //    {
        //        return BadRequest();
        //    }

        //    await userTableService.UpdateAsync(userTableToUpdate);

        //    return NoContent();
        //}

        // PUT api/<User Controller>/5
        [HttpPut("{id}")]
        public async Task Put(User user)
        {
            await userTableService.UpdateAsync(user);
        }
    }
}

