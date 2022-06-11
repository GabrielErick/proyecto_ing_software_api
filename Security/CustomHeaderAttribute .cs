using Microsoft.AspNetCore.Mvc.Filters;

namespace proyecto_ing_software_api.Security
{
    public class CustomHeaderAttribute : ResultFilterAttribute
    {
        public override void OnResultExecuting(ResultExecutingContext context)
        {
            context.HttpContext.Response.Headers.Add("tokensEmpresa", "facturacion response ");
            base.OnResultExecuting(context);
        }

    }
}