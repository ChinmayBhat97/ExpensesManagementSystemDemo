using DRS.ExpenseManagementSystem.Abstraction.Models;
using DRS.ExpenseManagementSystem.Abstraction.Repository;
using DRS.ExpenseManagementSystem.Repository.DatabaseContext;
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
        private readonly ExpenseManagementSystemContext dbContext;
        private DbSet<T> table;
        public BaseRepository(ExpenseManagementSystemContext databaseContext)
        {
            this.dbContext = databaseContext;
            this.dbContext.Database.SetCommandTimeout(360);
            table = dbContext.Set<T>();
        }

        public async Task AddAsync(T entity)
        {
            await table.AddAsync(entity);
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
    }
}
