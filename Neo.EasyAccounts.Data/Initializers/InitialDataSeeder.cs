using Neo.EasyAccounts.Models.Domain.Locations;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;

namespace Neo.EasyAccounts.Data.Initializers
{
	internal static class InitialDataSeeder
	{
		public static void SeedInitialData(DbEntities context)
		{
			// The calling order here is necessary as the data is dependent on each other
			LocationsInitializer.SeedInitialData(context);
			AccountsInitializer.SeedInitialData(context);
			MastersInitializer.SeedInitialData(context);
			VouchersInitializer.SeedInitialData(context);
		}
	}
}