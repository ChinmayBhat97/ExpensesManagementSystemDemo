using DRS.ExpenseManagementSystem.Abstraction.Models;
using DRS.ExpenseManagementSystem.Abstraction.Repository;
using DRS.ExpenseManagementSystem.Repository.DatabaseContext;
using DRS.ExpenseManagementSystem.Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DRS.ExpenseManagementSystem.Repository
{
    public class EmployeeRepository : BaseRepository<Employee>, IEmployeeRepository
    {
        private ExpenseManagementSystemContext databaseContext;
        public EmployeeRepository(ExpenseManagementSystemContext databaseContext) : base(databaseContext)
        {
            this.databaseContext = databaseContext;
        }

        public Task<List<Employee>> GetByDept(string dept)
        {
            return databaseContext.Employees.AsQueryable().Where(x => x.Dept = dept).ToList();
        }

        public Task<List<Employee>> GetByEmpFirstName(string empFirstName)
        {
            throw new NotImplementedException();
        }

        public Task<Employee> GetByEmpId(int empid)
        {
            throw new NotImplementedException();
        }
    }
}
