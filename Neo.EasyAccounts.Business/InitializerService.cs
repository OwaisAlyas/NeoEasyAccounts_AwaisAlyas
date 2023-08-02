using Neo.EasyAccounts.Data;
using Neo.EasyAccounts.Data.Initializers;
using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neo.EasyAccounts.Service
{
	public class InitializerServiceFactory
	{
		public static void CreateDatabaseIfNotExists()
		{
			DataInitializerFactory.CreateDatabaseIfNotExists();
		}

		public static void DropCreateDatabaseAlways()
		{
			DataInitializerFactory.DropCreateDatabaseAlways();
		}

		public static void DropCreateDatabaseIfModelChanges()
		{
			DataInitializerFactory.DropCreateDatabaseIfModelChanges();	
		}

		public static Type GetUnitofWorkType()
		{
			return typeof(UnitofWork);
		}
		public static Type GetIUnitofWorkType()
		{
			return typeof(IUnitofWork);
		}

		//public class UnitOfWorkService : UnitofWork
		//{
		//	public UnitOfWorkService(IDbFactory dbFactory)
		//	{

		//	}
		//}

		//public interface IUnitofWorkService :IUnitofWork
		//{

		//}
	}
}
