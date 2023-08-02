using Neo.EasyAccounts.Data.Initializers;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neo.EasyAccounts.Data
{
	public class DataInitializerFactory
	{
		public static void CreateDatabaseIfNotExists()
		{
			Database.SetInitializer(new DBInitializerCreateDatabaseIfNotExists());
		}

		public static void DropCreateDatabaseAlways()
		{
			Database.SetInitializer(new DBInitializerDropCreateDatabaseAlways());
		}

		public static void DropCreateDatabaseIfModelChanges()
		{
			Database.SetInitializer(new DBInitializerDropCreateDatabaseIfModelChanges());
		}
	}
}
