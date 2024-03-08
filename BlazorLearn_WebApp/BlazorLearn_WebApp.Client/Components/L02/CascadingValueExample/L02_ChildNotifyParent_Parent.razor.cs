using System.ComponentModel;
using System.Runtime.CompilerServices;

using Microsoft.AspNetCore.Components;

namespace BlazorLearn_WebApp.Client.Components.L02.CascadingValueExample
{
    public partial class L02_ChildNotifyParent_Parent : ComponentBase, INotifyPropertyChanged
    {
        private string? _name;
        private string? _email;
        public event PropertyChangedEventHandler? PropertyChanged;

        [Parameter]
        public EventCallback<(string, string)> PropertyChangedEvent { get; set; }

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged ();
            }
        }

        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                OnPropertyChanged ();
            }
        }

        private void OnPropertyChanged ( [CallerMemberName] string propertyName = null )
        {
            PropertyChanged?.Invoke ( this , new PropertyChangedEventArgs ( propertyName ) );
            PropertyChangedEvent.InvokeAsync ( (propertyName, GetPropertyValue ( propertyName )) );
        }

        private string GetPropertyValue ( string propertyName )
        {
            return this.GetType ().GetProperty ( propertyName ).GetValue ( this )?.ToString ();
        }
    }
}
