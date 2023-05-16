using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;

using Microsoft.EntityFrameworkCore;

namespace DRS.ExpenseManagementSystem.UI
{
    public class Startup
    {
        public IConfiguration _configuration1 { get; }
        public Startup(IConfiguration configuration)
        {
            _configuration1= configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews()
                .AddControllersAsServices()
                .AddRazorRuntimeCompilation();
            services.AddSession(options =>
            {
                options.Cookie.Name = ".WebPortal.Session";
                options.IdleTimeout = TimeSpan.FromHours(3);
                options.Cookie.IsEssential = true;
            });
            
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }


            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseSession();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Login}/{action=Index}/{id?}");
            });

        }
    }
}
