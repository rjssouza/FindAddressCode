using Infrastructure.CustomException;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Api.Filter
{
    /// <summary>
    /// Exception filter
    /// </summary>
    public class ExceptionFilter : IAsyncExceptionFilter
    {
        private readonly ILogger<ExceptionFilter> _customLogger;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="customLogger">Logger</param>
        public ExceptionFilter(ILogger<ExceptionFilter> customLogger)
        {
            _customLogger = customLogger;
        }

        /// <summary>
        /// Get Http Status code from exception 
        /// </summary>
        /// <param name="excecao">Main exception</param>
        /// <returns>Http Status Code</returns>
        private static HttpStatusCode GetHttpCode(Exception excecao)
        {
            var codigoErro = HttpStatusCode.InternalServerError;

            if (excecao is ApiException excecao1)
            {
                codigoErro = excecao1.StatusCode;
            }

            return codigoErro;
        }

        /// <summary>
        /// Format exception message to send for the client
        /// </summary>
        /// <param name="excecao">Exception thrown</param>
        /// <returns>Formatted message</returns>
        private static string? FormatMessage(Exception excecao)
        {
            var mensagem = excecao.Message;
            while (excecao.InnerException != null)
            {
                excecao = excecao.InnerException;
                mensagem += Environment.NewLine + excecao.Message;
            }

            if (excecao is ValidationException exception)
            {
                mensagem = exception.Validation.ErrorMessage;
            }

            return mensagem;
        }

        /// <summary>
        /// Method triggered en an exception is thrown
        /// </summary>
        /// <param name="context">Exception context</param>
        /// <returns>Http response</returns>
        public Task OnExceptionAsync(ExceptionContext context)
        {
            var excecao = context.Exception;
            var excecaoMensagem = FormatMessage(excecao);
            var codigoErro = GetHttpCode(excecao);

            var error = new
            {
                StatusCode = codigoErro,
                Message = excecaoMensagem,
                excecao.StackTrace
            };
            context.HttpContext.Response.StatusCode = codigoErro.GetHashCode();
            context.Result = new JsonResult(error);
            WriteLog(excecao);

            return Task.CompletedTask;
        }

        /// <summary>
        /// Write log error, can be use for telemetry purposes
        /// </summary>
        /// <param name="ex">Exception thrown</param>
        private void WriteLog(Exception ex)
        {
            _customLogger.LogError(ex, ex.Message);
        }
    }
}