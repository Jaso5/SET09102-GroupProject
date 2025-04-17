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

			builder.Services.AddSingleton<LogInPage>();
			builder.Services.AddTransient<LoginViewModel>();

			builder.Services.AddSingleton<HomePage>();
			builder.Services.AddTransient<HomeViewModel>();

			builder.Services.AddSingleton<IDiaglogService, DiaglogService>();

			builder.Services.AddSingleton<IReadDataService, UserService>();
			builder.Services.AddSingleton<IUpdateDataService, UserService>();
			builder.Services.AddSingleton<IValidationService, UserService>();


			builder.Services.AddSingleton<IReadDataService, SensorService>();
			builder.Services.AddSingleton<IUpdateDataService, SensorService>();
			
			

#if DEBUG
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}


