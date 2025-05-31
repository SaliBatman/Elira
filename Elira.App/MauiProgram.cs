using Microsoft.Extensions.Logging;
using Elira.App.Shared.Services;
using Elira.App.Services;

namespace Elira.App;

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
                fonts.AddFont("Vazirmatn-Bold.ttf", "Vazir");
                fonts.AddFont("ltim.ttf", "Ltim");


            });

        // Add device-specific services used by the Elira.App.Shared project
        builder.Services.AddSingleton<IFormFactor, FormFactor>();

        builder.Services.AddMauiBlazorWebView();

#if DEBUG
        builder.Services.AddBlazorWebViewDeveloperTools();
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}
