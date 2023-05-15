using DRS.ExpenseManagementSystem.Abstraction.Models;
using DRS.ExpenseManagementSystem.Abstraction.Repository;
using DRS.ExpenseManagementSystem.Abstraction.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DRS.ExpenseManagementSystem.Business.Services
{
    public class EmployeeService : BaseService<Employee>, IEmployeeService
    {
        private IEmployeeRepository employeeRepository;
        public EmployeeService(IEmployeeRepository repository) : base(repository)
        {
            this.employeeRepository = repository;
        }

        public async Task<List<Employee>> GetByDeptId(int deptid)
        {
            return await employeeRepository.GetByDeptId(deptid);
        }

        public async Task<List<Employee>> GetByEmpFirstName(string empFirstName)
        {
            return await employeeRepository.GetByEmpFirstName(empFirstName);
        }

        public async Task<Employee> GetByEmpId(int empid)
        {
            return await employeeRepository.GetByEmpId(empid);
        }
    }
}
