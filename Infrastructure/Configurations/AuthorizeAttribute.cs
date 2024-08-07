﻿using Domain.Constants;
using Domain.Models.Authentications;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Infrastructure.Configurations
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        public ICollection<string> Roles { get; set; }

        public AuthorizeAttribute(params string[] roles)
        {
            Roles = roles.Select(x => x.ToLower()).ToList();
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var auth = (AuthModel?)context.HttpContext.Items["USER"];
            if (auth == null)
            {
                context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
            }
            else if (auth.Status.Equals(UserStatuses.INACTIVE))
                context.Result = new JsonResult(new { message = "Account disabled" })
                    { StatusCode = StatusCodes.Status406NotAcceptable };
            else
            {
                
                var role = auth.Role;
                bool isValid = Roles == null || Roles != null && Roles.Count == 0 || Roles != null && Roles.Contains(role.ToLower());
                
                if (!isValid)
                {
                    context.Result = new JsonResult(new { message = "Forbidden" }) { StatusCode = StatusCodes.Status403Forbidden };
                }
            }
        }
    }
}
