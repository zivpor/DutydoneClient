using DutydoneClient.ViewModels;
using DutydoneClient.Views;
using Microsoft.Extensions.Logging;

namespace DutydoneClient
{
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
                    fonts.AddFont("Alloha.otf", "Alloha");
                    fonts.AddFont("yarden-regular-alefalefalef.oft", "Yarden");
                    fonts.AddFont("Super Magnum.otf", "SM");
                });
            builder.Services.AddSingleton<Login>();
            builder.Services.AddSingleton<LoginViewModel>();
            builder.Services.AddSingleton<Register>();
            builder.Services.AddSingleton<RegisterViewModel>();


#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
