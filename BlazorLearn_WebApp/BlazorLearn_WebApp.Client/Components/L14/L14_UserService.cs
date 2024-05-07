namespace BlazorLearn_WebApp.Client.Components.L14
{
    public class L14_UserService
    {
        private readonly HttpClient _httpClient;

        public L14_UserService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("local");
        }

        public async Task<string?> LoginAsync(string userName,string password)
        {
            HttpResponseMessage response = await _httpClient.PostAsync("/api/lobby/login",new FormUrlEncodedContent(new Dictionary<string,string>()
            {
                {"userName",userName },
                {"password",password }
            }));
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }

        public async Task SiginInAsync(string userName,string email,string password)
        {
            HttpResponseMessage response = await _httpClient.PostAsync("/api/lobby/sigin-in",new FormUrlEncodedContent(new Dictionary<string,string>()
            {
                {"userName",userName },
                {"email",email},
                {"password",password }
            }));
            response.EnsureSuccessStatusCode();
        }
    }
}
