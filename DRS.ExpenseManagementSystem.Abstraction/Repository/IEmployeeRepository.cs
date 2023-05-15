using DRS.ExpenseManagementSystem.Abstraction.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DRS.ExpenseManagementSystem.Abstraction.Repository
{
    public interface IEmployeeRepository: IBaseRepository<Employee>
    {
        public Task<Employee> GetByDept(int dept);
        public Task<Employee> GetByEmpId(int empid);
        public Task<Employee> GetByEmpFirstName(int empFirstName);

    }
}
