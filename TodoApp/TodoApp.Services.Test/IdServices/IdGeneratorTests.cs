using NUnit.Framework;
using TodoApp.Contract.Tests.Utilities;
using TodoApp.Services.Generators;

namespace TodoApp.Services.Test.IdServices
{
    public class IdGeneratorTests : TestBase
    {
        private IdGenerator _idGenerator;

        [Test]
        public void GnerateId_UniqueIdsReceived()
        {
            _idGenerator = new IdGenerator();

            var receivedId1 = _idGenerator.GenerateId();
            var receivedId2 = _idGenerator.GenerateId();

            Assert.That(receivedId1, Is.Not.EqualTo(receivedId2));
        }
    }
}
