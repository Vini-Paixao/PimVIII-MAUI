using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PimVIII.MauiCreator.Models;
using PimVIII.MauiCreator.Services;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace PimVIII.MauiCreator.ViewModels
{
    [QueryProperty(nameof(PlaylistSelecionada), "PlaylistSelecionada")]
    public partial class PlaylistDetailsViewModel : ObservableObject
    {
        private readonly ConteudoService _conteudoService;
        private readonly PlaylistService _playlistService;

        [ObservableProperty]
        private string _playlistNome;
        
        // Lista 1: Conteúdos que JÁ ESTÃO na playlist
        [ObservableProperty]
        private ObservableCollection<Conteudo> _conteudoNaPlaylist;
        
        // Lista 2: Conteúdos que PODEM SER ADICIONADOS
        [ObservableProperty]
        private ObservableCollection<Conteudo> _conteudoDisponivel;

        private Playlist _playlistSelecionada;
        public Playlist PlaylistSelecionada
        {
            get => _playlistSelecionada;
            set
            {
                SetProperty(ref _playlistSelecionada, value);
                
                if (value != null)
                {
                    PlaylistNome = value.Nome;
                    
                    // Inicia o carregamento dos dados
                    _ = LoadDataAsync(); 
                }
            }
        }

        public PlaylistDetailsViewModel(ConteudoService conteudoService, PlaylistService playlistService)
        {
            _conteudoService = conteudoService;
            _playlistService = playlistService;
            
            _conteudoNaPlaylist = new ObservableCollection<Conteudo>();
            _conteudoDisponivel = new ObservableCollection<Conteudo>();
        }

        private async Task LoadDataAsync()
        {
            try
            {
                // Limpa as listas
                ConteudoNaPlaylist.Clear();
                ConteudoDisponivel.Clear();

                // 1. Pega TODOS os conteúdos disponíveis no sistema
                var todosConteudos = await _conteudoService.GetConteudosAsync();

                // 2. Pega os IDs dos conteúdos que já estão na playlist
                // (Assumindo que a API preenche a lista 'Conteudos' do objeto 'Playlist')
                var idsNaPlaylist = new HashSet<int>(
                    _playlistSelecionada.Conteudos?.Select(c => c.ID) ?? Enumerable.Empty<int>()
                );

                // 3. Separa os conteúdos nas duas listas
                foreach (var item in todosConteudos)
                {
                    if (idsNaPlaylist.Contains(item.ID))
                    {
                        ConteudoNaPlaylist.Add(item);
                    }
                    else
                    {
                        ConteudoDisponivel.Add(item);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Erro ao carregar dados dos conteúdos: {ex.Message}");
            }
        }

        [RelayCommand]
        private void AdicionarItem(Conteudo conteudo)
        {
            if (conteudo == null) return;

            // Move da lista Disponível para a lista NaPlaylist
            if (ConteudoDisponivel.Remove(conteudo))
            {
                ConteudoNaPlaylist.Add(conteudo);
            }
        }

        [RelayCommand]
        private void RemoverItem(Conteudo conteudo)
        {
            if (conteudo == null) return;

            // Move da lista NaPlaylist para a lista Disponível
            if (ConteudoNaPlaylist.Remove(conteudo))
            {
                ConteudoDisponivel.Add(conteudo);
            }
        }


        [RelayCommand]
        private async Task SalvarAlteracoes()
        {
            try
            {
                // 1. Atualiza o objeto da playlist com a nova lista de conteúdos
                // A API espera o *objeto* Playlist, e o EF Core 
                // irá gerir a tabela de associação (PlaylistConteudo).
                _playlistSelecionada.Conteudos = new List<Conteudo>(ConteudoNaPlaylist);

                // 2. Envia o objeto 'Playlist' atualizado via PUT
                bool sucesso = await _playlistService.UpdatePlaylistAsync(_playlistSelecionada);

                if (sucesso)
                {
                    await Application.Current.MainPage.DisplayAlert("Sucesso", "Playlist atualizada.", "OK");
                    await Shell.Current.GoToAsync(".."); // Volta para a lista de playlists
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Erro", "Não foi possível salvar as alterações na API.", "OK");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Erro ao salvar alterações da playlist: {ex.Message}");
                await Application.Current.MainPage.DisplayAlert("Erro", "Ocorreu um erro local ao salvar.", "OK");
            }
        }
    }
}