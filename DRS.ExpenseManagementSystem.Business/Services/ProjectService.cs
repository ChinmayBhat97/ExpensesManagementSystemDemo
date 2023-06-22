using DRS.ExpenseManagementSystem.Abstraction.Models;
using DRS.ExpenseManagementSystem.Abstraction.Repository;
using DRS.ExpenseManagementSystem.Abstraction.Services;
using DRS.ExpenseManagementSystem.Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DRS.ExpenseManagementSystem.Business.Services
{
    public class ProjectService : BaseService<Project>, IProjectService
    {
        private IProjectRepository projectRepository;
        public ProjectService(IProjectRepository repository) : base(repository)
        {
            this.projectRepository = repository;
        }

        public async Task<List<Project>> GetByClientInfo(string clientInfo)
        {
            return await projectRepository.GetByTitleAsync(clientInfo);
        }

        public async Task<List<Project>> GetByEmpIdAsync(int empId)
        {
            return await projectRepository.GetByEmpIdAsync(empId);
        }

        public async Task<List<Project>> GetByTitleAsync(string projectTitle)
        {
            return await projectRepository.GetByTitleAsync(projectTitle);
        }

        public async Task<List<Project>> GetAllDetails()
        {
            return await projectRepository.GetAllDetails();
        }

        public async Task<List<Project>> GetByEmpId(int EmpId)
        {
           return await projectRepository.GetByEmpId(EmpId);
        }

        public async Task<bool> DeleteById(int id)
        {
            return await projectRepository.DeleteById(id);
        }
    }
}
