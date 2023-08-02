
namespace Neo.Logging
{
	using log4net.Config;

	/// <summary>
	/// Naming of the files are explained here
	/// http://softwareengineering.stackexchange.com/questions/273157/which-design-pattern-would-be-suitable-to-abstract-logging-platforms
	/// </summary>
	public class LoggerFactory
	{
		static LoggerFactory()
		{
			// load the log4net configuration from the application configuration.
			XmlConfigurator.Configure();
		}
		public static ILogger GetLogger()
		{
			return new Log4NetAdapter();
		}
		public static ILogger GetLogger(string name)
		{
			return new Log4NetAdapter(name);
		}
	}
}
