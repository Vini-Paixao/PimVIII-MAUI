// Importações necessárias
using CommunityToolkit.Mvvm.ComponentModel;
using PimVIII.MauiCreator.Models;
using PimVIII.MauiCreator.Services;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Input;
using PimVIII.MauiCreator.Views;
using CommunityToolkit.Mvvm.Messaging;
using PimVIII.MauiCreator.Messages;
using System.Diagnostics; // (Para o Debug.WriteLine)

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

            // 1. Inicia o carregamento inicial (isso já existia)
            _ = LoadConteudosAsync();

            // 2. REGISTRA O "OUVINTE"
            // Fica escutando por mensagens do tipo 'ConteudoSavedMessage'
            WeakReferenceMessenger.Default.Register<ConteudoSavedMessage>(this, (recipient, message) =>
            {
                // Quando a mensagem chegar, executa este código:
                Debug.WriteLine("MENSAGEM RECEBIDA: Recarregando conteúdos...");

                // Recarrega a lista. 
                // Usamos MainThread.BeginInvokeOnMainThread para garantir
                // que a atualização da coleção (UI) ocorra na thread principal.
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    await LoadConteudosAsync();
                });
            });
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

        // 5. Comando para excluir um conteúdo
        [RelayCommand]
        private async Task DeleteConteudoAsync(Conteudo conteudo)
        {
            if (conteudo == null) return;

            // Pergunta ao usuário se ele tem certeza
            bool confirma = await Application.Current.MainPage.DisplayAlert(
                "Excluir Conteúdo",
                $"Você tem certeza que deseja excluir '{conteudo.Titulo}'?",
                "Sim", "Não");

            if (confirma)
            {
                bool sucesso = await _conteudoService.DeleteConteudoAsync(conteudo.ID);
                if (sucesso)
                {
                    // Remove o item da lista na tela
                    Conteudos.Remove(conteudo);
                }
                else
                {
                    // Mostra erro
                    await Application.Current.MainPage.DisplayAlert("Erro", "Não foi possível excluir o item.", "OK");
                }
            }
        }

        // 6. Comando para editar um conteúdo
        [RelayCommand]
        private async Task EditConteudoAsync(Conteudo conteudo)
        {
            if (conteudo == null) return;

            // 1. Cria um dicionário para os dados de navegação
            var navigationParameters = new Dictionary<string, object>
            {
                { "ConteudoParaEditar", conteudo } // A chave é "ConteudoParaEditar"
            };

            // 2. Navega para a página E envia o dicionário
            await Shell.Current.GoToAsync(nameof(AddConteudoPage), navigationParameters);
        }
    }
}