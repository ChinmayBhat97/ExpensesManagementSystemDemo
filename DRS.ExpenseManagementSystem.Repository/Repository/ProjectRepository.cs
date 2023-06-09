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
    public class ProjectRepository : BaseRepository<Project>, IProjectRepository
    {
        private ExpensesManagementSystem_UpdatedContext _dbContext;
        public ProjectRepository(ExpensesManagementSystem_UpdatedContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Project>> GetByEmpIdAsync(int empId)
        {
            return await _dbContext.Projects.AsQueryable().Where(s =>s.EmpId == empId).ToListAsync();
        }

        public async Task<List<Project>> GetByTitleAsync(string projectTitle)
        {
            return await _dbContext.Projects.AsQueryable().Where(p =>p.Title == projectTitle).ToListAsync();
        }


        public async Task<List<Project>> GetByClientInfo(string clientInfo)
        {
            return await _dbContext.Projects.AsQueryable().Where(p => p.Client == clientInfo).ToListAsync();
        }

        public async Task<List<Project>> GetAllDetails()
        {
            return await _dbContext.Projects
               .Include(p => p.Emp)
               .ToListAsync();
        }

        public async Task<List<Project>> GetByEmpId(int EmpId)
        {
            return await _dbContext.Projects.AsQueryable().Where(p => p.EmpId == EmpId).ToListAsync();
        }

        public async Task<bool> DeleteById(int id)
        {
            var project = await _dbContext.Projects.FindAsync(id);

            if (project == null)
            {
                return false;
            }

            project.IsDelete = 1;

            await _dbContext.SaveChangesAsync();

            return true;
        }
    }
}
