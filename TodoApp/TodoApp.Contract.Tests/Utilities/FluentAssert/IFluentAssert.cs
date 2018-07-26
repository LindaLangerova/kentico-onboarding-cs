using NUnit.Framework.Constraints;

namespace TodoApp.Contract.Tests.Utilities.FluentAssert
{
    public interface IFluentAssert
    {
        IFluentAssert AndThat<TActual>(TActual actual, IResolveConstraint expression);
    }
}
