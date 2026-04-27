using Domain.Entities;

namespace Powerbuilding.Service
{
    public class MealClientService
    {
        private HttpClient m_httpClient;
        public MealClientService(IHttpClientFactory httpClientFactory)
        {
            m_httpClient= httpClientFactory.CreateClient("API");
        }
        public async Task<List<Meal>?> GetAllMealsAsync() 
        {
            return await m_httpClient.GetFromJsonAsync<List<Meal>>("API/Meal");
        }
        public async Task<Meal?> GetMealByIdAsync(int id) 
        {
            return await m_httpClient.GetFromJsonAsync<Meal>($"API/Meal/{id}");
        }
        public async Task<Meal?> AddMealAsync(Meal newMeal)
        {
            var result= await m_httpClient.PostAsJsonAsync<Meal>($"API/Meal/", newMeal);
            return await result.Content.ReadFromJsonAsync<Meal>();
        }
        public async Task<Meal?> UpdateMealAsync(Meal updatedMeal)
        {
            var result = await m_httpClient.PutAsJsonAsync<Meal>($"API/Meal/", updatedMeal);
            return await result.Content.ReadFromJsonAsync<Meal>();
        }
        public async Task<bool> DeleteMealAsync(int id)
        {
            var result = await m_httpClient.DeleteAsync($"API/Meal/{id}");
            return result.IsSuccessStatusCode;
        }
    }
}
