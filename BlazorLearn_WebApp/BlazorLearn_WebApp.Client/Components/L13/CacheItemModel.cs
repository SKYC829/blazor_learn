namespace BlazorLearn_WebApp.Client.Components.L13
{
    public sealed class CacheItemModel
    {
        public string? Url { get; set; }

        public string? Method { get; set; } = "get";

        public string? Body { get; set; }

        public string? Key { get; set; }

        public string? Value { get; set; }

        public TimeSpan TimeoutAt { get; set; } = TimeSpan.FromSeconds(10);
    }
}
