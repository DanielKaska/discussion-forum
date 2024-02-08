using System.Diagnostics;
using System.Linq.Expressions;
using Serilog;

namespace Forum
{
    public class ErrorMiddleware : IMiddleware
    {


        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next.Invoke(context);
            }
            catch(Exception ex)
            {
                Log.Error(ex.Message);
                Debug.WriteLine($"Error: {ex.Message}"); 
            }

        }

    }
}
