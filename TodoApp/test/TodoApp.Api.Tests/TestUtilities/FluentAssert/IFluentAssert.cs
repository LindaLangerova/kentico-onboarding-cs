using NUnit.Framework.Constraints;

namespace TodoApp.Api.Tests.TestUtilities.FluentAssert
{
    public interface IFluentAssert
    {
        IFluentAssert AndThat<TActual>(TActual actual, IResolveConstraint expression);
    }
}
