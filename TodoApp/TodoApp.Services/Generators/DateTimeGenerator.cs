using System;
using TodoApp.Contract.Services;
using TodoApp.Contract.Services.Generators;

namespace TodoApp.Services.Generators
{
    internal class DateTimeGenerator : IDateTimeGenerator
    {
        public DateTime GetActualDateTime()
            => DateTime.Now;
    }
}
