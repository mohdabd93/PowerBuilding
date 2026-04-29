using Domain.Entities;
using static System.Net.WebRequestMethods;

namespace Powerbuilding.Service
{
    public class WeekPlanClientService
    {
        private readonly HttpClient m_httpClient;

        public WeekPlanClientService(IHttpClientFactory factory)
        {
            m_httpClient = factory.CreateClient("API");
        }

        public async Task<WeekPlan?> GetFullWeekPlanAsync()
        {
            var response = await m_httpClient.GetAsync("api/WeekPlan/full");

            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                return null;

            if (!response.IsSuccessStatusCode)
                return null;

            return await response.Content.ReadFromJsonAsync<WeekPlan>();
        }

      
    }
}
