using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework.Constraints;

namespace TodoApp.Api.Tests.Utilities.FluentAssert
{
    public interface IFluentAssert
    {
        IFluentAssert AndThat<TActual>(TActual actual, IResolveConstraint expression);
    }
}
