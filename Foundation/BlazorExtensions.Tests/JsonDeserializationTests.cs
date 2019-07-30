using Newtonsoft.Json;
using SitecoreBlazorHosted.Shared.Models;
using System;
using System.Text.Json;
using Xunit;

namespace BlazorExtensions.Tests
{
    public class JsonDeserializationTests
    {
        [Fact]
        public void CompareSystemTextJsonWithNewtonSoftTest()
        {
            string carouselFile = "Carousel/en.json";
            string dataDir = "../../../Data/";
            
            var data = System.IO.File.ReadAllText(dataDir + carouselFile);

            JsonSerializerOptions options = new JsonSerializerOptions()
            {
                IgnoreNullValues = true,
                AllowTrailingCommas = true,
                PropertyNameCaseInsensitive = true
            };

            Route deserializedRouteUsingNewtonSoft = JsonConvert.DeserializeObject<Route>(data);
            Route deserializedRouteUsingSystemTextJson = System.Text.Json.JsonSerializer.Deserialize<Route>(data, options);

            Assert.Equal(deserializedRouteUsingNewtonSoft.Placeholders.Count, deserializedRouteUsingSystemTextJson.Placeholders.Values.Count);
        }


        [Fact]
        public void SerializeToStringTest()
        {
            string carouselFile = "Carousel/en.json";
            string dataDir = "../../../Data/";

            var data = System.IO.File.ReadAllText(dataDir + carouselFile);

            JsonSerializerOptions options = new JsonSerializerOptions()
            {
                IgnoreNullValues = true,
                AllowTrailingCommas = true,
                PropertyNameCaseInsensitive = true
            };

            Route deserializedRouteUsingSystemTextJson = System.Text.Json.JsonSerializer.Deserialize<Route>(data, options);
            
            Tuple<DateTime, string, Route> someData = new Tuple<DateTime, string, Route>(DateTime.Now,"/en/carousel", deserializedRouteUsingSystemTextJson);
            
            string jsonResult = System.Text.Json.JsonSerializer.Serialize<Tuple<DateTime, string, Route>>(someData, options);

            Assert.Contains(@"""Name"":""carousels""", jsonResult);
        }
    }
}
