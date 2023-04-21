using System;
using Newtonsoft.Json;
using Xunit;

namespace extensions_csharp.Newtonsoft
{
    public class NewtonsoftTests
    {
        [Fact]
        public void DateTimeRoundTrip_Works()
        {
            var utcNow = DateTime.UtcNow;
            var json = JsonConvert.SerializeObject(utcNow);
            var actual = JsonConvert.DeserializeObject<DateTime>(json);
            Assert.Equal(utcNow, actual);

            // Same as ISO string with double quotes!!!
            var iso = utcNow.ToString("O");
            Assert.Equal(json, $"\"{iso}\"");
        }
    }
}