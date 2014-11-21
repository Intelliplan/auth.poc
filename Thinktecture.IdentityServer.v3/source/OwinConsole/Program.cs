using System;
using Owin;
using Microsoft.Owin.Security.DataProtection;
using Thinktecture.IdentityServer.Core.Configuration;
using System.Collections.Generic;
using Thinktecture.IdentityServer.Core.Services.InMemory;
using System.Security.Claims;
using Thinktecture.IdentityServer.Core.Models;
using System.Security.Cryptography.X509Certificates;

namespace OwinConsole
{
	class Users{
		public static IEnumerable<InMemoryUser> Get(){
			var user = new InMemoryUser ();
			user.Password = "foobar";
			user.Provider = "intelliplan";
			user.Enabled = true;

			// might want to set up claims...
			return new List<InMemoryUser> { user };
		}
	}
	class Clients{
		public static IEnumerable<Client> Get(){
			var client = new Client ();
			client.ClientName = "Intelliplan";

			return new List<Client> { client };
		}
	}
	class Scopes{
		public static IEnumerable<Scope> Get(){
			var scope = new Scope ();
			scope.Enabled = true;
			return new List<Scope> { scope };
		}
	}
	class Certificate{
		public static X509Certificate2 Get(){
			var certificate = new X509Certificate2 ();
			return certificate;
		}
	}

	class Application{
		public Dictionary<string,object> Properties{
			get{
				return new Dictionary<string,object> {
					{ "host.AppName", "OwinConsole" }
				};
			}
		}
	}

	class MainClass
	{
		Application app = new Application();

		public void Configuration(IAppBuilder appBuilder)
		{
			appBuilder.SetDataProtectionProvider(new MonoDataProtectionProvider(app.Properties["host.AppName"] as string));

			var factory = InMemoryFactory.Create(
				users:   Users.Get(),
				clients: Clients.Get(),
				scopes:  Scopes.Get());

			var options = new IdentityServerOptions
			{
				IssuerUri = "https://idsrv3.com",
				SiteName = "Thinktecture IdentityServer v3",
				SigningCertificate = Certificate.Get(),
				Factory = factory,
			};

			appBuilder.UseIdentityServer(options);
		}

		public static void Main (string[] args)
		{
			Console.WriteLine ("Hello World!");
		}
	}
}
