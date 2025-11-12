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
            builder.Services.AddSingleton<PlaylistService>();

            // Registro das Páginas (Views)
            builder.Services.AddTransient<ManageContentPage>();
            builder.Services.AddTransient<AddConteudoPage>();
            builder.Services.AddTransient<ManagePlaylistsPage>();
            builder.Services.AddTransient<AddPlaylistPage>();
            builder.Services.AddTransient<PlaylistDetailsPage>();
            builder.Services.AddTransient<AnalyticsPage>();

            // Registro dos ViewModels
            builder.Services.AddTransient<ManageContentViewModel>();
            builder.Services.AddTransient<AddConteudoViewModel>();
            builder.Services.AddTransient<ManagePlaylistsViewModel>();
            builder.Services.AddTransient<AddPlaylistViewModel>();
            builder.Services.AddTransient<PlaylistDetailsViewModel>();
            builder.Services.AddTransient<AnalyticsViewModel>();

            return builder.Build();
        }
    }
}
