﻿using DRS.ExpenseManagementSystem.Abstraction.Models;
using DRS.ExpenseManagementSystem.Abstraction.Repository;

using DRS.ExpenseManagementSystem.WebAPI.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DRS.ExpenseManagementSystem.Repository.Repository
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly ExpensesManagementSystem_UpdatedContext dbContext;
        private DbSet<T> table;
        private string message;

        public BaseRepository(ExpensesManagementSystem_UpdatedContext databaseContext)
        {
            this.dbContext = databaseContext;
            this.dbContext.Database.SetCommandTimeout(360);
            table = dbContext.Set<T>();
        }

        public async Task AddAsync(T entity)
        {
            try
            {
                await table.AddAsync(entity);
            }
            catch (Exception ex)
            {
                SqlException innerException = ex.InnerException as SqlException;
                if (innerException != null)
                {
                    message = "yes";
                }
                else
                {
                    throw;
                }
            }

        }

        public async Task DeleteAsync(T entity)
        {
            T existing = await table.FindAsync(entity);
            table.Remove(existing);
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await table.ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await table.FindAsync(id); ;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            table.Attach(entity);
            dbContext.Entry(entity).State = EntityState.Modified;
        }

        public async Task UpdateRangeAsync(List<T> entity)
        {
            table.UpdateRange(entity);
            dbContext.Entry(entity).State = EntityState.Modified;
        }
    }
}
