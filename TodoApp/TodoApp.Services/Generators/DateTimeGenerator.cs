using System;
using TodoApp.Contract.Services;

namespace TodoApp.Services.Generators
{
    internal class DateTimeGenerator : IDateTimeGenerator
    {
        public DateTime GetActualDateTime()
            => DateTime.Now;
    }
}
