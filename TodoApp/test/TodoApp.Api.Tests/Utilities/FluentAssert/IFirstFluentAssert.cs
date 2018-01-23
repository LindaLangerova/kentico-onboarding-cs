using System;
using NUnit.Framework.Constraints;

namespace TodoApp.Api.Tests.Utilities.FluentAssert
{
    public interface IFirstFluentAssert : IDisposable
    {
        IFluentAssert That<TActual>(TActual actual, IResolveConstraint expression);
    }
}
