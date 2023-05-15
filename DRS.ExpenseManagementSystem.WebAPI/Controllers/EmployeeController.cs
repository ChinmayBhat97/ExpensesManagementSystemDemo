using DRS.ExpenseManagementSystem.Abstraction.Models;
using DRS.ExpenseManagementSystem.Abstraction.Services;
using DRS.ExpenseManagementSystem.Business.Services;
using Microsoft.AspNetCore.Mvc;

namespace DRS.ExpenseManagementSystem.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private IEmployeeService employeeService;
        public EmployeeController(IEmployeeService _employeeService)
        {
            this.employeeService = _employeeService;
        }

        // GET: api/<AssetController>
        [HttpGet("{id}")]
        public async Task<Employee> Get(int id)
        {
            return await employeeService.GetByIdAsync(id);
        }

        // GET api/<AssetController>/1
        [HttpGet]
        public async Task<Employee> Get1(int id)
        {
            return await employeeService.GetByEmpId(id);
        }

        //GET api/<AssetController>/2
        [HttpGet]
        public async Task<List<Employee>> Get2(string firstName)
        {
            return await employeeService.GetByEmpFirstName(firstName);
        }
    }

}
