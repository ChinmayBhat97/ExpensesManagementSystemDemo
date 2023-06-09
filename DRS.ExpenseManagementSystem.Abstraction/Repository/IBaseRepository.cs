﻿using DRS.ExpenseManagementSystem.Abstraction.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DRS.ExpenseManagementSystem.Abstraction.Repository
{
    public interface IBaseRepository<T>
    {
        Task<List<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);

        Task UpdateAsync(T entity);

        Task AddAsync(T entity);

        Task<int> SaveChangesAsync();
        Task DeleteAsync(T entity);
        Task UpdateRangeAsync(List<T> entity);
    }
}
