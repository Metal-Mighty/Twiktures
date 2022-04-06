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
                if (_backendUri.EndsWith('/'))
                    _backendUri.Trim('/');
            }
            else
                throw new InvalidOperationException($"The environment variable {uriEnvName} hasn't been ");

            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
            _httpClient.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");
            //_httpClient.BaseAddress = new Uri(_backendUri);
        }

        public async Task<User> GetUserAsync(string username)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<User>($"{_backendUri}/api/twitter/user/{username}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return null;
        }

        public async Task<List<Tweet>> GetUserTweetsAsync(string username, long? oldestId, long? newestId)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<List<Tweet>>($"{_backendUri}/api/twitter/user/{username}/tweets?oldestId={oldestId}&newestId={newestId}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return null;
        }
    }
}
