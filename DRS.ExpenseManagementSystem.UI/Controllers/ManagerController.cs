﻿using DRS.ExpenseManagementSystem.UI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.Text.Json;
using System.Linq;
using JsonSerializer = Newtonsoft.Json.JsonSerializer;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Authorization;
using System;
using Microsoft.AspNetCore.Hosting;
using Syncfusion.EJ2.Diagrams;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;
using System.Security.Claims;

namespace DRS.ExpenseManagementSystem.UI.Controllers
{
    //[Authorize(Roles = "2")]
    public class ManagerController : Controller
    {
        private readonly IConfiguration configuration;
        private readonly IHostingEnvironment webHostEnvironment;
        private readonly HttpClient client;

        public ManagerController(IConfiguration _configuration, IHostingEnvironment _webHostEnvironment)
        {
            this.configuration = _configuration;
            webHostEnvironment = _webHostEnvironment;
            this.client = new HttpClient
            {
                BaseAddress = new Uri(configuration["BaseUrl"]),
                Timeout = TimeSpan.FromMinutes(5)
            };
        }

       

       // [Authorize(Roles = "2")]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            HttpResponseMessage responseHomePage = await client.GetAsync(client.BaseAddress + "ExpenseClaim");
            if (responseHomePage.IsSuccessStatusCode)
            {
                var responseContent = await responseHomePage.Content.ReadAsStringAsync();
                var model = JsonConvert.DeserializeObject<List<ExpenseClaimViewModel>>(responseContent);

                var filteredModel = model?.Count > 0 ? model.Where(e => e.Status == 1).ToList() : new();
                return View(filteredModel);
            }
            else
            {

                return View();
            }
        }



        //[HttpGet("Manager/EditByManager/{id}")]
        //public async Task<IActionResult> EditByManager(int id)
        //{
        //    HttpResponseMessage responseEditUser = await client.GetAsync(client.BaseAddress + $"ExpenseClaim/{id}");
        //    var EditByManager = JsonConvert.DeserializeObject<ExpenseClaimViewModel>(await responseEditUser.Content.ReadAsStringAsync());
        //    return View(EditByManager);
        //}

        //[HttpPost("Manager/EditByManager/{id}")]
        //public async Task<IActionResult> EditByManager(int id, ExpenseClaim expenseClaim)
        //{
        //    if (ModelState.IsValid)
        //    {

        //        var myContent = JsonConvert.SerializeObject(expenseClaim);
        //        var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
        //        var byteContent = new ByteArrayContent(buffer);
        //        byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
        //        HttpResponseMessage response = await client.PutAsync(client.BaseAddress + $"ExpenseClaim/{expenseClaim.Id}", byteContent);
        //        if (response.IsSuccessStatusCode)
        //        {
        //            return RedirectToAction("Index");
        //        }
        //    }

        //    return View(expenseClaim);
        //}

        [HttpGet("ExpenseClaim/EditByManager/{id}")]
        public async Task<IActionResult> EditByManager(int id)
        {
            HttpResponseMessage responseDetailsClaim = await client.GetAsync(client.BaseAddress + $"ExpenseClaim/{id}");
            var detailsClaim = JsonConvert.DeserializeObject<ExpenseClaimViewModel>(await responseDetailsClaim.Content.ReadAsStringAsync());
            //var detailsProject = JsonConvert.DeserializeObject<Project>(await responseDetailsClaim.Content.ReadAsStringAsync());
            //if(detailsClaim.ProjectId == detailsProject.Id )

            // Retrieve IndividualExpenditure data and add it to the ExpenseClaimViewModel
            HttpResponseMessage responseIndividualExpenditures = await client.GetAsync(client.BaseAddress + $"IndividualExpenditure/{id}");
            var individualExpenditures = JsonConvert.DeserializeObject<List<IndividualExpenditureViewModel>>(await responseIndividualExpenditures.Content.ReadAsStringAsync());

            detailsClaim.ManagerApprovedOn = DateTime.Now;
            detailsClaim.IndividualExpenditures = individualExpenditures;

            //drop down to show department names
            HttpResponseMessage responseDepartmentList = await client.GetAsync(client.BaseAddress + $"Department");
            var departmentList = JsonConvert.DeserializeObject<List<Department>>(await responseDepartmentList.Content.ReadAsStringAsync());
            var departmentSelectList = new List<SelectListItem>();
            foreach (var department in departmentList)
            {
                departmentSelectList.Add(new SelectListItem(department.Name, department.Id.ToString()));
            }
            ViewBag.departmentList = departmentSelectList;

            //drop down to show project names
            HttpResponseMessage responseProjectList = await client.GetAsync(client.BaseAddress + $"Project");
            var projectList = JsonConvert.DeserializeObject<List<Project>>(await responseProjectList.Content.ReadAsStringAsync());
            var projectSelectList = new List<SelectListItem>();
            foreach (var project in projectList)
            {
                projectSelectList.Add(new SelectListItem(project.Title, project.Id.ToString()));
            }
            ViewBag.projectList = projectSelectList;

            //dropdown to show categories
            HttpResponseMessage responseCategoryList = await client.GetAsync(client.BaseAddress + $"ExpenseCategory");
            var categoryList = JsonConvert.DeserializeObject<List<ExpenseCategory>>(await responseCategoryList.Content.ReadAsStringAsync());
            var categorySelectList = new List<SelectListItem>();
            foreach (var category in categoryList)
            {
                categorySelectList.Add(new SelectListItem(category.Name, category.Id.ToString()));
            }
            ViewBag.categoryList = categorySelectList;

            //drop down to show status
            HttpResponseMessage responseStatusList = await client.GetAsync(client.BaseAddress + $"ClaimStatus");
            var statusList = JsonConvert.DeserializeObject<List<ClaimStatus>>(await responseStatusList.Content.ReadAsStringAsync());
            var statusSelectList = new List<SelectListItem>();
            foreach (var status in statusList)
            {
                statusSelectList.Add(new SelectListItem(status.Name, status.Id.ToString()));
            }
            ViewBag.statusList = statusSelectList;


            return View(detailsClaim);
        }

        
        [HttpPost("ExpenseClaim/EditByManager/{id}")]
        public async Task<IActionResult> EditByManager(ExpenseClaimViewModel expenseClaimViewModel)
        {
            expenseClaimViewModel.IndividualExpenditures.ForEach(x => x.ClaimId = expenseClaimViewModel.Id);

            // Save ExpenseClaim
            var expenseClaimContent = JsonConvert.SerializeObject(expenseClaimViewModel);
            var expenseClaimBuffer = System.Text.Encoding.UTF8.GetBytes(expenseClaimContent);
            var expenseClaimByteContent = new ByteArrayContent(expenseClaimBuffer);
            expenseClaimByteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            await client.PutAsync(client.BaseAddress + $"ExpenseClaim", expenseClaimByteContent);
            await client.PutAsync(client.BaseAddress + $"ExpenseClaim/", expenseClaimByteContent);

            return RedirectToAction("Index");
        }




        [HttpGet("Manager/DetailsManager/{id}")]
        public async Task<IActionResult> DetailsByManager(int id)
        {
            HttpResponseMessage responseDetailsManager = await client.GetAsync(client.BaseAddress + $"ExpenseClaim/{id}");
            var detailsManager = JsonConvert.DeserializeObject<ExpenseClaim>(await responseDetailsManager.Content.ReadAsStringAsync());
            return View(detailsManager);
        }

        // Get all that are approved

        [HttpGet]
        public async Task<IActionResult> IndexApprovedByManager()
        {
            HttpResponseMessage responseHomePage = await client.GetAsync(client.BaseAddress + "ExpenseClaim");
            if (responseHomePage.IsSuccessStatusCode)
            {
                var responseContent = await responseHomePage.Content.ReadAsStringAsync();
                var model = JsonConvert.DeserializeObject<List<ExpenseClaimViewModel>>(responseContent);

                var filteredModel = model?.Count > 0 ? model.Where(e => e.Status == 2).ToList() : new();
                return View(filteredModel);
            }
            else
            {

                return View();
            }
        }

        // Get all that are rejected

        [HttpGet]
        public async Task<IActionResult> IndexRejectedByManager()
        {
            HttpResponseMessage responseHomePage = await client.GetAsync(client.BaseAddress + "ExpenseClaim");
            if (responseHomePage.IsSuccessStatusCode)
            {
                var responseContent = await responseHomePage.Content.ReadAsStringAsync();
                var model = JsonConvert.DeserializeObject<List<ExpenseClaimViewModel>>(responseContent);

                var filteredModel = model?.Count > 0 ? model.Where(e => e.Status == 3).ToList() : new();
                return View(filteredModel);
            }
            else
            {

                return View();
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetByClaimId(int ID)
        {
            HttpResponseMessage responseManager = await client.GetAsync(client.BaseAddress + $"ExpenseClaim/{ID}");
            return View();
        }


        [HttpGet]
        public async Task<IActionResult> GetByEmployeeId(int EmpId)
        {
            HttpResponseMessage responseManager = await client.GetAsync(client.BaseAddress + $"ExpenseClaim/{EmpId}");
            return View();
        }


        [HttpGet]
        public async Task<IActionResult> GetByPeriod(DateTime startDate, DateTime endDate)
        {
            HttpResponseMessage responseManager = await client.GetAsync(client.BaseAddress + $"ExpenseClaim/{startDate},{endDate}");
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetByClaimRequestDate(DateTime requestDate)
        {
            HttpResponseMessage responseManager = await client.GetAsync(client.BaseAddress + $"ExpenseClaim/{requestDate}");
            return View();
        }
    }
}

