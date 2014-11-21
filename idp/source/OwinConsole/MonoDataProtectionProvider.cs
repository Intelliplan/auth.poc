using Microsoft.Owin.Security.DataProtection;
using System;
using System.Security.Cryptography;
using System.Text;

namespace OwinConsole
{
	public class MonoDataProtectionProvider : IDataProtectionProvider
	{
		private readonly string appName;

		public MonoDataProtectionProvider()
			: this(Guid.NewGuid().ToString())
		{ }

		public MonoDataProtectionProvider(string appName)
		{
			if (appName == null) { throw new ArgumentNullException("appName"); }

			this.appName = appName;
		}

		public IDataProtector Create(params string[] purposes)
		{
			if (purposes == null) { throw new ArgumentNullException("purposes"); }

			return new MonoDataProtector(appName, purposes);
		}
	}
}

