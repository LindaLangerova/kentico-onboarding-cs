using System;
using TodoApp.Contract.Services.Generators;

namespace TodoApp.Services.Generators
{
    internal class IdGenerator : IIdGenerator
    {
        public Guid GenerateId()
            => Guid.NewGuid();
    }
}
