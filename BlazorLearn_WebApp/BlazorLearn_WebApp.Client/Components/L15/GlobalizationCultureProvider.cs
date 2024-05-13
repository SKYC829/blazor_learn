using Microsoft.AspNetCore.Components;

using System.Globalization;
using System.Reflection;

namespace BlazorLearn_WebApp.Client.Components.L15
{
    internal class GlobalizationCultureProvider
    {
        private CultureInfo? _fallbackCulture;
        private CultureInfo? _culture;
        private List<CultureInfo>? _supportCultures;
        private List<ComponentBase>? _globalizationComponents;
        private readonly static MethodInfo? _stateHasChanged = typeof(ComponentBase).GetMethod("StateHasChanged",BindingFlags.Instance | BindingFlags.NonPublic);

        public CultureInfo FallbackCulture
        {
            get => _supportCultures?.FirstOrDefault() ?? CultureInfo.GetCultureInfo("zh-CN");
            set => _fallbackCulture = value;
        }

        public CultureInfo CurrentCulture
        {
            get => _culture ?? FallbackCulture;
            set { _culture = value; NotifyCultureChanged(); }
        }

        public IReadOnlyList<CultureInfo> SupportCultures => _supportCultures??new List<CultureInfo>();

        public GlobalizationCultureProvider()
        {
            _globalizationComponents = new List<ComponentBase>();
        }

        public GlobalizationCultureProvider(IEnumerable<CultureInfo> supportCultures):this()
        {
            _supportCultures = new List<CultureInfo>(supportCultures);
        }

        public void SubscribeCultureChange(ComponentBase component)
        {
            _globalizationComponents?.Add(component);
        }

        public void UnsubscribeCultureChange(ComponentBase component)
        {
            _globalizationComponents?.Remove(component);
        }

        public void NotifyCultureChanged()
        {
            if(_globalizationComponents is null )
            {
                return;
            }
            foreach ( var item in _globalizationComponents )
            {
                if(item is not null )
                {
                    _stateHasChanged?.Invoke(item, []);
                }
            }
        }
    }
}
