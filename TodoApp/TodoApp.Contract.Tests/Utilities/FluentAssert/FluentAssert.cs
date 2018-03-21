using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using TodoApp.Contract.Tests.Utilities.Exceptions;

namespace TodoApp.Contract.Tests.Utilities.FluentAssert
{
    public sealed class FluentAssert : IFirstFluentAssert, IFluentAssert
    {
        private readonly IList<Exception> _accumulatedExceptions = new List<Exception>();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IFluentAssert That<TActual>(TActual actual, IResolveConstraint expression)
        {
            try
            {
                Assert.That(actual, expression);
            } catch (Exception ex)
            {
                _accumulatedExceptions.Add(ex);
            }

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Dispose()
        {
            if (_accumulatedExceptions.Any()) throw new AllAsertException(_accumulatedExceptions);
        }

        public IFluentAssert AndThat<TActual>(TActual actual, IResolveConstraint expression)
            => That(actual, expression);
    }
}
