using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json; // Pacote System.Net.Http.Json
using System.Threading.Tasks;
using PimVIII.MauiCreator.Models;

namespace PimVIII.MauiCreator.Services
{
    public class ConteudoService
    {
        private readonly HttpClient _httpClient;
        
        private const string BaseApiUrl = "https://pimviii.marcuspaixao.com.br"; 

        public ConteudoService()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(BaseApiUrl);
        }

        // Método para buscar todos os conteúdos
        public async Task<List<Conteudo>> GetConteudosAsync()
        {
            List<Conteudo> conteudos = new List<Conteudo>();
            try
            {
                // Faz a chamada GET para /api/Conteudos
                HttpResponseMessage response = await _httpClient.GetAsync("/api/Conteudos");

                if (response.IsSuccessStatusCode)
                {
                    // Lê a resposta JSON e converte para a lista de Conteudos
                    conteudos = await response.Content.ReadFromJsonAsync<List<Conteudo>>();
                }
            }
            catch (Exception ex)
            {
                // Em um app real, trataríamos o erro
                Console.WriteLine($"Erro ao buscar conteúdos: {ex.Message}");
            }
            return conteudos;
        }

        // Método para Adicionar (POST) um novo conteúdo
        public async Task<bool> AddConteudoAsync(Conteudo conteudo)
        {
            try
            {
                // Faz a chamada POST para /api/Conteudos
                HttpResponseMessage response = await _httpClient.PostAsJsonAsync("/api/Conteudos", conteudo);

                // Retorna true se a API retornou 200 (OK) ou 201 (Created)
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao adicionar conteúdo: {ex.Message}");
                return false;
            }
        }

        // Método para Atualizar (PUT) um conteúdo
        public async Task<bool> UpdateConteudoAsync(Conteudo conteudo)
        {
            try
            {
                // Faz a chamada PUT para /api/Conteudos/{id}
                HttpResponseMessage response = await _httpClient.PutAsJsonAsync($"/api/Conteudos/{conteudo.ID}", conteudo);

                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao atualizar conteúdo: {ex.Message}");
                return false;
            }
        }

        // Método para Excluir (DELETE) um conteúdo
        public async Task<bool> DeleteConteudoAsync(int id)
        {
            try
            {
                // Faz a chamada DELETE para /api/Conteudos/{id}
                HttpResponseMessage response = await _httpClient.DeleteAsync($"/api/Conteudos/{id}");

                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao excluir conteúdo: {ex.Message}");
                return false;
            }
        }
    }
}