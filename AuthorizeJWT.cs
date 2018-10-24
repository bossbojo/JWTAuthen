using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace JWT.Authen
{
    public class AuthorizeJWT : AuthorizeAttribute
    {
        string[] roles = null;
        bool AuthorizeRole = false;
        public AuthorizeJWT(params string[] role)
        {
            this.roles = role;
        }
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            var user = Authentication.GetAuthentication<ModelAuthen>();

            var userCross = Authentication.GetAuthentication<ModelAuthenCross>();
            if (user != null && userCross != null)
            {
                //---------------Backoffice user-----------
                if (user.Auth.Username != null)
                {
                    foreach (var MyRole in user.Auth.Role)
                    {
                        foreach (var AuthorRole in roles)
                        {
                            if (MyRole == AuthorRole)
                            {
                                AuthorizeRole = true;
                            }
                        }
                    }
                    if (roles.Count() > 0)
                    {
                        if (AuthorizeRole)
                        {
                            return;
                        }
                        throw new HttpResponseException(actionContext.Request.CreateResponse(System.Net.HttpStatusCode.Forbidden, "This user don't have permission"));
                    }
                    else
                    {
                        return;
                    }
                }
                //---------------Moblie and Landing page user-----------
                if (userCross.Auth.CPhone != null)
                {
                    return;
                }
            }
            base.OnAuthorization(actionContext);
        }
    }
}