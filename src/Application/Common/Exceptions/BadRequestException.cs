using System;
using System.Collections.Generic;
using System.Linq;

namespace Application.Common.Exceptions
{
    public class BadRequestException : Exception
    {
        public IEnumerable<string> Errors { get; }

        public BadRequestException(string message)
            : base(message)
        {
            Errors = new[] { message };
        }

        public BadRequestException(IEnumerable<string> errors)
            : base("Multiple errors occurred. See error list for details.")
        {
            Errors = errors;
        }
    }
}
