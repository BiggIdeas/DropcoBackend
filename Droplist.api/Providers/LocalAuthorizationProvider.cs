using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Droplist.api.data;
using Droplist.api.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security.OAuth;

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
			}

			var identity = new ClaimsIdentity(context.Options.AuthenticationType);
			identity.AddClaim(new Claim(ClaimTypes.Name, context.UserName));
			identity.AddClaim(new Claim(ClaimTypes.Role, "user"));

			context.Validated(identity);
		}

		public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
		{
			context.Validated();
		}
	}
}