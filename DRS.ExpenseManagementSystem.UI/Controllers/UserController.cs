using Microsoft.AspNetCore.Mvc;
using DRS.ExpenseManagementSystem.UI.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace DRS.ExpenseManagementSystem.UI.Controllers
{
    public class UserController : Controller
    {
        readonly IConfiguration configuration;
        readonly HttpClient client;

        public UserController(IConfiguration _configuration)
        {

            this.configuration = _configuration;
            this.client = new HttpClient
            {
                BaseAddress = new Uri(configuration["BaseUrl"]),
                Timeout = TimeSpan.FromMinutes(5)
            };
        }
    }
}
