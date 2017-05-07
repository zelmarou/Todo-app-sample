using NSubstitute;
using NUnit.Framework;
using Shouldly;
using Todo.Services;

namespace Todo.Tests.Services
{
    [TestFixture]
    public class MyBusinessLogicServiceTests
    {
        public MyBusinessLogicService _myBusinessLogicService => new MyBusinessLogicService();
        
        [Test]
        public void DoSomeMathTest()
        {
            _myBusinessLogicService
                .DoSomeMath(2,3)
                .ShouldBe(5);
        }
    }
}
