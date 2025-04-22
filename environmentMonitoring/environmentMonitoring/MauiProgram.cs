using Microsoft.Extensions.Logging;
using environmentMonitoring.Database.Data;
using environmentMonitoring.Views;
using environmentMonitoring.ViewModels;
using environmentMonitoring.Services;


namespace environmentMonitoring;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

			builder.Services.AddDbContext<EnvironmentAppDbContext>();
			builder.Services.AddSingleton<IDiaglogService, DiaglogService>();

			builder.Services.AddSingleton<IReadDataService, UserService>();
			builder.Services.AddSingleton<IUpdateDataService, UserService>();
			builder.Services.AddSingleton<IValidationService, UserService>();

			builder.Services.AddSingleton<IUserSessionService, UserSessionService>();


			builder.Services.AddSingleton<IReadDataService, SensorService>();
			builder.Services.AddSingleton<IUpdateDataService, SensorService>();

			builder.Services.AddSingleton<RolePermissionService, RolePermissionService>();

			builder.Services.AddSingleton<UserService, UserService>();


			builder.Services.AddSingleton<LoginViewModel>();
			builder.Services.AddTransient<LogInPage>();

			builder.Services.AddSingleton<HomeViewModel>();
			builder.Services.AddTransient<HomePage>();	

			builder.Services.AddSingleton<AdminPanelViewModel>();
			builder.Services.AddTransient<AdminPanelPage>();

			builder.Services.AddSingleton<ManageRolesViewModel>();
			builder.Services.AddTransient<ManageRolesPage>();

			builder.Services.AddSingleton<RoleViewModel>();
			builder.Services.AddTransient<RolePage>();

			builder.Services.AddSingleton<UserViewModel>();
			builder.Services.AddSingleton<ListUsersForRoleAssignmentViewModel>();

			builder.Services.AddSingleton<AssignRoleViewModel>();
			
			builder.Services.AddTransient<ListUsersForRoleAssignmentPage>();
			builder.Services.AddTransient<AssignRolePage>();

			builder.Services.AddSingleton<ManageRolePermissionsViewModel>();
			builder.Services.AddSingleton<PermissionViewModel>();
			builder.Services.AddTransient<ManageRolePermissionsPage>();

#if DEBUG
		builder.Logging.AddDebug();
#endif

		return builder.Build();

	}

}


