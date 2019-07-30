namespace Foundation.BlazorExtensions
{
    using Microsoft.AspNetCore.Components;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.JSInterop;
    using SitecoreBlazorHosted.Shared.Models;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Text;
    using System.Text.Json;
    using System.Text.Json.Serialization;
    using System.Threading.Tasks;

    public abstract class StorageBase
    {
        private readonly string _fullTypeName;
        private readonly JsonSerializerOptions _jsonSerializerOptions;

        protected internal StorageBase()
        {
            _fullTypeName = GetType().FullName.Replace('.', '_');
            _jsonSerializerOptions = new JsonSerializerOptions()
            {
                IgnoreNullValues = true,
                AllowTrailingCommas = true,
                PropertyNameCaseInsensitive = true
            };
           //_jsonSerializerOptions.Converters.Add(new CustomTupleConverter());
        }

        public Task ClearAsync(IJSRuntime jsRuntime) => jsRuntime.InvokeAsync<object>($"{_fullTypeName}.Clear");



        public Task<string> GetItemAsync(string key, IJSRuntime jsRuntime)
        {
            return jsRuntime.InvokeAsync<string>($"{_fullTypeName}.GetItem", key);
        }

        public async Task<T> GetItemAsync<T>(string key, IJSRuntime jsRuntime)
        {
            var json = await GetItemAsync(key, jsRuntime);
            return string.IsNullOrWhiteSpace(json) ? default(T) : JsonSerializer.Deserialize<T>(json, _jsonSerializerOptions);
        }


        public Task<string> KeyAsync(int index, IJSRuntime jsRuntime)
        {
            return jsRuntime.InvokeAsync<string>($"{_fullTypeName}.Key", index);
        }

        public Task RemoveItemAsync(string key, IJSRuntime jsRuntime)
        {
            return jsRuntime.InvokeAsync<object>($"{_fullTypeName}.RemoveItem", key);
        }

        [Obsolete()]
        public void RemoveItem(string key, IJSRuntime jsRuntime)
        {
            ((IJSInProcessRuntime)jsRuntime).Invoke<object>($"{_fullTypeName}.RemoveItem", key);
        }

        public Task SetItemAsync(string key, string data, IJSRuntime jsRuntime)
        {
            return jsRuntime.InvokeAsync<object>($"{_fullTypeName}.SetItem", key, data);
        }


        public Task SetItemAsync<T>(string key, T data, IJSRuntime jsRuntime)
        {
            return SetItemAsync(key, JsonSerializer.Serialize<T>(data, _jsonSerializerOptions), jsRuntime);
        }


    }

    public class LocalStorage : StorageBase
    {

    }

    public class SessionStorage : StorageBase
    {

    }

    /// <summary>
    /// CustomTupleConverter is not working, UNDER CONSTRUCTION...
    /// </summary>
    public class CustomTupleConverter : JsonConverter<Tuple<DateTime, string, Route>>
    {
        public CustomTupleConverter() { }

        public override Tuple<DateTime, string, Route> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {


            Route route = new Route();
            string propertyName = string.Empty;

            if (reader.TokenType != JsonTokenType.StartObject)
            {
                throw new JsonException();
            }

            reader.Read();

            if (reader.TokenType != JsonTokenType.PropertyName)
            {
                throw new JsonException();
            }

            var item1 = reader.GetString();
            reader.Read();

            var item1Value = reader.GetDateTime();
            reader.Read();


            var item2 = reader.GetString();
            reader.Read();

            var item2Value = reader.GetString();
            reader.Read();

            var item3 = reader.GetString();


            while (reader.Read())
            {


                if (reader.TokenType == JsonTokenType.PropertyName)
                {
                    propertyName = reader.GetString();
                    //$@"""{System.Text.Encoding.UTF8.GetString(reader.ValueSpan.ToArray())}""");

                }

                if (reader.TokenType != JsonTokenType.PropertyName)
                {

                    switch (propertyName)
                    {
                        case "Name":
                            route.Name = System.Text.Encoding.UTF8.GetString(reader.ValueSpan.ToArray());
                            break;
                        case "Url":
                            route.Url = System.Text.Encoding.UTF8.GetString(reader.ValueSpan.ToArray());
                            break;
                        case "Id":
                            route.Id = System.Text.Encoding.UTF8.GetString(reader.ValueSpan.ToArray());
                            break;
                        case "DisplayName":
                            route.DisplayName = System.Text.Encoding.UTF8.GetString(reader.ValueSpan.ToArray());
                            break;
                        case "ItemLanguage":
                            route.ItemLanguage = System.Text.Encoding.UTF8.GetString(reader.ValueSpan.ToArray());
                            break;
                        case "Fields":
                            route.Fields = null;
                            break;
                        case "Placeholders":
                            route.Placeholders = null;
                            break;
                    }


                }


                if (reader.TokenType == JsonTokenType.EndObject)
                {
                    Tuple<DateTime, string, Route> tuple = new Tuple<DateTime, string, Route>(item1Value, item2Value, route);
                    return tuple;
                }

            }

            throw new JsonException();


        }


        public override void Write(Utf8JsonWriter writer, Tuple<DateTime, string, Route> value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();


            writer.WriteString("Item1", JsonSerializer.Serialize<DateTime>(value.Item1));
            writer.WriteString("Item2", value.Item2);
            writer.WriteString("Item3", JsonSerializer.Serialize<Route>(value.Item3));


            writer.WriteEndObject();
        }
    }

}
