using System.ComponentModel.DataAnnotations;

namespace BlazorLearn_WebApp.Client.Components.L11
{
    public sealed class ExampleFormModel
    {
        [Required]
        public string? Message { get; set; }

        [Range(0,5,ErrorMessage = "当验证失败时将会显示这条消息")]
        public int IntValue { get; set; } = 10;
    }
}
