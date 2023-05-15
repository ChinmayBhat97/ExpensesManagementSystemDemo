using DRS.ExpenseManagementSystem.Abstraction.Models;
using DRS.ExpenseManagementSystem.Abstraction.Services;
using DRS.ExpenseManagementSystem.Business.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using NuGet.ContentModel;

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

        // GET api/<EmployeeController>/1
        [HttpGet]
        public async Task<List<Employee>> Get1(int id)
        {
            return await employeeService.GetByDeptId(id);
        }

        //GET api/<EmployeeController>/2
        [HttpGet]
        public async Task<List<Employee>> Get2(string firstName)
        {
            return await employeeService.GetByEmpFirstName(firstName);
        }

        //POST api/<EmployeeController>
        [HttpPost]
        public async Task Post(Employee employee)
        {
            await employeeService.AddAsync(employee);
        }

        // PUT api/<AssetController>/5
        [HttpPut("{id}")]
        public async Task Put(int id, Employee employeeToUpdate)
        {
            await employeeService.UpdateAsync(employeeToUpdate);
        }
    }

}
