using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Droplist.api.Providers;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;

namespace Droplist.api
{
	public class Startup
	{
		public void Configuration(IAppBuilder app)
		{
			ConfigureOAuth(app);
			var config = new HttpConfiguration();
			WebApiConfig.Register(config);
			app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
			app.UseWebApi(config);
		}
		public void ConfigureOAuth(IAppBuilder app)
		{
			var authorizationOptions = new OAuthAuthorizationServerOptions
			{
				AllowInsecureHttp = true,
				TokenEndpointPath = new PathString("/api/users/login"),
				AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
				Provider = new LocalAuthorizationProvider()
			};

			var authenticationOptions = new OAuthBearerAuthenticationOptions();

			app.UseOAuthAuthorizationServer(authorizationOptions);
			app.UseOAuthBearerAuthentication(authenticationOptions);
		}
	}
}
