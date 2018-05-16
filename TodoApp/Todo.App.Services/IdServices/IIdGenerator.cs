using System;

namespace Todo.App.Services.IdServices
{
    public interface IIdGenerator
    {
        Guid GenerateId();
    }
}
