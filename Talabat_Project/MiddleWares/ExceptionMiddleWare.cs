using System.Net;
using System.Text.Json;
using Talabat_Project.Errors;

namespace Talabat_Project.MiddleWares
{
    public class ExceptionMiddleWare
    {
        private readonly RequestDelegate Next;
        private readonly ILogger<ExceptionMiddleWare> Logger;
        private readonly IHostEnvironment Env;

        public ExceptionMiddleWare(RequestDelegate next,ILogger<ExceptionMiddleWare>logger,IHostEnvironment env)
        {
            Next = next;
            Logger = logger;
            Env = env;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await Next.Invoke(context);
            }
            catch(Exception ex) 
            {
                Logger.LogError(ex, ex.Message);
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                //if(Env.IsDevelopment())
                //{
                //    var Response=new ApiInternalServerErrorResponse(ex.StackTrace.ToString());
                //}
                //else
                //{
                //    var Response = new ApiInternalServerErrorResponse();
                //}
                var Response = Env.IsDevelopment() ? new ApiInternalServerErrorResponse(ex.StackTrace.ToString()) : new ApiInternalServerErrorResponse();
                var Options = new JsonSerializerOptions()
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                };
                var JsonResponse=JsonSerializer.Serialize(Response,Options);
                await context.Response.WriteAsync(JsonResponse);

            }
        }
    }
}
