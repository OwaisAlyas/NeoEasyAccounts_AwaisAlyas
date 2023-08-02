using Autofac;
using Autofac.Integration.WebApi;
using Neo.EasyAccounts.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Http;

namespace Neo.EasyAccounts.Web.API
{
	public static class WebApiConfig
	{
		public static void Register(HttpConfiguration config)
		{
			// Web API configuration and services

			// Web API routes
			config.MapHttpAttributeRoutes();

			config.Routes.MapHttpRoute(
				name: "DefaultApi",
				routeTemplate: "api/{controller}/{id}",
				defaults: new { id = RouteParameter.Optional }
			);
		}

		public static IContainer GetDIContainer()
		{
			var builder = new ContainerBuilder();

			builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
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

			var container = builder.Build();

			return container;
		}
	}
}
