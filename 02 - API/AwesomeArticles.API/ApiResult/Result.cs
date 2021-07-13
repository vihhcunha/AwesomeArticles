using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomeArticles.API.ApiResult
{
    public class Result
    {
        public string Message { get; private set; }
        public bool Success { get; private set; }
        public object Data { get; private set; }

        public Result(string message, bool success, object data)
        {
            Message = message;
            Success = success;
            Data = data;
        }

        public static class ResultBuilder
        {
            public static Result InternalServerError()
            {
                return new Result("Ocorreu algum erro!", false, null);
            }

            public static Result DomainError(string message)
            {
                return new Result(message, false, null);
            }
        }
    }
}
