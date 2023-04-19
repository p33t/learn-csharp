using System;
using Moq;
using Xunit;

namespace extensions_csharp.MoqDemo
{
    public class MoqDemoTests
    {
        private readonly Mock<Action<string>> _mockStringAction = new();
        
        [Fact]
        public void Basic()
        {
            _mockStringAction.Setup(a => a.Invoke(It.IsAny<string>()));
            var stringAction = _mockStringAction.Object;
            stringAction("hello");
            _mockStringAction.Verify(a => a.Invoke("hello"), Times.Once);
        }
        
        [Fact]
        public void Capture()
        {
            string actualArg = "";
            _mockStringAction.Setup(a => a.Invoke(It.IsAny<string>()))
                .Callback((string s) => actualArg = s);
            var stringAction = _mockStringAction.Object;
            stringAction("hello");
            _mockStringAction.Verify(a => a.Invoke(It.IsAny<string>()), Times.Once);
            Assert.Equal("hello", actualArg);
        }
    }
}