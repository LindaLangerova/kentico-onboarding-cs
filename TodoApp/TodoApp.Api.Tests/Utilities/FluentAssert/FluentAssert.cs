using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace TodoApp.Api.Tests.Utilities.FluentAssert
{
    public sealed class FluentAssert : IFirstFluentAssert, IFluentAssert
    {
        private readonly IList<Exception> _accumulatedExceptions = new List<Exception>();
        private string _message = "";

        public IFluentAssert That<TActual>(TActual actual, IResolveConstraint expression)
        {
            try
            {
                Assert.That(actual, expression);
            }
            catch (Exception ex)
            {
                _accumulatedExceptions.Add(ex);
                _message += ex.Message;
            }

            return this;
        }

        public IFluentAssert AndThat<TActual>(TActual actual, IResolveConstraint expression)
            => That(actual, expression);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Dispose()
        {
            if (_accumulatedExceptions.Any())
            {
                throw new AggregateException("\n" + _message);
            }
        }
    }
}