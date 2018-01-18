using NUnit.Framework.Constraints;

namespace TodoApp.Api.Tests.Utilities.FluentAssert
{
    public interface IFluentAssert
    {
        IFluentAssert AndThat<TActual>(TActual actual, IResolveConstraint expression);
    }
}
