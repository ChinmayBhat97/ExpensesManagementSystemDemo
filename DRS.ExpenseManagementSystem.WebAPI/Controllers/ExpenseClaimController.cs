﻿using DRS.ExpenseManagementSystem.Abstraction.Models;
using DRS.ExpenseManagementSystem.Abstraction.Services;
using DRS.ExpenseManagementSystem.Business.Services;
using Microsoft.AspNetCore.Mvc;
using NuGet.ContentModel;
using System.Linq;

namespace DRS.ExpenseManagementSystem.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpenseClaimController : ControllerBase
    {
        private IExpenseClaimServices expensesClaimServices;


        public ExpenseClaimController(IExpenseClaimServices _expensesClaimServices)
        {
            this.expensesClaimServices = _expensesClaimServices;

        }

        // GET: api/<ExpenseClaimController>
        //[HttpGet]
        //public async Task<List<ExpenseClaim>> GetAll()
        //{
        //    return await expensesClaimServices.GetAllAsync();
        //}

        // GET: api/<ExpenseClaimController>/3
        [HttpGet]
        public async Task<List<ExpenseClaim>> GetAllDetails()
        {
            return await expensesClaimServices.GetAllDetails();
            
        }

        [HttpGet("Details/{id}")]
        public async Task<List<ExpenseClaim>> GetExpenseClaimDetailsById(string id)
        {
            var test = await expensesClaimServices.GetDetailsByEmpId(int.Parse(id));            
            return test;
        }

        // GET api/<ExpenseClaimController>/5
        [HttpGet("Id/{id}")]
        public async Task<ExpenseClaim> GetByID(int id)
        {
            return await expensesClaimServices.GetByIdAsync(id);
        }


        // GET api/<ExpenseClaimController>/5
        [HttpGet("ClaimId/{claimId}")]
        public async Task<ExpenseClaim> Details(int claimId)
        {
            return await expensesClaimServices.GetByIdAsync(claimId);
        }

        // GET api/<ExpenseClaimController>/5
        [HttpGet("{claimId}")]
        public async Task<ExpenseClaim> GetByClaimId(int claimId)
        {

            return await expensesClaimServices.GetByClaimId(claimId);
        }

        // GET api/<ExpenseClaimController>/5
        [HttpGet("empId/{EmpId}")]
        public async Task<List<ExpenseClaim>> GetByEmployeeID(int employeeId)
        {
            return await expensesClaimServices.GetByEmpId(employeeId);
        }


        // GET api/<ExpenseClaimController>/5
        [HttpGet("status/{statusId}")]
        public async Task<List<ExpenseClaim>> GetByStatus(int statusId)
        {
            return await expensesClaimServices.GetByClaimStatus(statusId);
        }


        // GET api/<ExpenseClaimController>/
        [HttpGet("dateStart,dateEnd/{startDate,dateEnd}")]
        public async Task<List<ExpenseClaim>> GetByPeriod(DateTime dateStart, DateTime dateEnd, int empId)
        {
            return await expensesClaimServices.GetByClaimPeriods(dateStart, dateEnd, empId);
        }

        // GET api/<ExpenseClaimController>/
        //[HttpGet("dateRequest/{requestDate}")]
        //public async Task<List<ExpenseClaim>> GetByDate(DateTime date)
        //{
        //    return await expensesClaimServices.GetByClaimedDate(date);
        //}

        // POST api/<ExpenseClaimController>
        [HttpPost]
        public async Task Post(ExpenseClaim expensesClaim)
        {
             await expensesClaimServices.AddAsync(expensesClaim);
        }

        // PUT api/<ExpenseClaimController>/5
        [HttpPut]
        public async Task Put(ExpenseClaim expensesClaim)
        {
            await expensesClaimServices.UpdateAsync(expensesClaim);
        }

        [HttpPut("Project/Details/{id}")]
        public async Task UpdateProjectDetails(ExpenseClaim expensesClaim)
        {
            await expensesClaimServices.UpdateAsync(expensesClaim);
        }


        [HttpPost("{EmpId}")]
        public async Task<List<ExpenseClaim>> GetByManagerId(int EmpId)
        {
            return await expensesClaimServices.GetByManagerId(EmpId);
        }
    }
}
