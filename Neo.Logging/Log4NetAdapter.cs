
namespace Neo.Logging
{
	using log4net;
	using System;
	using System.Reflection;

	internal class Log4NetAdapter : ILogger
	{
		private readonly ILog log;

		public string Name { get; private set; }

		/// <summary>
		/// Constructs a logger instance with the given name.
		/// </summary>
		/// <param name="name">the logger name</param>
		public Log4NetAdapter(string name)
		{
			log = LogManager.GetLogger(name);
		}

		/// <summary>
		/// Constructs an un-named logger
		/// </summary>
		public Log4NetAdapter()
		{
			Name = string.Empty;
			log = LogManager.GetLogger(Name);
		}

		public void Debug(string message)
		{
			log.Debug(message);
		}
		public void Debug(string format, params object[] args)
		{
			log.DebugFormat(format, args);
		}
		public void Debug(Exception exception)
		{
			log.Debug(exception);
		}
		public void Debug(Exception exception, string message)
		{
			log.Debug(message, exception);
		}
		public void Debug(Exception exception, string format, params object[] args)
		{
			log.Debug(string.Format(format, args), exception);
		}

		public void Info(string message)
		{
			log.Info(message);
		}
		public void Info(string format, params object[] args)
		{
			log.InfoFormat(format, args);
		}
		public void Info(Exception exception)
		{
			log.Info(exception);
		}
		public void Info(Exception exception, string message)
		{
			log.Info(message, exception);
		}
		public void Info(Exception exception, string format, params object[] args)
		{
			log.Info(string.Format(format, args), exception);
		}

		public void Warn(string message)
		{
			log.Warn(message);
		}
		public void Warn(string format, params object[] args)
		{
			log.WarnFormat(format, args);
		}
		public void Warn(Exception exception)
		{
			log.Warn(exception);
		}
		public void Warn(Exception exception, string message)
		{
			log.Warn(message, exception);
		}
		public void Warn(Exception exception, string format, params object[] args)
		{
			log.Warn(string.Format(format, args), exception);
		}

		public void Error(string message)
		{
			log.Warn(message);
		}
		public void Error(string format, params object[] args)
		{
			log.ErrorFormat(format, args);
		}
		public void Error(Exception exception)
		{
			log.Error(exception);
		}
		public void Error(Exception exception, string message)
		{
			log.Error(message, exception);
		}
		public void Error(Exception exception, string format, params object[] args)
		{
			log.Error(string.Format(format, args), exception);
		}

		public void Fatal(string message)
		{
			log.Fatal(message);
		}
		public void Fatal(string format, params object[] args)
		{
			log.FatalFormat(format, args);
		}
		public void Fatal(Exception exception)
		{
			log.Fatal(exception);
		}
		public void Fatal(Exception exception, string message)
		{
			log.Fatal(message, exception);
		}
		public void Fatal(Exception exception, string format, params object[] args)
		{
			log.Fatal(string.Format(format, args), exception);
		}
	}
}
