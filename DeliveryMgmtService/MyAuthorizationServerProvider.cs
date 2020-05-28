using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Owin.Security.OAuth;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DeliveryMgmtService
{
    public class MyAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }
        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
               UserMasterRepository usermasterrepo = new UserMasterRepository();
                var user = usermasterrepo.ValidateUser(context.UserName, context.Password);
                if (!user)
                {
                    context.SetError("invalid_grant", "Provided username and password is incorrect");
                    return;
                }
                
                var identity = new ClaimsIdentity(context.Options.AuthenticationType);
                identity.AddClaim(new Claim(ClaimTypes.Role, "SuperAdmin"));
                identity.AddClaim(new Claim(ClaimTypes.Name, "amarnath"));
                identity.AddClaim(new Claim("Email", "amar.m16@gmail.com"));
                context.Validated(identity);
          
        }
    }
}