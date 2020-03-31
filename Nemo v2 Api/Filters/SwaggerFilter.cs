using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Nemo_v2_Api.Filters
{
    public class AuthorizationHeaderParameterOperationFilter: IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)  
        {  
            if (operation.Parameters == null) operation.Parameters = new List<OpenApiParameter>();  
  
            var descriptor = context.ApiDescription.ActionDescriptor as ControllerActionDescriptor;  
  
                operation.Parameters.Add(new OpenApiParameter()  
                {  
                    Name = "x-api-key",  
                    In = ParameterLocation.Header,  
                    Description = "Api Key",  
                    Required = true
                });   
            
        }  
    }
}