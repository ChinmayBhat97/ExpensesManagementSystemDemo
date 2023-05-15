using DRS.ExpenseManagementSystem.Abstraction.Models;
using DRS.ExpenseManagementSystem.Abstraction.Repository;
using DRS.ExpenseManagementSystem.Repository.DatabaseContext;
using DRS.ExpenseManagementSystem.Repository.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DRS.ExpenseManagementSystem.Repository
{
    public class EmployeeRepository : BaseRepository<Employee>, IEmployeeRepository
    {
        private ExpenseManagementSystemContext _dbContext;

        public EmployeeRepository(ExpenseManagementSystemContext dbcontext) : base(dbcontext)
        {
            this._dbContext = dbcontext;
        }

       

        public async Task<List<Employee>> GetByDeptId(int deptId)
        {
          return await _dbContext.Employees.AsQueryable().Where(q =>q.DeptId == deptId).ToListAsync();
        }

        public async Task<List<Employee>> GetByEmpFirstName(string empFirstName)
        {
           return await _dbContext.Employees.AsQueryable().Where(t =>t.EmpFirstName== empFirstName).ToListAsync();
        }

        public async Task<Employee> GetByEmpId(int empid)
        {
            return await _dbContext.Employees.AsQueryable().Where(u =>u.EmpId==empid).FirstOrDefaultAsync<Employee>();
        }
    }
}
