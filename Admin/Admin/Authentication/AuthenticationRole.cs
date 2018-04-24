using Admin.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Admin.Authentication
{
    public class AuthenticationRole : AuthorizeAttribute
    {
        private readonly string[] userAssignedRole;
        private DbOperations db = new DbOperations();

        public AuthenticationRole(params string[] roles)
        {
            this.userAssignedRole = roles;
        }
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            bool authorize = false;
            foreach (var roles in userAssignedRole)
            {
                authorize = db.CheckUserRole(httpContext.User.Identity.Name, roles);
                if (authorize)
                {
                    return authorize;
                }

            }
            return authorize;
        }
    }
}