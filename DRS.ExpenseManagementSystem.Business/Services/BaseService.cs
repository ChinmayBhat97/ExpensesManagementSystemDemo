using DRS.ExpenseManagementSystem.Abstraction.Repository;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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

        public string Message { get; private set; }

        public async Task<int> AddAsync(T entity)
        {
            try
            {
                await repository.AddAsync(entity);
                return await repository.SaveChangesAsync();
            }
            catch (SqlException ex)
            {
                var p = ex.Message;
                Message = "data already exists";
            }

            return 0;
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
