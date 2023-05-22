using DRS.ExpenseManagementSystem.Abstraction.Models;
using DRS.ExpenseManagementSystem.Abstraction.Repository;

using DRS.ExpenseManagementSystem.WebAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DRS.ExpenseManagementSystem.Repository.Repository
{
    public class ProjectRepository : BaseRepository<Project>, IProjectRepository
    {
        private ExpensesManagementSystem_UpdatedContext _dbContext;
        public ProjectRepository(ExpensesManagementSystem_UpdatedContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Project>> GetByEmpIdAsync(int empId)
        {
            return await _dbContext.Projects.AsQueryable().Where(s =>s.EmpId == empId).ToListAsync();
        }

      
    }
}
