using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Common
{
    public class Result<T>
    {
        public bool IsSuccess { get; private set; }
        public T? Value { get; private set; }
        public IEnumerable<string>? Errors { get; private set; }

        public static Result<T> Success(T value) => new Result<T> { IsSuccess = true, Value = value };
        public static Result<T> Failure(string error) => new Result<T> { IsSuccess = false, Errors = new List<string> { error } };
        public static Result<T> Failure(IEnumerable<string> errors) => new Result<T> { IsSuccess = false, Errors = errors };
    }

}