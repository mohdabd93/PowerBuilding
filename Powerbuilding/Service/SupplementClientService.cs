using Domain.Entities;
using System.Net.Http.Json;

namespace Powerbuilding.Service
{
    public class SupplementClientService
    {
        private readonly HttpClient m_httpClient;

        public SupplementClientService(IHttpClientFactory httpClientFactory)
        {
            m_httpClient = httpClientFactory.CreateClient("API");
        }

        public async Task<List<Supplement>?> GetAllSupplementsAsync()
        {
            return await m_httpClient.GetFromJsonAsync<List<Supplement>>("api/Supplement");
        }

        public async Task<Supplement?> GetSupplementByIdAsync(int id)
        {
            return await m_httpClient.GetFromJsonAsync<Supplement>($"api/Supplement/{id}");
        }

        public async Task<Supplement?> AddSupplementAsync(Supplement newItem)
        {
            var response = await m_httpClient.PostAsJsonAsync("api/Supplement", newItem);

            if (!response.IsSuccessStatusCode)
                return null;

            return await response.Content.ReadFromJsonAsync<Supplement>();
        }

        public async Task<Supplement?> UpdateSupplementAsync(Supplement updatedItem)
        {
            var response = await m_httpClient.PutAsJsonAsync("api/Supplement", updatedItem);

            if (!response.IsSuccessStatusCode)
                return null;

            return await response.Content.ReadFromJsonAsync<Supplement>();
        }

        public async Task<bool> DeleteSupplementAsync(int id)
        {
            var result = await m_httpClient.DeleteAsync($"api/Supplement/{id}");
            return result.IsSuccessStatusCode;
        }
    }
}