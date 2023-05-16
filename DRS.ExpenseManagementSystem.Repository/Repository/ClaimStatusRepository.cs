using DRS.ExpenseManagementSystem.Abstraction.Models;
using DRS.ExpenseManagementSystem.Abstraction.Repository;

using DRS.ExpenseManagementSystem.WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DRS.ExpenseManagementSystem.Repository.Repository
{
    public class ClaimStatusRepository : BaseRepository<ClaimStatus> , IClaimStatusRepository
    {
        private ExpensesManagementSystem_UpdatedContext _dbContext;

        public ClaimStatusRepository(ExpensesManagementSystem_UpdatedContext databaseContext) : base(databaseContext)
        {
            this._dbContext = databaseContext;
        }
    }
}
