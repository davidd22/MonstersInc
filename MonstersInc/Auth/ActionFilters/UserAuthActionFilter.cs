using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using MonstersIncLogic;
using MonstersIncDomain;

namespace MonstersInc.Auth.ActionFilters
{
    public class UserAuthActionFilter : Attribute, IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            Intimidator intimidator = null;
            string clientId = context.HttpContext.Request.Headers[AuthDefinition.CLIENT_ID_HEADER].ToString();

            if (!string.IsNullOrEmpty(clientId))
            {
                var intimidatorRepository = context.HttpContext.RequestServices.GetService<IintimidatorRepository>();

                intimidator = await intimidatorRepository.SelectByClientIdAsync(clientId, null);

                if (intimidator != null)
                    context.RouteData.Values.Add(AuthDefinition.INTIMIDATOR, intimidator);

            }


            if (intimidator == null)
                context.Result = new UnauthorizedResult();
            else
                await next();
        }
    }
}
