using DRS.ExpenseManagementSystem.Abstraction.Models;
using DRS.ExpenseManagementSystem.Abstraction.Repository;
using DRS.ExpenseManagementSystem.Repository.Repository;
using Microsoft.AspNetCore.Mvc;

namespace DRS.ExpenseManagementSystem.WebAPI.Controllers
{
    public class ProjectController : Controller
    {
        private IProjectRepository projectRepository;

        public ProjectController(IProjectRepository _projectRepository)
        {
            this.projectRepository = _projectRepository;
        }

        // GET api/<Project Controller>/5
        [HttpGet]
        public async Task<List<Project>> Get()
        {
            return await projectRepository.GetAllAsync();
        }

        // GET api/<Project Controller>/5
        [HttpGet]
        public async Task<List<Project>> GetByEmployeeId(int Id)
        {
            return await projectRepository.GetByEmpIdAsync(Id);
        }

        // GET api/<Project Controller>/5
        [HttpGet]
        public async Task<List<Project>> GetByProjectName(string title)
        {
            return await projectRepository.GetByTitleAsync(title);
        }

        // POST api/<Project Controller>
        [HttpPost]
        public async Task Post(Project project)
        {
            await projectRepository.AddAsync(project);
        }

        // PUT api/<Project Controller>/5
        [HttpPut("{id}")]
        public async Task Put(Project project)
        {
            await projectRepository.UpdateAsync(project);
        }


    }
}
