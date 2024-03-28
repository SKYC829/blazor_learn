using Microsoft.JSInterop;

namespace BlazorLearn_WebApp.Client.Components.L08
{
    public class SampleModel
    {
        public string Name { get; set; }

        public int Age { get; set; }

        [JSInvokable()]
        public string SayHi(DotNetObjectReference<SampleModel> obj)
        {
            return $"Hi, my name is {obj.Value.Name} and i am {obj.Value.Age} years old";
        }
    }
}
