using DRS.ExpenseManagementSystem.Abstraction.Errors;
using DRS.ExpenseManagementSystem.Abstraction.Repository;
using DRS.ExpenseManagementSystem.Abstraction.Services;
using DRS.ExpenseManagementSystem.Business.Services;
using DRS.ExpenseManagementSystem.Repository;
using DRS.ExpenseManagementSystem.Repository.Repository;
using DRS.ExpenseManagementSystem.WebAPI.Models;
using Microsoft.AspNetCore.Diagnostics;
using Serilog;
using System.Net;

namespace DRS.ExpenseManagementSystem.WebAPI.Extensions
{
    public static class ServiceExtension
    {

        public static void ConfigureExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(error =>
            {
                error.Run(async context =>
                {
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    context.Response.ContentType = "application/json";
                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    context.Response.StatusCode = contextFeature.Error.GetType().Name switch
                    {
                        "ArgumentException" => (int)HttpStatusCode.BadRequest,
                        "ArgumentNullException" => (int)HttpStatusCode.BadRequest,
                        "ArgumentOutOfRangeException" => (int)HttpStatusCode.BadRequest,
                        "InvalidOperationException" => (int)HttpStatusCode.BadRequest,

                        "NotImplementedException" => (int)HttpStatusCode.NotImplemented,

                        "IOException" => (int)HttpStatusCode.InternalServerError,
                        "NullReferenceException" => (int)HttpStatusCode.InternalServerError,
                        "IndexOutOfRangeException" => (int)HttpStatusCode.InternalServerError,
                        "AccessViolationException" => (int)HttpStatusCode.InternalServerError,
                        "StackOverflowException" => (int)HttpStatusCode.InternalServerError,
                        "OutOfMemoryException" => (int)HttpStatusCode.InternalServerError,
                        "SqlException" => (int)HttpStatusCode.InternalServerError,
                        "ApplicationException" => (int)HttpStatusCode.InternalServerError,
                        "SystemException" => (int)HttpStatusCode.InternalServerError,
                        "Exception" => (int)HttpStatusCode.InternalServerError,

                        _ => (int)HttpStatusCode.ServiceUnavailable
                    };
                    if (contextFeature != null)
                    {
                        Log.Error($"Error in the {contextFeature.Error}");
                        await context.Response.WriteAsync(new Error
                        {
                            StatusCode = context.Response.StatusCode,
                            Message = context.Response.StatusCode switch
                            {
                                StatusCodes.Status400BadRequest => "Invalid Argument(s) supplied",
                                StatusCodes.Status401Unauthorized => "Unauthorized request",
                                StatusCodes.Status404NotFound => "Not Found",
                                StatusCodes.Status403Forbidden => "Forbidden request",
                                StatusCodes.Status500InternalServerError => "Internal Server Error. Please try again later.",
                                StatusCodes.Status501NotImplemented => "Service not implemented",
                                StatusCodes.Status503ServiceUnavailable => "Service not available. Please try again later.",
                                _ => "Internal Server Error. Please reachout for support."
                            }
                        }.ToString());
                    }
                });
            });
        }

        public static void RegisterRepositories(this IServiceCollection services)
        {
            services.AddTransient<ExpensesManagementSystem_UpdatedContext>();
            services.AddTransient<IClaimStatusRepository, ClaimStatusRepository>();
            services.AddTransient<IDepartmentRepository, DepartmentRepository>();
            services.AddTransient<IEmployeeRepository, EmployeeRepository>();
            services.AddTransient<IExpenseCategoryRepository, ExpenseCategoryRepository>();
            services.AddTransient<IExpenseClaimRepository, ExpenseClaimRepository>();
            services.AddTransient<IIndividualExpenditureRepository, IndividualExpenditureRepository>();
            services.AddTransient<IProjectRepository, ProjectRepository>();
            services.AddTransient<IUserRepository, UserRepository>();

        }

        public static void RegisterService(this IServiceCollection services)
        {
            services.AddTransient<ExpensesManagementSystem_UpdatedContext>();
            services.AddTransient<IClaimStatusService, ClaimStatusService>();
            services.AddTransient<IDepartmentService, DepartmentService>();
            services.AddTransient<IEmployeeService, EmployeeService>();
            services.AddTransient<IExpenseCategoryServices, ExpenseCategoryService>();
            services.AddTransient<IExpenseClaimServices, ExpenseClaimService>();
            services.AddTransient<IIndividualExpenditureServices, IndividualExpenditureService>();
            services.AddTransient<IProjectService, ProjectService>();
            services.AddTransient<IUserService, UserService>();

        }
    }
}
