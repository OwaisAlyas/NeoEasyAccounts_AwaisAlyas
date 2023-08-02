using Autofac;
using Autofac.Integration.Mvc;
using Neo.EasyAccounts.Data;
using System.Reflection;
using System.Web.Mvc;

namespace Neo.EasyAccounts.Web.UI
{
	public static class AutoFacBootsrtapper
	{

		public static void Run()
		{
			SetAutofacContainer();
		}

		private static void SetAutofacContainer()
		{
			var builder = new ContainerBuilder();
			builder.RegisterControllers(Assembly.GetExecutingAssembly());

			builder.RegisterType<UnitofWork>().As<IUnitofWork>().InstancePerRequest();
			builder.RegisterType<DbFactory>().As<IDbFactory>().InstancePerRequest();

			// Repositories
			builder.RegisterAssemblyTypes(typeof(Data.AutoFacBooter).Assembly)
				.Where(t => t.Name.EndsWith("Repository"))
				.AsImplementedInterfaces().InstancePerRequest();
			// Services
			builder.RegisterAssemblyTypes(typeof(Service.AutoFacBooter).Assembly)
				.Where(t => t.Name.EndsWith("Service"))
				.AsImplementedInterfaces().InstancePerRequest();

			// Cache
			builder.RegisterAssemblyTypes(typeof(Caching.InMemoryCache).Assembly)
				.Where(t => t.Name.EndsWith("Cache"))
				.AsImplementedInterfaces().InstancePerRequest();

			var container = builder.Build();
			DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
		}
	}
}