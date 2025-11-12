
using PimVIII.MauiCreator.Views;

namespace PimVIII.MauiCreator
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            
            // Isso diz ao MAUI: "Quando alguém pedir pela rota 'AddConteudoPage', carregue a classe AddConteudoPage e assim por diante"
            Routing.RegisterRoute(nameof(AddConteudoPage), typeof(AddConteudoPage));
            Routing.RegisterRoute(nameof(AddPlaylistPage), typeof(AddPlaylistPage));
            Routing.RegisterRoute(nameof(PlaylistDetailsPage), typeof(PlaylistDetailsPage));
        }
    }
}
