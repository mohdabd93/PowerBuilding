using Domain.Entities;
using System.Net.Http.Json;

namespace Powerbuilding.Service
{
    public class WorkoutClientService
    {
        private HttpClient m_httpClient;
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
            var result = await m_httpClient.PostAsJsonAsync<WorkoutDay>("api/WorkoutDay/", newDay);
            return await result.Content.ReadFromJsonAsync<WorkoutDay>();
        }
        public async Task<WorkoutDay?> UpdateDayAsync(WorkoutDay updatedDay)
        {
            var result = await m_httpClient.PutAsJsonAsync<WorkoutDay>("api/WorkoutDay/", updatedDay);
            return await result.Content.ReadFromJsonAsync<WorkoutDay>();
        }
        public async Task<bool> DeleteDayAsync(int id)
        {
            var result = await m_httpClient.DeleteAsync($"api/WorkoutDay/{id}");
            return result.IsSuccessStatusCode;
        }
    }
}