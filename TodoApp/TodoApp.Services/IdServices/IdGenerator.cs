using System;

namespace TodoApp.Services.IdServices
{
    public class IdGenerator : IIdGenerator
    {
        public Guid GenerateId()
            => Guid.NewGuid();
    }
}
