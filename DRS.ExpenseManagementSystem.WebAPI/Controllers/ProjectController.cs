﻿using DRS.ExpenseManagementSystem.Abstraction.Models;
using DRS.ExpenseManagementSystem.Abstraction.Repository;
using DRS.ExpenseManagementSystem.Abstraction.Services;
using DRS.ExpenseManagementSystem.Business.Services;
using DRS.ExpenseManagementSystem.Repository.Repository;
using Microsoft.AspNetCore.Mvc;

namespace DRS.ExpenseManagementSystem.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private IProjectService projectService;

        public ProjectController(IProjectService _projectService)
        {
            this.projectService = _projectService;
        }

        // GET api/<Department Controller>/5
        //[HttpGet]
        //public async Task<List<Department>> Get()
        //{
        //    return await departmentService.GetAllAsync();
        //}

        // GET api/<Project Controller>/5
        //[HttpGet]
        //public async Task<List<Project>> Get()
        //{
        //    return await projectService.GetAllAsync();
        //}

        [HttpGet]
        public async Task<List<Project>> GetAllDetails()
        {
            return await projectService.GetAllDetails();
        }

        //[HttpGet("{pid}")]
        //public async Task<Project> GetByProjectId(int pid)
        //{
        //    return await projectService.GetByIdAsync(pid);
        //}


        // Edited by Chinmay
        // GET api/<Project Controller>/5
        [HttpGet("{id}")]
        public async Task<Project> GetByProjectId(int id)
        {
            return await projectService.GetByIdAsync(id);
        }

        // GET api/<Project Controller>/5
        [HttpGet("Title/{title}")]
        public async Task<List<Project>> GetByProjectName(string title)
        {
            return await projectService.GetByTitleAsync(title);
        }

        // POST api/<Project Controller>
        [HttpPost]
        public async Task Post(Project project)
        {
           await projectService.AddAsync(project);
        }

        // PUT api/<Project Controller>/5
        [HttpPut("{id}")]
        public async Task Put(Project project)
        {
            await projectService.UpdateAsync(project);
        }


    }
}
