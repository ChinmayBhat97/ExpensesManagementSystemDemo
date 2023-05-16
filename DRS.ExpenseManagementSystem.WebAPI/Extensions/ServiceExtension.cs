using DRS.ExpenseManagementSystem.Abstraction.Repository;
using DRS.ExpenseManagementSystem.Repository;
using DRS.ExpenseManagementSystem.Repository.Repository;
using DRS.ExpenseManagementSystem.WebAPI.Models;

namespace DRS.ExpenseManagementSystem.WebAPI.Extensions
{
    public static class ServiceExtension
    {



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
    }
}