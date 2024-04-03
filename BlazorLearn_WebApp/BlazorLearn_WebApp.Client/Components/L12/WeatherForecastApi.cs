namespace BlazorLearn_WebApp.Client.Components.L12
{
    public class WeatherForecastApi(IHttpClientFactory httpClientFactory,ILogger<WeatherForecastApi> logger)
    {
        public async Task<string> GetWeatherForecastAsync()
        {
            logger.LogInformation($"[包装器]:模拟干预请求之前的操作");
            HttpClient localClient = httpClientFactory.CreateClient("local");
            HttpResponseMessage response = await localClient.GetAsync("/WeatherForecast");
            response.EnsureSuccessStatusCode();
            logger.LogInformation($"[包装器]:模拟干预请求之后的操作");
            return await response.Content.ReadAsStringAsync();
        }
    }
}
