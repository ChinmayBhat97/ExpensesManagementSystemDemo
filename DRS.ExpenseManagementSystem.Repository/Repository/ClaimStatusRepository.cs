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
    public class ClaimStatusRepository : BaseRepository<ClaimStatus> , IClaimStatusRepository
    {
        private ExpenseManagementSystemContext _dbContext;

        public ClaimStatusRepository(ExpenseManagementSystemContext dbcontext) : base(dbcontext)
        {
            this._dbContext = dbcontext;
        }


    }
}
