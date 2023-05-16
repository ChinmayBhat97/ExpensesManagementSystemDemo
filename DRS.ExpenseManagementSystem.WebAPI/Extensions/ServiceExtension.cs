using DRS.ExpenseManagementSystem.Abstraction.Repository;
using DRS.ExpenseManagementSystem.Repository.Repository;
using DRS.ExpenseManagementSystem.WebAPI.Models;

namespace DRS.ExpenseManagementSystem.WebAPI.Extensions
{
    public static class ServiceExtension
    {
    }

    public static void RegisterRepositories(this IServiceCollection services)
    {
        services.AddTransient<ExpensesManagementSystem_UpdatedContext>();
        services.AddTransient<IClaimStatusRepository, AnonymousDataRepository>();
        services.AddTransient<IDepartmentRepository, AppConfigRepository>();
        services.AddTransient<IBandwiseReadingRepository, BandwiseReadingRepository>();
        services.AddTransient<IImageRepository, ImageRepository>();
        services.AddTransient<ILocationRepository, LocationRepository>();
        services.AddTransient<IPlantConfigurationRepository, PlantConfigurationRepository>();
        services.AddTransient<IResultRepository, ResultRepository>();
        services.AddTransient<IThermalBandsConfigRepository, ThermalBandsConfigRepository>();
        services.AddTransient<IThermalZoneRepository, ThermalZoneRepository>();
        services.AddTransient<IAssetRepository, AssetRepository>();
        services.AddTransient<IWorkOrderRepository, WorkOrderRepository>();
        services.AddTransient<IFeedRateRepository, FeedRateRepository>();
        services.AddTransient<IUserRepository, UserRepository>();
        services.AddTransient<IThermalImageTaskRepository, ThermalImageTaskRepository>();
        services.AddTransient<IThermalImageRepository, ThermalImageRepository>();
        services.AddTransient<ITraceImageRepository, TraceImageRepository>();
        services.AddTransient<ITenantRepository, TenantRepository>();
        services.AddTransient<IForecastMaintenanceTrendChartRepository, ForecastMaintenanceTrendChartRepository>();

    }
}
