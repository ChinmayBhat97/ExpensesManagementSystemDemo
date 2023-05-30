using DRS.ExpenseManagementSystem.Abstraction.Models;
using DRS.ExpenseManagementSystem.Abstraction.Repository;

using DRS.ExpenseManagementSystem.Repository.Repository;
using DRS.ExpenseManagementSystem.WebAPI.Models;
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
        private ExpensesManagementSystem_UpdatedContext _dbContext;

        public EmployeeRepository(ExpensesManagementSystem_UpdatedContext dbcontext) : base(dbcontext)
        {
            this._dbContext = dbcontext;
        }
        public async Task<List<Employee>> GetByDeptId(int deptId)
        {
          return await _dbContext.Employees.AsQueryable().Where(q =>q.DeptId == deptId).ToListAsync();
        }

        public async Task<List<Employee>> GetByEmpFirstName(string empFirstName)
        {
           return await _dbContext.Employees.AsQueryable().Where(t =>t.FirstName== empFirstName).ToListAsync();
        }

        public async Task<Employee> GetByEmpId(int empid)
        {
            return await _dbContext.Employees.AsQueryable().Where(u =>u.EmpId==empid).FirstOrDefaultAsync<Employee>();
        }


        public async Task<List<Employee>> GetAllDetails()
        {
            return await _dbContext.Employees.Include(a => a.Dept).Include(a =>a.Emp).ToListAsync();
        }
    }
}
