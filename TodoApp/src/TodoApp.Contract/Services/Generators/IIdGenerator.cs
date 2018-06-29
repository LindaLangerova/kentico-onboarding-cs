using System;

namespace TodoApp.Contract.Services.Generators
{
    public interface IIdGenerator
    {
        Guid GenerateId();
    }
}
