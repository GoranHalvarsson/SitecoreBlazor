using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SitecoreBlazorHosted.Shared.Models;
using System;
using System.IO;
using System.Text.Json.Serialization;
using Xunit;
using Foundation.BlazorExtensions.Extensions;

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

            Assert.Equal(DeserializedRouteUsingSystemTextJson.Placeholders.Values.Count, DeserializedRouteUsingNewtonSoft.Placeholders.Count);
        }
    }
}
