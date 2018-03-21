using System;
using System.Runtime.CompilerServices;
using NUnit.Framework;
using TodoApp.Contract.Tests.Utilities.FluentAssert;

namespace TodoApp.Contract.Tests.Utilities
{
    [TestFixture]
    public abstract class TestBase : IDisposable
    {
        [SetUp]
        public void ResetAccumulatedExceptions()
            => Assert = new FluentAssert.FluentAssert();

        [TearDown, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void ThrowAccumulatedExceptions()
            => Assert.Dispose();

        protected IFirstFluentAssert Assert { get; private set; }

        protected virtual void Dispose(bool isCalledFromDestructor)
        {
            if (!isCalledFromDestructor) Assert?.Dispose();
        }

        public void Dispose()
        {
            Dispose(false);
            GC.SuppressFinalize(this);
        }

        ~TestBase()
            => Dispose(true);
    }
}
