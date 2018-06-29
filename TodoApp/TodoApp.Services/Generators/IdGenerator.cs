using System;
using TodoApp.Contract.Services;
using TodoApp.Contract.Services.Generators;

namespace TodoApp.Services.Generators
{
    public class IdGenerator : IIdGenerator
    {
        public Guid GenerateId()
            => Guid.NewGuid();
    }
}
