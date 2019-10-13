namespace Foundation.BlazorExtensions
{
    using Microsoft.JSInterop;
    using SitecoreBlazorHosted.Shared.Models;
    using System;
    using System.Text.Json;
    using System.Text.Json.Serialization;
    using System.Threading.Tasks;

    public abstract class StorageBase
    {
        private readonly string? _fullTypeName;
        private readonly JsonSerializerOptions _jsonSerializerOptions;

        protected internal StorageBase()
        {
            _fullTypeName = GetType().FullName?.Replace('.', '_');
            _jsonSerializerOptions = new JsonSerializerOptions()
            {
                IgnoreNullValues = true,
                AllowTrailingCommas = true,
                PropertyNameCaseInsensitive = true
            };
            //_jsonSerializerOptions.Converters.Add(new CustomTupleConverter());
        }

        public ValueTask<object> ClearAsync(IJSRuntime jsRuntime) => jsRuntime.InvokeAsync<object>($"{_fullTypeName}.Clear");



        public ValueTask<string> GetItemAsync(string key, IJSRuntime? jsRuntime)
        {
            return jsRuntime.InvokeAsync<string>($"{_fullTypeName}.GetItem", key);
        }

        public async Task<T> GetItemAsync<T>(string key, IJSRuntime? jsRuntime)
        {
            var json = await GetItemAsync(key, jsRuntime);
            return string.IsNullOrWhiteSpace(json) ? default : JsonSerializer.Deserialize<T>(json, _jsonSerializerOptions);
        }


        public ValueTask<string> KeyAsync(int index, IJSRuntime? jsRuntime)
        {
            return jsRuntime.InvokeAsync<string>($"{_fullTypeName}.Key", index);
        }

        public ValueTask<object> RemoveItemAsync(string key, IJSRuntime? jsRuntime)
        {
            return jsRuntime.InvokeAsync<object>($"{_fullTypeName}.RemoveItem", key);
        }

        [Obsolete()]
        public void RemoveItem(string key, IJSRuntime? jsRuntime)
        {

            jsRuntime.InvokeAsync<object>($"{_fullTypeName}.RemoveItem", key, key);
            
        }

        public ValueTask<object> SetItemAsync(string key, string? data, IJSRuntime? jsRuntime)
        {
            return jsRuntime.InvokeAsync<object>($"{_fullTypeName}.SetItem", key, data);
        }


        public ValueTask<object> SetItemAsync<T>(string key, T data, IJSRuntime? jsRuntime)
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
    public class CustomTupleConverter : JsonConverter<Tuple<DateTime, string, BlazorRoute>>
    {
        public CustomTupleConverter() { }

        public override Tuple<DateTime, string, BlazorRoute> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {


            BlazorRoute route = new BlazorRoute();
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
                    Tuple<DateTime, string, BlazorRoute> tuple = new Tuple<DateTime, string, BlazorRoute>(item1Value, item2Value, route);
                    return tuple;
                }

            }

            throw new JsonException();


        }


        public override void Write(Utf8JsonWriter writer, Tuple<DateTime, string, BlazorRoute> value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();


            writer.WriteString("Item1", JsonSerializer.Serialize<DateTime>(value.Item1));
            writer.WriteString("Item2", value.Item2);
            writer.WriteString("Item3", JsonSerializer.Serialize<BlazorRoute>(value.Item3));


            writer.WriteEndObject();
        }
    }

}
