namespace BlazorLearn_WebApp.Client.Components.L12
{
    public static class WeatherForecastClientExtensions
    {
        public static async Task<string> GetWeatherForecastAsync(this HttpClient client,ILogger logger)
        {
            logger.LogInformation($"[扩展方法]:模拟干预请求之前的操作");
            HttpResponseMessage response = await client.GetAsync("/WeatherForecast");
            response.EnsureSuccessStatusCode();
            logger.LogInformation($"[扩展方法]:模拟干预请求之后的操作");
            return await response.Content.ReadAsStringAsync();
        }
    }
}
