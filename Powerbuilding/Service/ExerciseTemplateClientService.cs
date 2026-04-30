using System.Net.Http.Json;
using Domain.Entities;
using Domain.Enum;

namespace Powerbuilding.Service
{
    public class ExerciseTemplateClientService
    {
        private readonly HttpClient m_http;

        public ExerciseTemplateClientService(IHttpClientFactory factory)
        {
            m_http = factory.CreateClient("API");
        }

        //--------------------------------------------------
        // GET ALL
        //--------------------------------------------------
        public async Task<List<ExerciseTemplate>> GetAll()
        {
            return await m_http.GetFromJsonAsync<List<ExerciseTemplate>>(
                "api/ExerciseTemplate") ?? new();
        }

        //--------------------------------------------------
        // GET BY MUSCLE
        //--------------------------------------------------
        public async Task<List<ExerciseTemplate>> GetByMuscle(TargetMuscle muscle)
        {
            return await m_http.GetFromJsonAsync<List<ExerciseTemplate>>(
                $"api/ExerciseTemplate/muscle/{muscle}") ?? new();
        }

        //--------------------------------------------------
        // ADD
        //--------------------------------------------------
        public async Task<ExerciseTemplate?> Add(ExerciseTemplate model)
        {
            // FIX: strip Id before sending so API lets SQL Server generate it
            model.Id = 0;

            var res = await m_http.PostAsJsonAsync("api/ExerciseTemplate", model);
            if (!res.IsSuccessStatusCode)
                throw new Exception("Failed to add template");

            return await res.Content.ReadFromJsonAsync<ExerciseTemplate>();
        }

        //--------------------------------------------------
        // UPDATE
        //--------------------------------------------------
        public async Task Update(ExerciseTemplate model)
        {
            var res = await m_http.PutAsJsonAsync("api/ExerciseTemplate", model);
            if (!res.IsSuccessStatusCode)
                throw new Exception("Failed to update template");
        }

        //--------------------------------------------------
        // DELETE
        //--------------------------------------------------
        public async Task Delete(int id)
        {
            var res = await m_http.DeleteAsync($"api/ExerciseTemplate/{id}");
            if (!res.IsSuccessStatusCode)
                throw new Exception("Failed to delete template");
        }

        //--------------------------------------------------
        // GET FILTERED
        //--------------------------------------------------
        public async Task<List<ExerciseTemplate>> GetFiltered(TargetMuscle muscle, ExerciseType type)
        {
            return await m_http.GetFromJsonAsync<List<ExerciseTemplate>>(
                $"api/ExerciseTemplate/filtered/{muscle}/{type}") ?? new();
        }
    }
}