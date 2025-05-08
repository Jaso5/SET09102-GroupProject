using Microsoft.Extensions.Logging;
using environmentMonitoring.Database.Data;
using environmentMonitoring.Views;
using environmentMonitoring.ViewModels;
using environmentMonitoring.Services;
using LiveChartsCore.SkiaSharpView.Maui;
using SkiaSharp.Views.Maui.Controls.Hosting;

namespace environmentMonitoring;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseSkiaSharp()
			.UseLiveCharts()
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

		builder.Services.AddDbContext<EnvironmentAppDbContext>();
		builder.Services.AddSingleton<IDiaglogService, DiaglogService>();

		builder.Services.AddSingleton<IncidentReportService, IncidentReportService>();

		builder.Services.AddSingleton<IReadDataService, UserService>();
		builder.Services.AddSingleton<IUpdateDataService, UserService>();
		builder.Services.AddSingleton<IValidationService, UserService>();

		builder.Services.AddSingleton<IUserSessionService, UserSessionService>();


		builder.Services.AddSingleton<IReadDataService, SensorService>();
		builder.Services.AddSingleton<IUpdateDataService, SensorService>();

		builder.Services.AddSingleton<RolePermissionService, RolePermissionService>();

		builder.Services.AddSingleton<ReportService>();


		builder.Services.AddSingleton<UserService, UserService>();
		builder.Services.AddSingleton<PermissionService, PermissionService>();

		builder.Services.AddSingleton<LoginViewModel>();
		builder.Services.AddTransient<LogInPage>();

		builder.Services.AddSingleton<HomeViewModel>();
		builder.Services.AddTransient<HomePage>();

		builder.Services.AddSingleton<AccountViewModel>();
		builder.Services.AddTransient<AccountPage>();

		builder.Services.AddSingleton<AdminPanelViewModel>();
		builder.Services.AddTransient<AdminPanelPage>();

		builder.Services.AddSingleton<ManageRolesViewModel>();
		builder.Services.AddTransient<ManageRolesPage>();

		builder.Services.AddSingleton<RoleViewModel>();
		builder.Services.AddTransient<RolePage>();

		builder.Services.AddSingleton<SensorViewModel>();
		builder.Services.AddTransient<SensorPage>();
			
		builder.Services.AddTransient<ReportPage>();

		builder.Services.AddSingleton<SensorListViewModel>();
		builder.Services.AddSingleton<SensorDisplayModel>();
		builder.Services.AddTransient<SensorListPage>();

		builder.Services.AddSingleton<UserViewModel>();
		builder.Services.AddSingleton<ListUsersForRoleAssignmentViewModel>();

		builder.Services.AddSingleton<AssignRoleViewModel>();
			
		builder.Services.AddTransient<ListUsersForRoleAssignmentPage>();
		builder.Services.AddTransient<AssignRolePage>();

		builder.Services.AddSingleton<ManageRolePermissionsViewModel>();
		builder.Services.AddSingleton<PermissionViewModel>();
		builder.Services.AddTransient<ManageRolePermissionsPage>();

		builder.Services.AddSingleton<IncidentReportViewModel>();
		builder.Services.AddTransient<IncidentReportPage>();

		builder.Services.AddSingleton<IncidentReportEditViewModel>();
		builder.Services.AddTransient<IncidentReportEditPage>();

		builder.Services.AddSingleton<SensorDataService, SensorDataService>();

		builder.Services.AddTransient<VirtualSensorSettings>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

		return builder.Build();
	}

}


