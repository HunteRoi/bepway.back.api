using System;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace API.Services
{
    public class BusinessExceptionFilter : IExceptionFilter
    {
        private readonly ILogger _logger;

        public BusinessExceptionFilter(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger("API.Exceptions");
        }

        void IExceptionFilter.OnException(ExceptionContext context)
        {
            Exception exception = context.Exception;
            _logger.LogError(context.Exception, "An unknown error occured");
            if (exception.GetType() == typeof(DbUpdateConcurrencyException))
            {
                context.Result = new ContentResult()
                {
                    StatusCode = (int)HttpStatusCode.Conflict,
                    ContentType = "application/json"
                };
            }
            else 
            {
                bool isSqlException = exception.GetType() == typeof(System.Data.SqlClient.SqlException);
                if (isSqlException || exception.GetType().IsSubclassOf(typeof(Model.BusinessException)))
                {
                    int statusCode = (int)(isSqlException ? HttpStatusCode.InternalServerError : HttpStatusCode.BadRequest);
                    string content = Newtonsoft.Json.JsonConvert.SerializeObject(new DTO.Error()
                    {
                        Message = isSqlException ? "An unexpected error occurred. Try again later!" : exception.Message
                    });
                    context.Result = new ContentResult()
                    {
                        StatusCode = statusCode,
                        Content = content,
                        ContentType = "application/json"
                    };
                }
            }
        }
    }
}