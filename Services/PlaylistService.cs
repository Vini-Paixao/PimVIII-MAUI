using System.Net.Http.Json;
using PimVIII.MauiCreator.Models;

namespace PimVIII.MauiCreator.Services
{
    public class PlaylistService
    {
        private readonly HttpClient _httpClient;
        private const string BaseApiUrl = "https://pimviii.marcuspaixao.com.br";

        public PlaylistService()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(BaseApiUrl);
        }

        // 1. Método para buscar todas as playlists
        public async Task<List<Playlist>> GetPlaylistsAsync()
        {
            try
            {
                // ATENÇÃO: Verifique se o endpoint na sua API é "/api/Playlists"
                HttpResponseMessage response = await _httpClient.GetAsync("/api/Playlists");

                if (response.IsSuccessStatusCode)
                {
                    var playlists = await response.Content.ReadFromJsonAsync<List<Playlist>>();
                    return playlists ?? new List<Playlist>();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao buscar playlists: {ex.Message}");
            }
            return new List<Playlist>();
        }

        // 2. Método para Adicionar (POST) uma nova playlist
        public async Task<bool> AddPlaylistAsync(Playlist playlist)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.PostAsJsonAsync("/api/Playlists", playlist);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao adicionar playlist: {ex.Message}");
                return false;
            }
        }

        // 3. Método para Atualizar (PUT) uma playlist
        public async Task<bool> UpdatePlaylistAsync(Playlist playlist)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.PutAsJsonAsync($"/api/Playlists/{playlist.ID}", playlist);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao atualizar playlist: {ex.Message}");
                return false;
            }
        }

        // 4. Método para Excluir (DELETE) uma playlist
        public async Task<bool> DeletePlaylistAsync(int id)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.DeleteAsync($"/api/Playlists/{id}");
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao excluir playlist: {ex.Message}");
                return false;
            }
        }
    }
}