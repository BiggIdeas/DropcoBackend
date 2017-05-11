using Droplist.api.Data;
using Droplist.api.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Droplist.api.Providers
{
    public class LocalAuthorizationProvider : OAuthAuthorizationServerProvider
	{
		public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
		{
			context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });

			var db = new DroplistDataContext();
			var store = new UserStore<User>(db);

			using (var manager = new UserManager<User>(store))
			{
				var user = manager.Find(context.UserName, context.Password);

				if (user == null)
				{
					context.SetError("invalid_grant", "Incorrect username or password");
					return;
				}

                var identity = new ClaimsIdentity(context.Options.AuthenticationType);
                identity.AddClaim(new Claim(ClaimTypes.Name, context.UserName));
                identity.AddClaim(new Claim(ClaimTypes.Role, user.Employee.Role));

                var extraData = new AuthenticationProperties(new Dictionary<string, string>
                {
                    {
                        "username", user.UserName
                    },
                    {
                        "role", user.Employee.Role
                    },
					{
						"userId", user.Employee.EmployeeId.ToString()
					},
                    {
                        "buildingId", user.Employee.BuildingId.ToString()
                    }
                });

                var token = new AuthenticationTicket(identity, extraData);

                context.Validated(token);
            }

			
		}

		public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
		{
			context.Validated();
		}

        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            foreach (var extraProperty in context.Properties.Dictionary)
            {
                context.AdditionalResponseParameters.Add(extraProperty.Key, extraProperty.Value);
            }

            return Task.FromResult<object>(null);
        }
    }
}