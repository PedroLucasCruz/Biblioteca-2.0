using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Web.Mvc;
using StackExchange.Exceptional;
using StackExchange.Exceptional.Stores;
namespace Biblioteca._2._0.Common.Filters
{
    public class GlobalExceptionHandlingFilter
    {
        private Guid _codigo;
        private readonly ILogger<GlobalExceptionHandlingFilter> _logger;
        public readonly IConfiguration _configuration;

        public GlobalExceptionHandlingFilter(ILogger<GlobalExceptionHandlingFilter> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public void OnException(ExceptionContext context)
        {
            HttpStatusCode status = HttpStatusCode.InternalServerError;

            var exception = context.Exception;

            try
            {
                string connectionString = _configuration.GetConnectionString("DefaultConnection");

                Exceptional.Configure(new ExceptionalSettings() { DefaultStore = new SQLErrorStore(connectionString, "My Application") });
                _codigo = exception.LogNoContext().GUID;
            }
            catch (Exception ex)
            {
                _logger.LogError(1, ex, ex.Message);
            }

            var resultado = new
            {
                success = false,
                errors = $"Erro não catalogado - código: {_codigo}"
            };

           // HttpResponse response = context.HttpContext.Response;

            context.ExceptionHandled = true;
            //response.StatusCode = (int)status;
            //response.ContentType = "application/json";

            var retsul = new JsonResult();
          
            //context.Result = new JsonResult(resultado);

           // _logger.LogError(response.StatusCode, exception, exception.Message);
        }
    }
}
