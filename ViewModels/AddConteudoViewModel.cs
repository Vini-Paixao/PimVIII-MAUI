using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PimVIII.MauiCreator.Models;
using PimVIII.MauiCreator.Services;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;

namespace PimVIII.MauiCreator.ViewModels
{
    // 1. Herda de ObservableObject
    public partial class AddConteudoViewModel : ObservableObject
    {
        private readonly ConteudoService _conteudoService;

        // 2. Propriedades que serão "ligadas" (Bind) aos Entries do XAML
        [ObservableProperty]
        private string _titulo;

        [ObservableProperty]
        private string _tipo;

        public AddConteudoViewModel(ConteudoService conteudoService)
        {
            _conteudoService = conteudoService;
        }

        // 3. Comando que o botão "Salvar" irá chamar
        [RelayCommand]
        private async Task SaveConteudoAsync()
        {
            if (string.IsNullOrWhiteSpace(Titulo) || string.IsNullOrWhiteSpace(Tipo))
            {
                await Application.Current.MainPage.DisplayAlert("Campos Vazios", "Por favor, preencha o Título e o Tipo do conteúdo.", "OK");
                return;
            }

            // Cria o novo objeto Conteudo
            var novoConteudo = new Conteudo
            {
                Titulo = Titulo,
                Tipo = Tipo,
                CriadorID = 1 // Hardcodado para o protótipo
            };

            // 4. Envia para a API
            bool sucesso = await _conteudoService.AddConteudoAsync(novoConteudo);

            if (sucesso)
            {
                // 5. Se salvar, navega de volta para a página anterior (a lista)
                await Shell.Current.GoToAsync("..");
            }
            else
            {
                // Alerta de erro de API
                await Application.Current.MainPage.DisplayAlert("Erro de API", "Não foi possível salvar o conteúdo. Verifique a API.", "OK");
            }
        }
    }
}