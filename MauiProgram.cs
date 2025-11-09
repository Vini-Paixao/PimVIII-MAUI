using Microsoft.Extensions.Logging;
using PimVIII.MauiCreator.Views; 
using PimVIII.MauiCreator.Services;
using PimVIII.MauiCreator.ViewModels;

namespace PimVIII.MauiCreator
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
                });

            // Registro dos Serviços (Padrão Singleton para serviços de API)
            builder.Services.AddSingleton<ConteudoService>();

            // Registro das Páginas (Views)
            builder.Services.AddTransient<ManageContentPage>();
            builder.Services.AddTransient<AddConteudoPage>();

            // Registro dos ViewModels
            builder.Services.AddTransient<ManageContentViewModel>();
            builder.Services.AddTransient<AddConteudoViewModel>();

            return builder.Build();
        }
    }
}
