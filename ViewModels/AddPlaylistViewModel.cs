using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using PimVIII.MauiCreator.Messages;
using PimVIII.MauiCreator.Models;
using PimVIII.MauiCreator.Services;
using System.Threading.Tasks;

namespace PimVIII.MauiCreator.ViewModels
{
    // 1. O Atributo "QueryProperty" escuta a navegação
    [QueryProperty(nameof(PlaylistParaEditar), "PlaylistParaEditar")]
    public partial class AddPlaylistViewModel : ObservableObject
    {
        private readonly PlaylistService _playlistService;

        [ObservableProperty]
        private string _nome;

        // Guarda o ID (se for 0, é novo. Se > 0, é edição)
        private int _playlistID;

        // 2. Propriedade que recebe o objeto da navegação
        private Playlist _playlistParaEditar;
        public Playlist PlaylistParaEditar
        {
            get => _playlistParaEditar;
            set
            {
                SetProperty(ref _playlistParaEditar, value);

                // 3. Preenche o formulário quando o objeto chega
                if (value != null)
                {
                    Nome = value.Nome;
                    _playlistID = value.ID;
                }
            }
        }

        public AddPlaylistViewModel(PlaylistService playlistService)
        {
            _playlistService = playlistService;
        }

        [RelayCommand]
        private async Task SavePlaylistAsync()
        {
            if (string.IsNullOrWhiteSpace(Nome))
            {
                await Application.Current.MainPage.DisplayAlert("Campo Vazio", "...", "OK");
                return;
            }

            bool sucesso = false;

            // Cria o objeto
            var playlist = new Playlist
            {
                ID = _playlistID,
                Nome = Nome,
                UsuarioID = 2 // Hardcodado (protótipo)
            };

            // 4. Decide se é Adicionar (POST) ou Atualizar (PUT)
            if (_playlistID == 0)
            {
                sucesso = await _playlistService.AddPlaylistAsync(playlist);
            }
            else
            {
                sucesso = await _playlistService.UpdatePlaylistAsync(playlist);
            }

            if (sucesso)
            {
                // Limpa tudo
                Nome = string.Empty;
                _playlistID = 0;
                PlaylistParaEditar = null;

                WeakReferenceMessenger.Default.Send(new ConteudoSavedMessage()); // Reutilizamos a mensagem
                await Shell.Current.GoToAsync("..");
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Erro de API", "Não foi possível salvar a playlist.", "OK");
            }
        }
    }
}