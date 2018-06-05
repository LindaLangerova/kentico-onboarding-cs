using System;

namespace TodoApp.Contract.Services
{
    public interface IDateTimeGenerator
    {
        DateTime GetActualDateTime();
    }
}
