using DRS.ExpenseManagementSystem.Abstraction.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DRS.ExpenseManagementSystem.Business.Services
{
    public class BaseService<T>
    {
        private IBaseRepository<T> repository;

        public BaseService(IBaseRepository<T> repository)
        {
            this.repository = repository;
        }

        public async Task<int> AddAsync(T entity)
        {
            await repository.AddAsync(entity);
            return await repository.SaveChangesAsync();
        }

        public async Task<int> DeleteAsync(T entity)
        {
            await repository.DeleteAsync(entity);
            return await repository.SaveChangesAsync();
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await repository.GetAllAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await repository.GetByIdAsync(id);
        }

        public async Task<int> UpdateAsync(T entity)
        {
            await repository.UpdateAsync(entity);
            return await repository.SaveChangesAsync();
        }
    }
}
