using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SitecoreBlazorHosted.Shared.Models;
using System;
using System.IO;
using System.Text.Json.Serialization;
using Xunit;
using Foundation.BlazorExtensions.Extensions;
using System.Collections.Generic;

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
                PropertyNameCaseInsensitive = true,
                MaxDepth = 100
            };

            Route DeserializedRouteUsingNewtonSoft = JsonConvert.DeserializeObject<Route>(data);
            Route DeserializedRouteUsingSystemTextJson = System.Text.Json.Serialization.JsonSerializer.Parse<Route>(data, options);

            Assert.Equal(DeserializedRouteUsingNewtonSoft.Placeholders.Count, DeserializedRouteUsingSystemTextJson.Placeholders.Values.Count);
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
                PropertyNameCaseInsensitive = true,
                MaxDepth = 100
            };

            Route DeserializedRouteUsingSystemTextJson = System.Text.Json.Serialization.JsonSerializer.Parse<Route>(data, options);

            
            Tuple<DateTime, string, Route> someData = new Tuple<DateTime, string, Route>(DateTime.Now,"/en/carousel", DeserializedRouteUsingSystemTextJson);
            
            string jsonResult = System.Text.Json.Serialization.JsonSerializer.ToString<Tuple<DateTime, string, Route>>(someData, options);

            Assert.Contains(@"""Name"":""carousels""", jsonResult);
        }
    }
}
