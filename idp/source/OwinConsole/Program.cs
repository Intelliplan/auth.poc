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

	public class IntelliplanPoC
	{
		public void Configuration(IAppBuilder app)
		{
            app.SetDataProtectionProvider(new MonoDataProtectionProvider("IntelliplanPoC"));

            var factory = InMemoryFactory.Create(
                users:   Users.Get(),
                clients: Clients.Get(),
                scopes:  Scopes.Get());

            var idsrvOptions = new IdentityServerOptions
            {
                IssuerUri = "https://sts.intelliplan.eu",
                SiteName = "Thinktecture IdentityServer v3",
                Factory = factory,
                SigningCertificate = Certificate.Get(),

                CorsPolicy = CorsPolicy.AllowAll,
                CspOptions = new CspOptions 
                {
                    ReportEndpoint = EndpointSettings.Enabled,
                },

                AuthenticationOptions = new AuthenticationOptions 
                {
                    IdentityProviders = ConfigureIdentityProviders,
                }
            };

            app.UseIdentityServer(idsrvOptions);

            // only for showing the getting started index page
            app.UseStaticFiles();
		}
        public static void ConfigureIdentityProviders(IAppBuilder app, string signInAsType){
        }
	}
}
