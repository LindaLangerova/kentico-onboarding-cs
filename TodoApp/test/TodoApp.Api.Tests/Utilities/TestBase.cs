using System;
using System.Runtime.CompilerServices;
using NUnit.Framework;
using TodoApp.Api.Tests.Utilities.FluentAssert;

namespace TodoApp.Api.Tests.Utilities
{
    [TestFixture]
    public abstract class TestBase : IDisposable
    {
        protected IFirstFluentAssert Assert { get; private set; }

        [SetUp]
        public void ResetAccumulatedExceptions()
            => Assert = new FluentAssert.FluentAssert();

        [TearDown]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void ThrowAccumulatedExceptions()
            => Assert.Dispose();

        protected virtual void Dispose(bool isCalledFromDestructor)
        {
            if (!isCalledFromDestructor)
            {
                Assert?.Dispose();
            }
        }

        public void Dispose()
        {
            Dispose(isCalledFromDestructor: false);
            GC.SuppressFinalize(this);
        }

        ~TestBase()
            => Dispose(isCalledFromDestructor: true);
    }
}