using DRS.ExpenseManagementSystem.UI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Text.Json;
using JsonSerializer = Newtonsoft.Json.JsonSerializer;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Hosting;

namespace DRS.ExpenseManagementSystem.UI.Controllers
{
    public class ExpenseClaimController : Controller
    {
       private readonly IConfiguration configuration;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly HttpClient client;

        public ExpenseClaimController(IConfiguration _configuration, IWebHostEnvironment hostEnvironment)
        {
            this._hostEnvironment = hostEnvironment;
            this.configuration = _configuration;
            this.client = new HttpClient
            {
                BaseAddress = new Uri(configuration["BaseUrl"]),
                Timeout = TimeSpan.FromMinutes(5)
            };
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            int empID = Convert.ToInt32(TempData["logged_empID"]);
            int EmpID = empID;
           
            HttpResponseMessage responseHomePage = await client.GetAsync(client.BaseAddress + "ExpenseClaim");
            if (responseHomePage.IsSuccessStatusCode)
            {
                var responseContent = await responseHomePage.Content.ReadAsStringAsync();
                var model = JsonConvert.DeserializeObject<List<ExpenseClaimViewModel>>(responseContent);
                return View(model);
            }
            else
            {
                
                return View();
            }
        }

        [HttpGet("ExpenseClaim/Create")]
        public async Task<IActionResult> Create()
        {
            HttpResponseMessage responseCreateClaim = await client.GetAsync(client.BaseAddress + $"ExpenseClaim");
            HttpResponseMessage responseProjectList = await client.GetAsync(client.BaseAddress + $"Project");
            if (responseCreateClaim.IsSuccessStatusCode && responseProjectList.IsSuccessStatusCode)
            {
                var projectList = JsonConvert.DeserializeObject<List<Project>>(await responseProjectList.Content.ReadAsStringAsync());
                var projectSelectList = new List<SelectListItem>();
                foreach (var project in projectList)
                {
                    projectSelectList.Add(new SelectListItem(project.Title, project.Id.ToString()));
                }
                ViewBag.projectList = projectSelectList;
                var expenseClaimList = JsonConvert.DeserializeObject<List<ExpenseClaim>>(await responseCreateClaim.Content.ReadAsStringAsync());
                expenseClaimList = expenseClaimList.OrderBy(x => x.Id).ToList();
                return View();
            }
            return View();
        }

        [HttpPost("ExpenseClaim/Create")]
        public async Task<IActionResult> Create(ExpenseClaim expenseClaim)
        {
            if (ModelState.IsValid)
            {
                expenseClaim.Status = 1;
                int EmpID = Convert.ToInt32(TempData["logged_empID"]);
                var myContent = JsonConvert.SerializeObject(expenseClaim);
                var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                HttpResponseMessage createNewClaim = await client.PostAsync(client.BaseAddress + $"ExpenseClaim",byteContent);
                return RedirectToAction("Index");
            }
            return View(expenseClaim);
        }

        [HttpGet("ExpenseClaim/Edit/{id}")]
        public async Task<IActionResult> EditByClaimant(int id)
        {
            HttpResponseMessage responseEditClaim = await client.GetAsync(client.BaseAddress + $"ExpenseClaim/{id}");
            var EditClaim = JsonConvert.DeserializeObject<ExpenseClaim>(await responseEditClaim.Content.ReadAsStringAsync());
            return View(EditClaim);
        }

        [HttpPost("ExpenseClaim/Edit")]
        public async Task<IActionResult> EditByClaimant(ExpenseClaim expenseClaim)
        {
            if (ModelState.IsValid)
            {
                expenseClaim.Status = 1;
                var myContent = JsonConvert.SerializeObject(expenseClaim);
                var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                HttpResponseMessage response = await client.PutAsync(client.BaseAddress + $"ExpenseClaim/{expenseClaim.Id}", byteContent);
                return RedirectToAction("Index");
            }
            return View(expenseClaim);
        }

        [HttpGet("ExpenseClaim/DetailsByClaimID/{claimId}")]
        public async Task<IActionResult> DetailsByClaimID(int claimId)
        {
            HttpResponseMessage responseDetailsClaim = await client.GetAsync(client.BaseAddress + $"ExpenseClaim/{claimId}");
           var detailsClaim = JsonConvert.DeserializeObject<ExpenseClaim>(await responseDetailsClaim.Content.ReadAsStringAsync());
            return View(detailsClaim);


        }

        [HttpGet("IndividualExepnditure/Create")]
        public async Task<IActionResult> AddExpenditures(int Id)
        {
            ViewBag.ClaimId = Id;
            HttpResponseMessage responseCreateClaim = await client.GetAsync(client.BaseAddress + $"IndividualExpenditure");
            return View();

        }

        [HttpPost("IndividualExepnditure/Create")]
        public async Task<IActionResult> AddExpenditures(IndividualExpenditure individualExpenditure)
        {

            //string wwwRootPath = _hostEnvironment.WebRootPath;
            //string fileName = Path.GetFileNameWithoutExtension(individualExpenditure.ImageFile.FileName);
            //string extension = Path.GetExtension(individualExpenditure.ImageFile.FileName);
            //individualExpenditure.AttachmentPath=fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
            //string path = Path.Combine(wwwRootPath + "/Image/", individualExpenditure.AttachmentPath);
            //using (var fileStream = new FileStream(path, FileMode.Create))
            //{
            //    await individualExpenditure.ImageFile.CopyToAsync(fileStream);
            //}


            string wwwRootPath = _hostEnvironment.WebRootPath;
            string fileName = Path.GetFileNameWithoutExtension(individualExpenditure.ImageFile.FileName);
            string extension = Path.GetExtension(individualExpenditure.ImageFile.FileName);
            individualExpenditure.AttachmentPath=fileName = fileName + DateTime.Now.ToString("yymmddssff")+ extension;
            string path = Path.Combine(wwwRootPath + "/Image/", fileName);
            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                await individualExpenditure.ImageFile.CopyToAsync(fileStream);
            }

            var myContent = JsonConvert.SerializeObject(individualExpenditure);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            HttpResponseMessage createNewClaim = await client.PostAsync(client.BaseAddress + $"IndividualExpenditure", byteContent);
            return RedirectToAction("Index");

            // return View(individualExpenditure);
        }



       
        [HttpGet("IndividualExpenditure/IndexExpenditures/{id}")]
        public async Task<IActionResult> IndexExpenditures(int id)
        {
            HttpResponseMessage responseExpenditure = await client.GetAsync(client.BaseAddress + $"IndividualExpenditure/{id}");
            var EditClaim = JsonConvert.DeserializeObject<List<IndividualExpenditure>>(await responseExpenditure.Content.ReadAsStringAsync());
            return View(EditClaim);
        }
    }
}


//[HttpGet("ExpenseClaim/DetailsByClaimID/{claimId}")]
//public async Task<IActionResult> DetailsByClaimID(int claimId)
//{
//    HttpResponseMessage responseDetailsClaim = await client.GetAsync(client.BaseAddress + $"ExpenseClaim/{claimId}");
//    var detailsClaim = JsonConvert.DeserializeObject<ExpenseClaim>(await responseDetailsClaim.Content.ReadAsStringAsync());
//    return View(detailsClaim);
//}