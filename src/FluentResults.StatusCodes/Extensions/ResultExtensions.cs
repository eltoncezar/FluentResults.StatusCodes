using System;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Mvc;

namespace FluentResults.StatusCodes.Extensions
{
    public static class ResultExtensions
    {
        private static string GetErrorsMessagesBase(IResultBase result)
        {
            var sb = new StringBuilder();
            var errors = result.Errors.Where(error => error.GetType() != typeof(ExceptionalError)).Select(error => error.Message);
            var message = string.Join("; ", errors);
            sb.Append(message);

            var exceptions = result.Errors.OfType<ExceptionalError>();
            foreach (var exceptionalError in exceptions)
            {
                sb.Append(exceptionalError.Exception.GetMessages());
                sb.Append(exceptionalError.Exception.StackTrace);
            }

            return sb.ToString();
        }

        public static string GetErrorsMessages(this ResultBase result)
        {
            return GetErrorsMessagesBase(result);
        }

        public static string GetMessages(this Exception exception)
        {
            return exception.InnerException != null ? $"{exception.Message}. {GetMessages(exception.InnerException)}" : exception.Message;
        }

        public static ActionResult Validate(this ResultBase result, Func<ActionResult> success = null, Action failure = null)
        {
            return new ResultControllerBase().ValidateResult(result, success, failure);
        }
    }
}