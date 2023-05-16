using DRS.ExpenseManagementSystem.Abstraction.Models;
using DRS.ExpenseManagementSystem.Abstraction.Services;
using DRS.ExpenseManagementSystem.Business.Services;
using Microsoft.AspNetCore.Mvc;

namespace DRS.ExpenseManagementSystem.WebAPI.Controllers
{
    public class UserTableController : ControllerBase
    {
        private IUserService userTableService;
        public UserTableController(IUserService _userTableService)
        {
            this.userTableService = _userTableService;
        }
        // GET: api/<UserTableController>
        [HttpGet("{id}")]
        public async Task<UserTable> Get(int id)
        {
            return await userTableService.GetByIdAsync(id);
        }

        //POST api/<EmployeeController>
        [HttpPost]
        public async Task Post(UserTable userTable)
        {
            await userTableService.AddAsync(userTable);
        }

        // PUT api/<AssetController>/5
        [HttpPut("{id}")]
        public async Task Put(int id, UserTable userTableToUpdate)
        {
            await userTableService.UpdateAsync(userTableToUpdate);
        }
    }
}
