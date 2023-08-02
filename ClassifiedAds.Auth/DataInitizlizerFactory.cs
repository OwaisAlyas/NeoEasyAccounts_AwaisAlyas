using ClassifiedAds.Auth;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neo.Auth
{
	public class DataInitializerFactory
	{
		public static void CreateDatabaseIfNotExists()
		{
			Database.SetInitializer<IdentityContext>(new IdentityContext.InitializerCreateDatabaseIfNotExists());
		}

		public static void DropCreateDatabaseAlways()
		{
			Database.SetInitializer<IdentityContext>(new IdentityContext.InitializerDropCreateDatabaseAlways());
		}

		public static void DropCreateDatabaseIfModelChanges()
		{
			Database.SetInitializer<IdentityContext>(new IdentityContext.InitializerDropCreateDatabaseIfModelChanges());
		}
	}
}
