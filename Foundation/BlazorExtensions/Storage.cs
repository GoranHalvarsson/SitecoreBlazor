namespace Foundation.BlazorExtensions
{
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.JSInterop;
    using System;
    using System.Threading.Tasks;

    public abstract class StorageBase
    {
        private readonly string _fullTypeName;

        protected internal StorageBase()
        {
            _fullTypeName = GetType().FullName.Replace('.', '_');
        }

        public Task ClearAsync() => JSRuntime.Current.InvokeAsync<object>($"{_fullTypeName}.Clear");


        [Obsolete()]
        public void Clear()
        {
            ((IJSInProcessRuntime)JSRuntime.Current).Invoke<object>($"{_fullTypeName}.Clear");
        }

        public Task<string> GetItemAsync(string key)
        {
            return JSRuntime.Current.InvokeAsync<string>($"{_fullTypeName}.GetItem", key);
        }

        [Obsolete()]
        public string GetItem(string key)
        {
            return ((IJSInProcessRuntime)JSRuntime.Current).Invoke<string>($"{_fullTypeName}.GetItem", key);
        }

        public async Task<T> GetItemAsync<T>(string key)
        {
            var json = await GetItemAsync(key);
            return string.IsNullOrWhiteSpace(json) ? default(T) : Json.Deserialize<T>(json);
        }

        [Obsolete()]
        public T GetItem<T>(string key)
        {
            var json = GetItem(key);
            return string.IsNullOrEmpty(json) ? default(T) : Json.Deserialize<T>(json);
        }

        public Task<string> KeyAsync(int index)
        {
            return JSRuntime.Current.InvokeAsync<string>($"{_fullTypeName}.Key", index);
        }

        [Obsolete()]
        public string Key(int index)
        {
            return ((IJSInProcessRuntime)JSRuntime.Current).Invoke<string>($"{_fullTypeName}.Key", index);
        }

        [Obsolete()]
        public int Length => ((IJSInProcessRuntime)JSRuntime.Current).Invoke<int>($"{_fullTypeName}.Length");


        public Task RemoveItemAsync(string key)
        {
            return JSRuntime.Current.InvokeAsync<object>($"{_fullTypeName}.RemoveItem", key);
        }

        [Obsolete()]
        public void RemoveItem(string key)
        {
            ((IJSInProcessRuntime)JSRuntime.Current).Invoke<object>($"{_fullTypeName}.RemoveItem", key);
        }

        public Task SetItemAsync(string key, string data)
        {
            return JSRuntime.Current.InvokeAsync<object>($"{_fullTypeName}.SetItem", key, data);
        }
        [Obsolete()]
        public void SetItem(string key, string data)
        {
            ((IJSInProcessRuntime)JSRuntime.Current).Invoke<object>($"{_fullTypeName}.SetItem", key, data);
        }

        public Task SetItemAsync(string key, object data)
        {
            return SetItemAsync(key, Json.Serialize(data));
        }

        [Obsolete()]
        public void SetItem(string key, object data)
        {
            SetItem(key, Json.Serialize(data));
        }

        [Obsolete()]
        public string this[string key]
        {
            get => ((IJSInProcessRuntime)JSRuntime.Current).Invoke<string>($"{_fullTypeName}.GetItemString", key);
            set => ((IJSInProcessRuntime)JSRuntime.Current).Invoke<object>($"{_fullTypeName}.SetItemString", key, value);
        }

        [Obsolete()]
        public string this[int index]
        {
            get => ((IJSInProcessRuntime)JSRuntime.Current).Invoke<string>($"{_fullTypeName}.GetItemNumber", index);
            set => ((IJSInProcessRuntime)JSRuntime.Current).Invoke<object>($"{_fullTypeName}.SetItemNumber", index, value);
        }
    }

    public sealed class LocalStorage : StorageBase
    {

    }

    public sealed class SessionStorage : StorageBase
    {

    }

   
}
