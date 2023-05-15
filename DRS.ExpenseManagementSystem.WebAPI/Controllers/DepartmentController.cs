using DRS.ExpenseManagementSystem.Abstraction.Models;
using DRS.ExpenseManagementSystem.Abstraction.Repository;
using Microsoft.AspNetCore.Mvc;

namespace DRS.ExpenseManagementSystem.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : Controller
    {
        private IDepartmentRepository departmentRepository;

        public DepartmentController(IDepartmentRepository _departmentRepository)
        {
            this.departmentRepository = _departmentRepository;
        }

        // GET api/<Department Controller>/5
        [HttpGet]
        public async Task<List<Department>> Get()
        {
            return await departmentRepository.GetAllAsync();
        }


        // POST api/<Department Controller>
        [HttpPost]
        public async Task Post(Department department)
        {
            await departmentRepository.AddAsync(department);
        }

        // PUT api/<Department Controller>/5
        [HttpPut("{id}")]
        public async Task Put(Department department)
        {
            await departmentRepository.UpdateAsync(department);
        }


    }
}
