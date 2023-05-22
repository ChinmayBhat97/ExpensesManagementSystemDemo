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
        //
        public Task<List<Project>> GetByEmpIdAsync(int empId);

      
    }
}
