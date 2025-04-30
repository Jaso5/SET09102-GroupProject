using Microsoft.Maui;
using Microsoft.Maui.Hosting;

namespace environmentMonitoring;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>() // Ensure this references your App class
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                fonts.AddFont("CustomFont-Regular.ttf", "CustomFontRegular");
            });

        return builder.Build();
    }
}