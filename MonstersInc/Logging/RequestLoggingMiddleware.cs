using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using MonstersIncDomain;
using MonstersIncLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MonstersInc
{
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        const int DEFAULT_ERROR_CODE = 500;

        public RequestLoggingMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            _next = next;
            _logger = loggerFactory.CreateLogger<RequestLoggingMiddleware>();
        }

        public async Task Invoke(HttpContext context, ILogRepository _ilogRepository)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleException(context, ex, _ilogRepository);
            }
            finally
            {

            }
        }
        private async Task HandleException(HttpContext context, Exception ex, ILogRepository _ilogRepository)
        {
            string ex_msg = string.Empty;

            if (ex != null)
            {
                ex_msg = ex.Message;
            }

            Log entry = GetEntry(context);
            entry.ErrorMsg = ex_msg;
            entry.ResponseStatusCode = DEFAULT_ERROR_CODE;
            entry.StackTrace = Environment.StackTrace;

            await _ilogRepository.LogAsync(entry);

            context.Response.StatusCode = DEFAULT_ERROR_CODE;
            await context.Response.WriteAsync("Internal error");
        }
        private Log GetEntry(HttpContext context)
        {
            return new Log()
            {
                Time = Time.GetSystemNow(),
                RequestMethod = context.Request?.Method,
                RequestPath = context.Request?.Path.Value,
                ResponseStatusCode = (int)context.Response?.StatusCode,
            };
        }
    }
}
