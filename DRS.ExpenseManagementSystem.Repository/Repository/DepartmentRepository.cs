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
    public class DepartmentRepository : BaseRepository<Department>, IDepartmentRepository
    {
        private ExpensesManagementSystem_UpdatedContext databaseContext;
        public DepartmentRepository(ExpensesManagementSystem_UpdatedContext databaseContext) : base(databaseContext)
        {
            this.databaseContext = databaseContext;
        }

        public async Task<bool> DeleteById(int Id)
        {
            var status = await databaseContext.Departments.FindAsync(Id);

            if (status == null)
            {
                return false; 
            }

            status.IsDelete = 1; 

            await databaseContext.SaveChangesAsync();

            return true;
        }
    }
}
