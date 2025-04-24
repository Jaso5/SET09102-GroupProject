using Microsoft.UI.Xaml;

namespace environmentMonitoring.WinUI;

public partial class MauiProgram : MauiWinUIApplication
{
    public MauiProgram()
    {
        this.InitializeComponent();
    }

    protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();

}