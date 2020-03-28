using System;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Nemo_v2_Data.Entities;
using Nemo_v2_Repo.DbContexts;

namespace Nemo_v2_Api.Filters
{
    public class AuthorizationFilterAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var apiKey = context.HttpContext.Request.Headers["x-api-key"];
            if (apiKey.Any())
            {
                // this would be your business
                if (apiKey != "tural")
                {
                    context.Result = new UnauthorizedResult();
                }
            }
            else
            {
                context.Result = new UnauthorizedResult();
            }
        }
    }
}