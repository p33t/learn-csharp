using System;
using NUnit.Framework;

namespace extensions_csharp.NUnit
{
    public class NUnit
    {
        private readonly long _ticks = Environment.TickCount;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            Console.WriteLine(nameof(OneTimeSetup) + _ticks);
        }

        [SetUp]
        public void Setup()
        {
            Console.WriteLine(nameof(Setup) + _ticks);
        }

        [Test]
        public void BasicTest()
        {
            Console.WriteLine(nameof(BasicTest) + _ticks);
            Assert.True(true);
        }

        [TestCase(1, 2, false)]
        [TestCase(2, 2, true)]
        public void ParameterizedTest(int i, int j, bool expEquals)
        {
            Console.WriteLine(nameof(ParameterizedTest) + _ticks);
            Assert.AreEqual(expEquals, i == j);
        }

        public static (int, int, bool)[] DynamicParams() => new[]
        {
            (1, 2, false),
            (2, 2, true)
        };

        [TestCaseSource(nameof(DynamicParams))]
        public void DynamicParametersTest((int i, int j, bool expEquals) arg)
        {
            Console.WriteLine(nameof(DynamicParametersTest) + _ticks);
            Assert.AreEqual(arg.expEquals, arg.i == arg.j);
        }
    }
}