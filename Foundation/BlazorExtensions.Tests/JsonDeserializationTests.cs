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

            BlazorRoute deserializedRouteUsingNewtonSoft = JsonConvert.DeserializeObject<BlazorRoute>(data);
            BlazorRoute deserializedRouteUsingSystemTextJson = System.Text.Json.JsonSerializer.Deserialize<BlazorRoute>(data, options);

            Assert.Equal(deserializedRouteUsingNewtonSoft.Placeholders.Count, deserializedRouteUsingSystemTextJson.Placeholders.Values.Count);
        }

        [Fact]
        public void TestRawJssLayoutService()
        {
            string jssFile = "jss_layout_service_sample.json";
            string dataDir = "../../../Data/";

            var data = System.IO.File.ReadAllText(dataDir + jssFile);

            JsonSerializerOptions options = new JsonSerializerOptions()
            {
                IgnoreNullValues = true,
                AllowTrailingCommas = true,
                PropertyNameCaseInsensitive = true
            };

            object deserializedRouteUsingNewtonSoft = JsonConvert.DeserializeObject(data);
            Object deserializedRouteUsingSystemTextJson = System.Text.Json.JsonSerializer.Deserialize<Object>(data, options);

            Assert.NotNull(deserializedRouteUsingNewtonSoft);
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

            BlazorRoute deserializedRouteUsingSystemTextJson = System.Text.Json.JsonSerializer.Deserialize<BlazorRoute>(data, options);
            
            Tuple<DateTime, string, BlazorRoute> someData = new Tuple<DateTime, string, BlazorRoute>(DateTime.Now,"/en/carousel", deserializedRouteUsingSystemTextJson);
            
            string jsonResult = System.Text.Json.JsonSerializer.Serialize<Tuple<DateTime, string, BlazorRoute>>(someData, options);

            Assert.Contains(@"""Name"":""carousels""", jsonResult);
        }
    }
}
