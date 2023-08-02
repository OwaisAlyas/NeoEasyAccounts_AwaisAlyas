
namespace Neo.Logging
{
	using System;

	public interface ILogger
	{
		/// <summary>
		/// Gets this logger's name. If the instance is the default logger, it returns an empty string.
		/// </summary>
		string Name { get; }

		/// <summary>
		/// Logs a debug message (Debug).
		/// </summary>
		/// <param name="message">The message to log.</param>
		void Debug(string message);		
		/// <summary>
		/// Logs a formatted message string with the Debug level. 
		/// </summary>
		/// <param name="format">A composite format string that contains placeholders for the arguments.</param>
		/// <param name="args">An <see cref="object"/> array containing zero or more objects to format.</param>
		void Debug(string format, params object[] args);		
		/// <summary>
		/// Logs an exception with a logging level of Debug.
		/// </summary>
		/// <param name="exception">The exception to log.</param>
		void Debug(Exception exception);
		/// <summary>
		/// Logs an exception and an additional message with a logging level of Debug.
		/// </summary>
		/// <param name="exception"> The exception to log.</param>
		/// <param name="message">Additional information regarding the logged exception.</param>
		void Debug(Exception exception, string message);
		/// <summary>
		/// Logs a formatted message string with the Debug level. 
		/// </summary>
		/// <param name="exception">The exception to log </param>
		/// <param name="format">A composite format string that contains placeholders for the arguments.</param>
		/// <param name="args">An <see cref="object"/> array containing zero or more objects to format.</param>
		void Debug(Exception exception, string format, params object[] args);

		/// <summary>
		/// Logs an informational message.
		/// </summary>
		/// <param name="message">The message to log.</param>
		void Info(string message);
		/// <summary>
		/// Logs a formatted message string with the Info level. 
		/// </summary>
		/// <param name="format">A composite format string that contains placeholders for the arguments.</param>
		/// <param name="args">An <see cref="object"/> array containing zero or more objects to format.</param>
		void Info(string format, params object[] args);
		/// <summary>
		/// Logs an exception with a logging level of Info.
		/// </summary>
		/// <param name="exception">The exception to log.</param>
		void Info(Exception exception);
		/// <summary>
		/// Logs an exception and an additional message with a logging level of
		/// Info.
		/// </summary>
		/// <param name="exception"> The exception to log.</param>
		/// <param name="message">Additional information regarding the logged exception.</param>
		void Info(Exception exception, string message);
		/// <summary>
		/// Logs a formatted message string with the Info level. 
		/// </summary>
		/// <param name="exception">The exception to log </param>
		/// <param name="format">A composite format string that contains placeholders for the arguments.</param>
		/// <param name="args">An <see cref="object"/> array containing zero or more objects to format.</param>
		void Info(Exception exception, string format, params object[] args);
		
		/// <summary>
		/// Logs a warning message.
		/// </summary>
		/// <param name="message">The message to log.</param>
		void Warn(string message);
		/// <summary>
		/// Logs a formatted message string with the Warn level. 
		/// </summary>
		/// <param name="format">A composite format string that contains placeholders for the arguments.</param>
		/// <param name="args">An <see cref="object"/> array containing zero or more objects to format.</param>
		void Warn(string format, params object[] args);
		/// <summary>
		/// Logs an exception with a logging level of Warn.
		/// </summary>
		/// <param name="exception">The exception to log.</param>
		void Warn(Exception exception);
		/// <summary>
		/// Logs an exception and an additional message with a logging level of Warn.
		/// </summary>
		/// <param name="exception"> The exception to log.</param>
		/// <param name="message">Additional information regarding the logged exception.</param>
		void Warn(Exception exception, string message);
		/// <summary>
		/// Logs a formatted message string with the Warn level. 
		/// </summary>
		/// <param name="exception">The exception to log </param>
		/// <param name="format">A composite format string that contains placeholders for the arguments.</param>
		/// <param name="args">An <see cref="object"/> array containing zero or more objects to format.</param>
		void Warn(Exception exception, string format, params object[] args);

		/// <summary>
		/// Logs an error message.
		/// </summary>
		/// <param name="message">The message to log.</param>
		void Error(string message);
		/// <summary>
		/// Logs a formatted message string with the Error level. 
		/// </summary>
		/// <param name="format">A composite format string that contains placeholders for the arguments.</param>
		/// <param name="args">An <see cref="object"/> array containing zero or more objects to format.</param>
		void Error(string format, params object[] args);
		/// <summary>
		/// Logs an exception with a logging level of Error.
		/// </summary>
		/// <param name="exception">The exception to log.</param>
		void Error(Exception exception);
		/// <summary>
		/// Logs an exception and an additional message with a logging level of Error.
		/// </summary>
		/// <param name="exception"> The exception to log.</param>
		/// <param name="message">Additional information regarding the logged exception.</param>
		void Error(Exception exception, string message);
		/// <summary>
		/// Logs a formatted message string with the Error level. 
		/// </summary>
		/// <param name="exception">The exception to log </param>
		/// <param name="format">A composite format string that contains placeholders for the arguments.</param>
		/// <param name="args">An <see cref="object"/> array containing zero or more objects to format.</param>
		void Error(Exception exception, string format, params object[] args);
				
		/// <summary>
		/// Logs a fatal error message.
		/// </summary>
		/// <param name="message">The message to log.</param>
		void Fatal(string message);
		/// <summary>
		/// Logs a formatted message string with the Fatal level. 
		/// </summary>
		/// <param name="format">A composite format string that contains placeholders for the arguments.</param>
		/// <param name="args">An <see cref="object"/> array containing zero or more objects to format.</param>
		void Fatal(string format, params object[] args);
		/// <summary>
		/// Logs an exception with a logging level of Fatal.
		/// </summary>
		/// <param name="exception">The exception to log.</param>
		void Fatal(Exception exception);
		/// <summary>
		/// Logs an exception and an additional message with a logging level of Fatal.
		/// </summary>
		/// <param name="exception"> The exception to log.</param>
		/// <param name="message">Additional information regarding the logged exception.</param>
		void Fatal(Exception exception, string message);
		/// <summary>
		/// Logs a formatted message string with the Fatal level. 
		/// </summary>
		/// <param name="exception">The exception to log </param>
		/// <param name="format">A composite format string that contains placeholders for the arguments.</param>
		/// <param name="args">An <see cref="object"/> array containing zero or more objects to format.</param>
		void Fatal(Exception exception, string format, params object[] args);
	}
}
