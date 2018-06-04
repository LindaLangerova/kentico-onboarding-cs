using System;
using TodoApp.Contract.Services;

namespace TodoApp.Services.Generators
{
    public class IdGenerator : IIdGenerator
    {
        public Guid GenerateId()
            => Guid.NewGuid();
    }
}
