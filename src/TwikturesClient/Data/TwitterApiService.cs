using TwikturesClient.Models;
using System.Net.Http.Headers;

namespace TwikturesClient.Data
{
    public class TwitterApiService
    {
        const string uriEnvName = "TWIKTURES_BACKEND_URI";
        private static string _backendUri;
        private static readonly HttpClient _httpClient = new HttpClient();

        public TwitterApiService()
        {
            var uri = Environment.GetEnvironmentVariable(uriEnvName);
            if (uri != null)
            {
                _backendUri = uri;
                if (!_backendUri.EndsWith('/'))
                    _backendUri = _backendUri + '/';
            }
            else
                throw new InvalidOperationException($"The environment variable {uriEnvName} hasn't been ");

            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
            _httpClient.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");
            _httpClient.BaseAddress = new Uri($"http://{_backendUri}");
        }

        public async Task<User> GetUserAsync(string username)
        {
            return await _httpClient.GetFromJsonAsync<User>($"twitter/user/{username}");
        }
    }
}
