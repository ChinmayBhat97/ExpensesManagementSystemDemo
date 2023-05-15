using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DRS.ExpenseManagementSystem.Abstraction.Services
{
    public interface IBaseService<T>
    {
        Task<List<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);

        Task<int> UpdateAsync(T entity);

        Task<int> DeleteAsync(T entity);

        Task<int> AddAsync(T entity);
    }
}
