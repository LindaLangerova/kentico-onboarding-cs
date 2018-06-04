using System;

namespace TodoApp.Contract.Services
{
    public interface IIdGenerator
    {
        Guid GenerateId();
    }
}
