
using PimVIII.MauiCreator.Views;

namespace PimVIII.MauiCreator
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            
            // Isso diz ao MAUI: "Quando alguém pedir pela rota 'AddConteudoPage', carregue a classe AddConteudoPage."
            Routing.RegisterRoute(nameof(AddConteudoPage), typeof(AddConteudoPage));
        }
    }
}
