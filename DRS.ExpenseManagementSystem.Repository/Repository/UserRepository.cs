using DRS.ExpenseManagementSystem.Abstraction.Models;
using DRS.ExpenseManagementSystem.Abstraction.Repository;
using DRS.ExpenseManagementSystem.Abstraction.ViewModels;
using DRS.ExpenseManagementSystem.WebAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DRS.ExpenseManagementSystem.Repository.Repository
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        private ExpensesManagementSystem_UpdatedContext _dbContext;
        public UserRepository(ExpensesManagementSystem_UpdatedContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public UserViewModel GetByEmployeeCodeAndPassword(string EmployeeCode, string Password)
        {
            //var userTable = _dbContext.Users
            //    .Where(n => n.EmployeeCode == EmployeeCode && n.Password == Password).ToList()
            //    .Select(x => new UserViewModel
            //    {
            //        EmployeeCode = x.EmployeeCode,
            //        Password = x.Password,
            //        IsAccountLocked = x.IsAccountLocked,
            //        IsActive =x.IsActive,
            //        Role = x.Role
            //    }).FirstOrDefault();



            var userTable = (from u in _dbContext.Users.AsQueryable().ToList() // outer sequence
                             join e in _dbContext.Employees.AsQueryable().ToList() //inner sequence 
                            on u.Id equals e.EmpId // key selector 
                             where u.EmployeeCode == EmployeeCode && u.Password ==Password

                             select new UserViewModel

                             { // result selector 
                                 EmployeeCode = u.EmployeeCode,
                                 Password = u.Password,
                                 IsAccountLocked = u.IsAccountLocked,
                                 IsActive =u.IsActive,
                                 Role = (int)u.Role,
                                 EmpId = e.Id,
                                 FirstName = e.FirstName,
                                 Designation = e.Designation
                             }).FirstOrDefault();

            //  var  userID = _dbContext.Users.Where(a =>a.EmployeeCode==EmployeeCode).Select(c =>c.Id).FirstOrDefault();
            //  var  empID= _dbContext.Employees.Where(k =>k.EmpId==userID).Select(g =>g.Id).FirstOrDefault();

            //  var firstName = _dbContext.Employees.Where(d =>d.Id==empID).Select(s =>s.FirstName).FirstOrDefault();
            //  var designation = _dbContext.Employees.Where(w =>w.Id==empID).Select(t =>t.Designation).FirstOrDefault();

            //TempData["EmpID"]= empID;

            return userTable;
        }

        public Task<List<User>> GetByRole(int role)
        {
            throw new NotImplementedException();
        }


    }
}
