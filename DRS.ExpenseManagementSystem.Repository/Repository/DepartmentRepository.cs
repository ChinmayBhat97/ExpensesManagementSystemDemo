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
    public class DepartmentRepository : BaseRepository<Department>, IDepartmentRepository
    {
        private ExpenseManagementSystemContext databaseContext;
        public DepartmentRepository(ExpenseManagementSystemContext databaseContext) : base(databaseContext)
        {
            this.databaseContext = databaseContext;
        }
    }
}
