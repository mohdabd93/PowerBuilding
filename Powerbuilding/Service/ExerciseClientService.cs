using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Powerbuilding.Service
{
    public class ExerciseClientService
    {
        private readonly HttpClient m_httpClient;

        public ExerciseClientService(IHttpClientFactory httpClientFactory)
        {
            m_httpClient = httpClientFactory.CreateClient("API");
        }

        public async Task<List<ExerciseLog>?> GetAllExerciseLogsAsync()
        {
            return await m_httpClient.GetFromJsonAsync<List<ExerciseLog>>("api/ExerciseLog");
        }

        public async Task<ExerciseLog?> GetExerciseLogAsync(int id)
        {
            return await m_httpClient.GetFromJsonAsync<ExerciseLog>($"api/ExerciseLog/log/{id}");
        }

        public async Task<List<ExerciseLog>?> GetLastLogsByExerciseAsync(int exerciseId)
        {
            var response = await m_httpClient.GetAsync($"api/ExerciseLog/exercise/{exerciseId}");

            if (!response.IsSuccessStatusCode)
                return new List<ExerciseLog>();  

            return await response.Content.ReadFromJsonAsync<List<ExerciseLog>>();
        }

        public async Task<ExerciseLog?> AddExerciseLogAsync(ExerciseLog exerciseLog)
        {
            var result = await m_httpClient.PostAsJsonAsync("api/ExerciseLog", exerciseLog);
            return await result.Content.ReadFromJsonAsync<ExerciseLog>();
        }

        public async Task<ExerciseLog?> UpdateExerciseLogAsync(ExerciseLog exerciseLog)
        {
            var result = await m_httpClient.PutAsJsonAsync("api/ExerciseLog", exerciseLog);
            return await result.Content.ReadFromJsonAsync<ExerciseLog>();
        }

        public async Task<bool> RemoveExerciseLogAsync(int id)
        {
            var response = await m_httpClient.DeleteAsync($"api/ExerciseLog/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
