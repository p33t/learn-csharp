using System.Security;
using Xunit;

namespace extensions_csharp.XUnit
{
    public class XUnit
    {
        [Fact]
        public void FactDemo()
        {
            Assert.Equal(10, 10);
        }

        // InlineData need to be compile-time constant (or string)
        [Theory]
        [InlineData("a", "a", true)]
        [InlineData("a", "b", false)]
        public void InlineDataTheoryDemo(string left, string right, bool expectEqual)
        {
            Assert.True(expectEqual == string.Equals(left, right));
        }

        public static TheoryData<string, int> StringLengthTestData = new()
        {
            {"hello", 5},
            {"bruce", 5},
            {"lee", 3}
        };

        // Use MemberData for complex data types
        [Theory]
        [MemberData(nameof(StringLengthTestData))]
        public void MemberDataTheoryDemo(string str, int expLength)
        {
            Assert.Equal(expLength, str.Length);
        }
    }
}