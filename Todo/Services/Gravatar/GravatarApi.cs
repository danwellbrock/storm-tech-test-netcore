using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Todo.Services.Gravatar
{
    public static class GravatarApi
    {
        public static async Task<GravatarProfile> GetProfile(string email)
        {
            try
            {
                var httpClient = new HttpClient();

                httpClient.DefaultRequestHeaders.Add("User-Agent", "Todo/1.0");

                var uri = $"https://www.gravatar.com/{StringConvenience.GetHash(email)}.json";

                var response = await httpClient.GetFromJsonAsync<Response>(uri);
                var entry = response.Entry.FirstOrDefault();

                if (entry == null) return null;

                var profile = new GravatarProfile()
                {
                    DisplayName = entry?.Name.Formatted ?? (entry.DisplayName ?? ""),
                    Photo = entry.Photos != null && entry.Photos.Any() && !string.IsNullOrWhiteSpace(entry.Photos.First().Value) ? entry.Photos.First().Value : ""
                };

                return profile;
            }
            catch (HttpRequestException)
            {
                return null;
            }
        }
    }
}