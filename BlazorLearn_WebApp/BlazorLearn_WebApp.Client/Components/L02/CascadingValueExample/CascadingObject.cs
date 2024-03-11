namespace BlazorLearn_WebApp.Client.Components.L02.CascadingValueExample
{
    internal class CascadingObject
    {
        public string Value1 => "Hello1";

        public string Value2 => "World1";
    }

    internal class UserInfo
    {
        public string Name { get; set; }

        public string Email { get; set; }
    }
}
