// Importações necessárias
using CommunityToolkit.Mvvm.ComponentModel; // <-- Do pacote NuGet
using PimVIII.MauiCreator.Models;
using PimVIII.MauiCreator.Services;
using System.Collections.ObjectModel; // <-- Importante!
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Input;
using PimVIII.MauiCreator.Views;

namespace PimVIII.MauiCreator.ViewModels
{
    // 1. Herda de ObservableObject para notificar a UI
    public partial class ManageContentViewModel : ObservableObject
    {
        private readonly ConteudoService _conteudoService;

        // 2. Esta é a coleção que o XAML irá "observar"
        [ObservableProperty] // <-- Isso cria a propriedade "Conteudos"
        private ObservableCollection<Conteudo> _conteudos;

        // O construtor recebe o serviço via Injeção de Dependência
        public ManageContentViewModel(ConteudoService conteudoService)
        {
            _conteudoService = conteudoService;
            _conteudos = new ObservableCollection<Conteudo>();

            // Inicia o carregamento dos dados assim que o ViewModel é criado
            // Usamos _ = para não bloquear o construtor
            _ = LoadConteudosAsync();
        }

        // 3. Método para buscar os dados da API
        private async Task LoadConteudosAsync()
        {
            var conteudosDaApi = await _conteudoService.GetConteudosAsync();

            if (conteudosDaApi != null && conteudosDaApi.Count > 0)
            {
                // Limpa a lista atual e adiciona os novos dados
                Conteudos.Clear();
                foreach (var conteudo in conteudosDaApi)
                {
                    Conteudos.Add(conteudo);
                }
            }
        }

        // 4. Comando para navegar até a página de Adicionar
        [RelayCommand]
        private async Task GoToAddPageAsync()
        {
            // Usa o Shell Navigation para ir até a página registrada
            await Shell.Current.GoToAsync(nameof(AddConteudoPage));
        }
    }
}