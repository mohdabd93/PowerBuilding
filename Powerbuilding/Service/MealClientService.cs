using Domain.Entities;

namespace Powerbuilding.Service
{
    public class MealClientService
    {
        private readonly HttpClient m_httpClient;

        public MealClientService(IHttpClientFactory httpClientFactory)
        {
            m_httpClient = httpClientFactory.CreateClient("API");
        }

        public async Task<List<Meal>?> GetAllMealsAsync()
        {
            return await m_httpClient.GetFromJsonAsync<List<Meal>>("api/Meal");
        }

        public async Task<Meal?> GetMealByIdAsync(int id)
        {
            return await m_httpClient.GetFromJsonAsync<Meal>($"api/Meal/{id}");
        }

        public async Task<Meal?> AddMealAsync(Meal newMeal)
        {
            var response = await m_httpClient.PostAsJsonAsync("api/Meal", newMeal);
 
            if (!response.IsSuccessStatusCode)
                return null;

            return await response.Content.ReadFromJsonAsync<Meal>();
        }

        public async Task<Meal?> UpdateMealAsync(Meal updatedMeal)
        {
            var response = await m_httpClient.PutAsJsonAsync("api/Meal", updatedMeal);

            if (!response.IsSuccessStatusCode)
                return null;

            return await response.Content.ReadFromJsonAsync<Meal>();
        }

        public async Task<bool> DeleteMealAsync(int id)
        {
            var result = await m_httpClient.DeleteAsync($"api/Meal/{id}");
            return result.IsSuccessStatusCode;
        }
    }
}
