using CommunityToolkit.Mvvm.ComponentModel;
using PimVIII.MauiCreator.Models;
using PimVIII.MauiCreator.Services;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Input; // <-- Adicione o using
using PimVIII.MauiCreator.Views;
using CommunityToolkit.Mvvm.Messaging;
using PimVIII.MauiCreator.Messages;
using System.Diagnostics;

namespace PimVIII.MauiCreator.ViewModels
{
    public partial class ManagePlaylistsViewModel : ObservableObject
    {
        private readonly PlaylistService _playlistService;

        [ObservableProperty]
        private ObservableCollection<Playlist> _playlists;

        public ManagePlaylistsViewModel(PlaylistService playlistService)
        {
            _playlistService = playlistService;
            _playlists = new ObservableCollection<Playlist>();

            _ = LoadPlaylistsAsync();

            WeakReferenceMessenger.Default.Register<ConteudoSavedMessage>(this, (recipient, message) =>
            {
                Debug.WriteLine("MENSAGEM RECEBIDA: Recarregando playlists...");

                // Recarrega a lista
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    await LoadPlaylistsAsync();
                });
            });
        }

        private async Task LoadPlaylistsAsync()
        {
            var items = await _playlistService.GetPlaylistsAsync();
            if (items != null)
            {
                Playlists.Clear();
                foreach (var item in items)
                {
                    Playlists.Add(item);
                }
            }
        }

        [RelayCommand]
        private async Task GoToAddPageAsync()
        {
            await Shell.Current.GoToAsync(nameof(AddPlaylistPage));
        }

        [RelayCommand]
        private async Task DeletePlaylistAsync(Playlist playlist)
        {
            if (playlist == null) return;

            bool confirma = await Application.Current.MainPage.DisplayAlert(
                "Excluir Playlist",
                $"Você tem certeza que deseja excluir '{playlist.Nome}'?",
                "Sim", "Não");

            if (confirma)
            {
                bool sucesso = await _playlistService.DeletePlaylistAsync(playlist.ID);
                if (sucesso)
                {
                    Playlists.Remove(playlist); // Remove da lista na tela
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Erro", "Não foi possível excluir a playlist.", "OK");
                }
            }
        }

        [RelayCommand]
        private async Task GoToDetails(Playlist playlist)
        {
            if (playlist == null) return;

            // Envia o objeto 'Playlist' para a página de detalhes
            var navigationParameters = new Dictionary<string, object>
    {
        { "PlaylistSelecionada", playlist }
    };

            await Shell.Current.GoToAsync(nameof(PlaylistDetailsPage), navigationParameters);
        }
    }
}