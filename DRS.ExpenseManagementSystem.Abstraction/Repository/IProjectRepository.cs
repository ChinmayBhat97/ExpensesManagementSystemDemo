using DRS.ExpenseManagementSystem.Abstraction.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DRS.ExpenseManagementSystem.Abstraction.Repository
{
    public interface IProjectRepository:IBaseRepository<Project>
    {
        public Task<List<Project>> GetByEmpIdAsync(int empId);

        public Task<List<Project>> GetByTitleAsync(string projectTitle);
        public Task<List<Project>> GetAllDetails();

        public Task<List<Project>> GetByEmpId(int EmpId);
        public Task<bool> DeleteById(int id);
    }
}
