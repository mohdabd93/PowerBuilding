using Domain.Entities;
using System.Net.Http.Json;

namespace Powerbuilding.Service
{
    public class WorkoutClientService
    {
        private readonly HttpClient m_httpClient;

        public WorkoutClientService(IHttpClientFactory httpClientFactory)
        {
            m_httpClient = httpClientFactory.CreateClient("API");
        }

        public async Task<List<WorkoutDay>?> GetAllDaysAsync()
        {
            return await m_httpClient.GetFromJsonAsync<List<WorkoutDay>>("api/WorkoutDay");
        }

        public async Task<WorkoutDay?> GetDayByIdAsync(int id)
        {
            return await m_httpClient.GetFromJsonAsync<WorkoutDay>($"api/WorkoutDay/{id}");
        }

        public async Task<WorkoutDay?> AddNewDayAsync(WorkoutDay newDay)
        {
            var response = await m_httpClient.PostAsJsonAsync("api/WorkoutDay", newDay);

            if (!response.IsSuccessStatusCode)
                return null;

            return await response.Content.ReadFromJsonAsync<WorkoutDay>();
        }

        public async Task<WorkoutDay?> UpdateDayAsync(WorkoutDay updatedDay)
        {
            var response = await m_httpClient.PutAsJsonAsync("api/WorkoutDay", updatedDay);

            if (!response.IsSuccessStatusCode)
                return null;

            return await response.Content.ReadFromJsonAsync<WorkoutDay>();
        }

        public async Task<bool> DeleteDayAsync(int id)
        {
            var result = await m_httpClient.DeleteAsync($"api/WorkoutDay/{id}");
            return result.IsSuccessStatusCode;
        }
    }
}