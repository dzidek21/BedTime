using BedTime.Models;
using BedTime.Services.Interfaces;
using System.Net.Http.Json;

namespace BedTime.Services
{
    public class FairyTailsApi : IApiRepo
    {
        //Put API URL here
        private const string URL = "";
        private readonly HttpClient _httpClient;
        public FairyTailsApi(HttpClient httpClient)
        {
            _httpClient = httpClient;

        }

        public async Task AddLike(int id)
        {
            var resposne = await _httpClient.GetAsync(URL);
            var result = await resposne.Content.ReadFromJsonAsync<List<TailModel>>();
            if (result == null)
            {
                return;
            }
            var model = result.FirstOrDefault(x => x.Id == id);
            if (model == null)
            {
                return;
            }

            model.LikeCount++;
            await _httpClient.PutAsJsonAsync<TailModel>(URL, model);


        }

        public async Task<List<TailModel>> GetAllTails()
        {
            var resposne = await _httpClient.GetAsync(URL);
            var result = await resposne.Content.ReadFromJsonAsync<List<TailModel>>();
            if (result == null)
            {
                return new List<TailModel>();
            }
            return result;
        }

        public async Task<int> GetLikesCount(int id)
        {
            var resposne = await _httpClient.GetAsync(URL);

            var result = await resposne.Content.ReadFromJsonAsync<List<TailModel>>();
            if (result == null)
            {
                return 0;
            }
            var model = result.FirstOrDefault(x => x.Id == id);
            if (model == null)
            {
                return 0;
            }
            return model.LikeCount;
        }

        public async Task RemoveLike(int id)
        {
            var resposne = await _httpClient.GetAsync(URL);
            var result = await resposne.Content.ReadFromJsonAsync<List<TailModel>>();
            if (result == null)
            {
                return;
            }
            var model = result.FirstOrDefault(x => x.Id == id);
            if (model == null)
            {
                return;
            }

            model.LikeCount--;
            await _httpClient.PutAsJsonAsync<TailModel>(URL, model);
        }
    }
}
