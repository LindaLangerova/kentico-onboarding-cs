using System;

namespace TodoApp.Services.IdServices
{
    public interface IIdGenerator
    {
        Guid GenerateId();
    }
}
