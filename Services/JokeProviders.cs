using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace examples_dotnet_core.Services
{
    public interface IJokeProvider
    {
        Task<string> SayJoke();
    }

    public class ChuckNorrisResponseDTO
    {
        [JsonProperty("type")]
        public string Type { get; set;}
        [JsonProperty("value")]
        public ChuckNorrisResponseDataDTO Value { get; set; }
    }

    public class ChuckNorrisResponseDataDTO
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("joke")]
        public string Joke { get; set;}
    }

    public class ChuckNorrisJokeProvider : IJokeProvider, IDisposable
    {
        private readonly HttpClient _httpClient;

        public ChuckNorrisJokeProvider()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("http://api.icndb.com/");
        }

        public async Task<string> SayJoke()
        {
            HttpResponseMessage responseMessage = await _httpClient.GetAsync("jokes/random");
            responseMessage.EnsureSuccessStatusCode();

            string payload = await responseMessage.Content.ReadAsStringAsync();

            ChuckNorrisResponseDTO responseDTO = JsonConvert.DeserializeObject<ChuckNorrisResponseDTO>(payload);

            return responseDTO.Value.Joke;
        }

        public void Dispose()
        {
            _httpClient.Dispose();
        }
    }

    public class BufferedJokeProvider : IJokeProvider
    {
        private readonly IJokeProvider _jokeProvider;
        private readonly int _size;
        private int _ptr;

        private string[] _jokes;

        public BufferedJokeProvider(IJokeProvider jokeProvider, int size)
        {
            if (jokeProvider == null) {
                throw new ArgumentNullException(nameof(jokeProvider));
            }

            if (size < 1) {
                throw new ArgumentException("Size must be greater than zero.", nameof(size));
            }

            _jokeProvider = jokeProvider;
            _size = size;
            _ptr = 0;
            _jokes = new string[_size];

            for(int i = 0; i < _size; i++) {
                _jokes[i] = null;
            }
        }

        public async Task<string> SayJoke()
        {
            if (_jokes[_ptr] == null) {
                _jokes[_ptr] = await _jokeProvider.SayJoke();
            }
            
            string joke = _jokes[_ptr];
            _ptr = (_ptr + 1) % _size;

            return joke;
        }
    }
}
