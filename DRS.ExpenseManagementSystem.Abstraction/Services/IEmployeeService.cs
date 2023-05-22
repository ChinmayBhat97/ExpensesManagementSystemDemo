using DRS.ExpenseManagementSystem.Abstraction.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DRS.ExpenseManagementSystem.Abstraction.Services
{
    public interface IEmployeeService:IBaseService<Employee>
    {
        public Task<List<Employee>> GetByDeptId(int deptId);
        public Task<Employee> GetByEmpId(int empid);
       
    }
}
