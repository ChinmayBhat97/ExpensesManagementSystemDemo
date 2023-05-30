using DRS.ExpenseManagementSystem.UI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace DRS.ExpenseManagementSystem.UI.Controllers
{
    public class ClaimStatusController : Controller
    {
        readonly IConfiguration configuration;
        readonly HttpClient client;

        public ClaimStatusController(IConfiguration _configuration)
        {

            this.configuration = _configuration;
            this.client = new HttpClient
            {
                BaseAddress = new Uri(configuration["BaseUrl"]),
                Timeout = TimeSpan.FromMinutes(5)
            };
        }
       
     // [Authorize(Roles = "4")]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            HttpResponseMessage responseHomePage = await client.GetAsync(client.BaseAddress + "ClaimStatus");
            if (responseHomePage.IsSuccessStatusCode)
            {
                var responseContent = await responseHomePage.Content.ReadAsStringAsync();
                var model = JsonConvert.DeserializeObject<List<ClaimStatusViewModel>>(responseContent);
                return View(model);
            }
            else
            {

                return View();
            }
        }

       // [Authorize(Roles = "4")]

        [HttpGet("ClaimStatus/CreateClaimStatus")]
        public async Task<IActionResult> CreateClaimStatusAsync()
        {
            HttpResponseMessage responseCreateDepartment = await client.GetAsync(client.BaseAddress + $"ClaimStatus");
            return View();
        }

     //   [Authorize(Roles = "4")]
        [HttpPost("ClaimStatus/CreateClaimStatus")]
        public async Task<IActionResult> CreateClaimStatus(ClaimStatusViewModel claimStatusViewModel)
        {
            if (ModelState.IsValid)
            {
                var myContent = JsonConvert.SerializeObject(claimStatusViewModel);
                var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                HttpResponseMessage createClaimStatus = await client.PostAsync(client.BaseAddress + $"ClaimStatus", byteContent);

                return RedirectToAction("Index");
            }

            return View(claimStatusViewModel);
        }

      //  [Authorize(Roles = "4")]
        [HttpGet("ClaimStatus/EditClaimStatus/{id}")]
        public async Task<IActionResult> EditClaimStatus(int id)
        {
            HttpResponseMessage responseEditClaimStatus = await client.GetAsync(client.BaseAddress + $"ClaimStatus/{id}");
            var EditClaimStatus = JsonConvert.DeserializeObject<ClaimStatusViewModel>(await responseEditClaimStatus.Content.ReadAsStringAsync());
            return View(EditClaimStatus);
        }

       // [Authorize(Roles = "4")]
        [HttpPost("ClaimStatus/EditClaimStatus/{id}")]
        public async Task<IActionResult> EditClaimStatus(int id, ClaimStatusViewModel claimStatusViewModel)
        {
            if (ModelState.IsValid)
            {
                var myContent = JsonConvert.SerializeObject(claimStatusViewModel);
                var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                HttpResponseMessage response = await client.PutAsync(client.BaseAddress + $"ClaimStatus/{claimStatusViewModel.Id}", byteContent);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }

            return View(claimStatusViewModel);
        }

      //  [Authorize(Roles = "4")]
        [HttpGet("ClaimStatus/DetailsClaimStatus/{id}")]
        public async Task<IActionResult> DetailsByClaimStatusID(int id)
        {
            HttpResponseMessage responseDetailsClaimStatus = await client.GetAsync(client.BaseAddress + $"ClaimStatus/{id}");
            var detailsClaimStatus = JsonConvert.DeserializeObject<ClaimStatusViewModel>(await responseDetailsClaimStatus.Content.ReadAsStringAsync());
            return View(detailsClaimStatus);
        }
    }
}
