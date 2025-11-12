using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PimVIII.MauiCreator.Models;
using PimVIII.MauiCreator.Services;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;
using CommunityToolkit.Mvvm.Messaging;
using PimVIII.MauiCreator.Messages;

namespace PimVIII.MauiCreator.ViewModels
{

    // 1. Herda de ObservableObject
    [QueryProperty(nameof(ConteudoParaEditar), "ConteudoParaEditar")]
    public partial class AddConteudoViewModel : ObservableObject
    {
        private readonly ConteudoService _conteudoService;

        // 2. Propriedades que serão "ligadas" (Bind) aos Entries do XAML
        [ObservableProperty]
        private string _titulo;

        [ObservableProperty]
        private string _tipo;

        // Propriedade para guardar o ID (se for edição)
        private int _conteudoID;

        private Conteudo _conteudoParaEditar;
        public Conteudo ConteudoParaEditar
        {
            get => _conteudoParaEditar;
            set
            {
                SetProperty(ref _conteudoParaEditar, value);

                // 3. QUANDO O OBJETO CHEGAR, PREENCHE O FORMULÁRIO
                if (value != null)
                {
                    Titulo = value.Titulo;
                    Tipo = value.Tipo;
                    _conteudoID = value.ID; // Guarda o ID
                }
            }
        }

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
                await Application.Current.MainPage.DisplayAlert("Campos Vazios", "...", "OK");
                return;
            }

            bool sucesso = false;

            // Cria o objeto com os dados da tela
            var conteudo = new Conteudo
            {
                ID = _conteudoID, // Se for 0, é novo. Se for > 0, é edição.
                Titulo = Titulo,
                Tipo = Tipo,
                CriadorID = 1 // Hardcodado para o protótipo
            };

            if (_conteudoID == 0)
            {
                // 1. LÓGICA DE ADICIONAR (POST)
                sucesso = await _conteudoService.AddConteudoAsync(conteudo);
            }
            else
            {
                // 2. LÓGICA DE ATUALIZAR (PUT)
                sucesso = await _conteudoService.UpdateConteudoAsync(conteudo);
            }

            if (sucesso)
            {
                // Limpa os campos para a próxima vez
                Titulo = string.Empty;
                Tipo = string.Empty;
                _conteudoID = 0;
                ConteudoParaEditar = null;

                // Dispara a mensagem (para atualizar a lista)
                WeakReferenceMessenger.Default.Send(new ConteudoSavedMessage());

                // Navega de volta
                await Shell.Current.GoToAsync("..");
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Erro de API", "Não foi possível salvar...", "OK");
            }
        }
    }
}