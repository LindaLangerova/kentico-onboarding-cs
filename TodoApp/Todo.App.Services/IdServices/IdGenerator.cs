using System;

namespace Todo.App.Services.IdServices
{
    public class IdGenerator : IIdGenerator
    {
        public Guid GenerateId() => Guid.NewGuid();
    }
}
