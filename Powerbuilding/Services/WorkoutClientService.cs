using Domain.Entities;

namespace Powerbuilding.Services
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
            return await m_httpClient
                .GetFromJsonAsync<List<WorkoutDay>>("api/WorkoutDay");
        }
    }
}

