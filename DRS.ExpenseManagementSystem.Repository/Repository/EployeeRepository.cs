using DRS.ExpenseManagementSystem.Abstraction.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DRS.ExpenseManagementSystem.Repository.Repository
{
    public class EmployeeRepository : BaseRepository<Tenant>, ITenantRepository
    {
        private ScaffoldDbContext databaseContext;
        public TenantRepository(ScaffoldDbContext databaseContext) : base(databaseContext)
        {
            this.databaseContext = databaseContext;
        }
    }
}
