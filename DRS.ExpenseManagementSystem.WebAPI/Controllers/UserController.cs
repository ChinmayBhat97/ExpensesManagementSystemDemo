//using DRS.ExpenseManagementSystem.Abstraction.Models;
//using DRS.ExpenseManagementSystem.Abstraction.Services;
//using DRS.ExpenseManagementSystem.Business.Services;
//using Microsoft.AspNetCore.Mvc;

//namespace DRS.ExpenseManagementSystem.WebAPI.Controllers
//{
//    public class UserController : ControllerBase
//    {
//        private IUserService userTableService;
//        public UserController(IUserService _userTableService)
//        {
//            this.userTableService = _userTableService;
//        }
//        // GET: api/<UserTableController>
//        [HttpGet("{userid}")]
//        public async Task<User> Get(int id)
//        {
//            return await userTableService.GetByIdAsync(id);
//        }

//        //POST api/<EmployeeController>
//        [HttpPost]
//        public async Task Post(User userTable)
//        {
//            await userTableService.AddAsync(userTable);
//        }

//        // PUT api/<AssetController>/5
//        [HttpPut("{userid}")]
//        public async Task Put(int id, User userTableToUpdate)
//        {
//            await userTableService.UpdateAsync(userTableToUpdate);
//        }
//    }
//}

using DRS.ExpenseManagementSystem.Abstraction.Models;
using DRS.ExpenseManagementSystem.Abstraction.Services;
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

        // GET api/user/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> Get(int id)
        {
            var user = await userTableService.GetByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return user;
        }

        // POST api/user
        [HttpPost]
        public async Task<ActionResult> Post(User userTable)
        {
            await userTableService.AddAsync(userTable);
            return CreatedAtAction(nameof(Get), new { id = userTable.Id }, userTable);
        }

        // PUT api/user/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, User userTableToUpdate)
        {
            if (id != userTableToUpdate.Id)
            {
                return BadRequest();
            }

            await userTableService.UpdateAsync(userTableToUpdate);

            return NoContent();
        }
    }
}

