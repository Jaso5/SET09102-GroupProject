using Microsoft.Extensions.Logging;
using environmentMonitoring.Database.Data;
using environmentMonitoring.Views;
using environmentMonitoring.ViewModels;


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

#if DEBUG
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}


