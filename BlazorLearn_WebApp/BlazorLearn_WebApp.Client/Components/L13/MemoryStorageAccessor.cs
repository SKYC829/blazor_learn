using System.Runtime.InteropServices.JavaScript;
using System.Runtime.Versioning;

namespace BlazorLearn_WebApp.Client.Components.L13
{
    public sealed class MemoryStorageAccessor : IDisposable
    {
        private Dictionary<string,object?>? _storage;

        public MemoryStorageAccessor()
        {
            _storage = new Dictionary<string,object?>();
        }

        public void Dispose()
        {
            _storage?.Clear();
            _storage = null;
        }

        public void Set(string key, object? value)
        {
            if(_storage is not null )
            {
                _storage[key] = value;
            }
        }

        public object? Get(string key)
        {
            if(_storage is not null && _storage.TryGetValue(key,out object? value) )
            {
                return value;
            }
            return null;
        }

        public void Delete(string key)
        {
            if(_storage is not null && _storage.ContainsKey(key) )
            {
                _storage.Remove(key);
            }
        }

        public void Clear()
        {
            if(_storage is not null )
            {
                _storage.Clear();
            }
        }
    }

    [SupportedOSPlatform("BROWSER")]
    public partial class MemoryStorageAccessorForJs
    {
        private static MemoryStorageAccessor _accessor = new MemoryStorageAccessor();
        [JSExport]
        public static void Set(string key,[JSMarshalAs<JSType.Any>]object? value)
        {
            _accessor.Set(key,value);
        }

        [JSExport]
        [return:JSMarshalAs<JSType.Any>]
        public static object? Get(string key)
        {
            return _accessor.Get(key);
        }

        [JSExport]
        public static void Delete(string key)
        {
            _accessor.Delete(key);
        }

        [JSExport]
        public static void Clear()
        {
            _accessor.Clear();
        }
    }
}
