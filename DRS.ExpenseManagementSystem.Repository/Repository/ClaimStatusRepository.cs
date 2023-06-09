﻿using DRS.ExpenseManagementSystem.Abstraction.Models;
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
    public class ClaimStatusRepository : BaseRepository<ClaimStatus> , IClaimStatusRepository
    {
        private ExpensesManagementSystem_UpdatedContext _dbContext;

        public ClaimStatusRepository(ExpensesManagementSystem_UpdatedContext databaseContext) : base(databaseContext)
        {
            this._dbContext = databaseContext;
        }

        public async Task<bool> DeleteStatusById(int Id)
        {
            var status = await _dbContext.ClaimStatuses.FindAsync(Id);

            if (status == null)
            {
                return false; 
            }

            status.IsDelete = 1; 

            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<List<ClaimStatus>> StatusByRole(int role)
        {
            if(role==2)
            {
                return await _dbContext.ClaimStatuses.Where(p => p.Id == 2 || p.Id==4 && p.IsDelete == 0).ToListAsync();
            }
            else if(role==3)
            {
                return await _dbContext.ClaimStatuses.Where(p => p.Id == 3 || p.Id==5 ||p.Id==7 || p.Id==8 && p.IsDelete == 0).ToListAsync();
            }
            else
            {
                return await _dbContext.ClaimStatuses.ToListAsync();
            }
               
        }
    }
}
