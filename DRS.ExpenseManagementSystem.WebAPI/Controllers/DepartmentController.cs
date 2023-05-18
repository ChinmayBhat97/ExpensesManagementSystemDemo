using DRS.ExpenseManagementSystem.Abstraction.Models;
using DRS.ExpenseManagementSystem.Abstraction.Repository;
using DRS.ExpenseManagementSystem.Abstraction.Services;
using Microsoft.AspNetCore.Mvc;

namespace DRS.ExpenseManagementSystem.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : Controller
    {
        private IDepartmentService departmentService;

        public DepartmentController(IDepartmentService _departmentService)
        {
            this.departmentService = _departmentService;
        }

        // GET api/<Department Controller>/5
        [HttpGet]
        public async Task<List<Department>> Get()
        {
            return await departmentService.GetAllAsync();
        }


        // POST api/<Department Controller>
        [HttpPost]
        public async Task Post(Department department)
        {
            await departmentService.AddAsync(department);
        }

        // PUT api/<Department Controller>/5
        [HttpPut("{id}")]
        public async Task Put(Department department)
        {
            await departmentService.UpdateAsync(department);
        }


    }
}
