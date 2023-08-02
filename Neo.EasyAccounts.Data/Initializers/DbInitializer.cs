using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neo.EasyAccounts.Data.Initializers
{
	public class DBInitializerCreateDatabaseIfNotExists : CreateDatabaseIfNotExists<DbEntities>
	{
		protected override void Seed(DbEntities context)
		{
			InitialDataSeeder.SeedInitialData(context);

			base.Seed(context);
		}
	}
	public class DBInitializerDropCreateDatabaseAlways : DropCreateDatabaseAlways<DbEntities>
	{
		protected override void Seed(DbEntities context)
		{
			InitialDataSeeder.SeedInitialData(context);

			base.Seed(context);
		}
	}
	public class DBInitializerDropCreateDatabaseIfModelChanges : DropCreateDatabaseIfModelChanges<DbEntities>
	{
		protected override void Seed(DbEntities context)
		{
			InitialDataSeeder.SeedInitialData(context);
			base.Seed(context);
		}
	}
}
