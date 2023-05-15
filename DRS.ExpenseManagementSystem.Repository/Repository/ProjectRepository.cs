using DRS.ExpenseManagementSystem.Abstraction.Models;
using DRS.ExpenseManagementSystem.Abstraction.Repository;
using DRS.ExpenseManagementSystem.Repository.DatabaseContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DRS.ExpenseManagementSystem.Repository.Repository
{
    public class ProjectRepository : BaseRepository<Project>, IProjectRepository
    {
        private ExpenseManagementSystemContext _dbContext;
        public ProjectRepository(ExpenseManagementSystemContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<List<Project>> GetByEmpIdAsync(int empId)
        {
            throw new NotImplementedException();
        }
    }
}
