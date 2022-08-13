using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using System;

namespace AnimalRescue.Web
{

	public partial class Startup
	{

		/// <summary>
		/// Hook up the Owin Context to the application
		/// for Authentication
		/// </summary>
		/// <param name="app">The app.</param>
		public void ConfigureAuth(IAppBuilder app)
		{
			// Enable the application to use a cookie to store information for the signed in user
			app.UseCookieAuthentication(new CookieAuthenticationOptions
			{
				AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
				LoginPath = new PathString("/Login"),
				SlidingExpiration = true,
				ExpireTimeSpan = TimeSpan.FromMinutes(20.0)
			}) ;
		}

	}
}