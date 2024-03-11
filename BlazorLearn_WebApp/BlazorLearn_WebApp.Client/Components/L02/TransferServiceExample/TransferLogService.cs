using System.Collections.ObjectModel;

namespace BlazorLearn_WebApp.Client.Components.L02.TransferServiceExample
{
    public sealed class TransferLogService
    {
        private static Guid _serviceId = Guid.NewGuid();
        private static ObservableCollection<string> _logs = new ObservableCollection<string>();
        private static object _locker = new object();
        private static TransferLogService _service;

        //public static TransferLogService Instance
        //{
        //    get
        //    {
        //        if ( _service is null )
        //        {
        //            lock ( _locker )
        //            {
        //                if ( _service is null )
        //                {
        //                    _service = new TransferLogService ();
        //                }
        //            }
        //        }
        //        return _service;
        //    }
        //}
        public Guid ServiceId => _serviceId;
        public ObservableCollection<string> Logs => _logs;
    }
}
