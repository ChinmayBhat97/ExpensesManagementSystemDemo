﻿using DRS.ExpenseManagementSystem.Abstraction.Models;
using DRS.ExpenseManagementSystem.Abstraction.Services;
using DRS.ExpenseManagementSystem.Abstraction.ViewModels;
using DRS.ExpenseManagementSystem.Business.Services;
using Microsoft.AspNetCore.Mvc;

namespace DRS.ExpenseManagementSystem.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpenseCategoryController : ControllerBase
    {
        private IExpenseCategoryServices expensesCategoryServices;

        public ExpenseCategoryController(IExpenseCategoryServices _expensesCategoryServices)
        {
            this.expensesCategoryServices= _expensesCategoryServices;
        }

        // GET api/<ExpensesCategory Controller>/5
        [HttpGet]
        public async Task<List<ExpenseCategory>> Get()
        {
            return await expensesCategoryServices.GetAllAsync();
        }

        // GET api/<ExpenseCategoryController>/5
        [HttpGet("{id}")]
        public async Task<ExpenseCategory> GetByExpenseCategoryId(int id)
        {
            return await expensesCategoryServices.GetByIdAsync(id);
        }


        // POST api/<ExpensesCategory Controller>
        [HttpPost]
        public async Task Post(ExpenseCategory expensesCategory)
        {
            await expensesCategoryServices.AddAsync(expensesCategory);
        }

        // PUT api/<ExpensesCategory Controller>/5
        [HttpPut("{id}")]
        public async Task Put(ExpenseCategory expensesCategory)
        {
            await expensesCategoryServices.UpdateAsync(expensesCategory);
        }

        // DELETE api/<LocationController>/5
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await expensesCategoryServices.DeleteById(id);
        }


        //[HttpPost]
        //public ExpenseCategoryViewModel CheckByTitle(string title)
        //{
        //   return  expensesCategoryServices.GetByTitle(title);
        //}

    }
}
