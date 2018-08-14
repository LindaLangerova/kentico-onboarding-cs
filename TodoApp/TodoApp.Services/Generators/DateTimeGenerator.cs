using System;
using TodoApp.Contract.Services.Generators;

namespace TodoApp.Services.Generators
{
    public class DateTimeGenerator : IDateTimeGenerator
    {
        public DateTime GetActualDateTime()
            => DateTime.Now;
    }
}
