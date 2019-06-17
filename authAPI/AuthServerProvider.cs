using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace authAPI
{
    public class AuthServerProvider:OAuthAuthorizationServerProvider
    {
        private readonly Models.AppContext db = new Models.AppContext();

        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();//
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var identity = new ClaimsIdentity(context.Options.AuthenticationType);
          

            var user = db.Users.Where(x => x.UserName == context.UserName && x.Password == context.Password).FirstOrDefault();

            if (user!=null)
            {
                //identity.AddClaim(new Claim(ClaimTypes.Role, "admin"));
                identity.AddClaim(new Claim("username", context.UserName));
                identity.AddClaim(new Claim(ClaimTypes.Name, context.UserName));
                identity.AddClaim(new Claim(ClaimTypes.Role, user.Role.Name));
                context.Validated(identity);

            }
            //else if(context.UserName == "user" && context.Password == "user")
            //{
            //    identity.AddClaim(new Claim(ClaimTypes.Role, "user"));
            //    identity.AddClaim(new Claim("username", "user"));
            //    identity.AddClaim(new Claim(ClaimTypes.Name, "kresoUser"));
            //    context.Validated(identity);
            //}
            else
            {
                context.SetError("Invalid grant", "Invalid username or password");
                return;
            }
        }
    }
}