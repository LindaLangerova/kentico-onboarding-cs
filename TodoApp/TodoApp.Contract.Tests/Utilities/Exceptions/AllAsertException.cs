using System;
using System.Collections.Generic;
using System.Text;

namespace TodoApp.Contract.Tests.Utilities.Exceptions
{
    internal class AllAsertException : Exception
    {
        private readonly IList<Exception> _exceptions;

        public AllAsertException(IList<Exception> exceptions)
            => _exceptions = exceptions;

        public override string Message
        {
            get
            {
                var finalMessage = new StringBuilder();
                finalMessage.AppendLine();
                finalMessage.AppendLine();

                for (var i = 0; i < _exceptions.Count; i++)
                {
                    finalMessage.AppendLine($"{i + 1})");
                    finalMessage.AppendLine(_exceptions[i].Message);
                }

                return finalMessage.ToString();
            }
        }
    }
}
