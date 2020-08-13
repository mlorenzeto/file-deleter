using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FileDeleterService.Core
{
    public class LoggerService
    {
        static LoggerConfiguration Config = new LoggerConfiguration()			
			.WriteTo.File($"{ Configuration.PATH_TO_CONTENT_ROOT }{Path.AltDirectorySeparatorChar}logs{Path.AltDirectorySeparatorChar}file_deleter-.log",
						  rollingInterval: RollingInterval.Day,
						  retainedFileCountLimit: 15,
						  outputTemplate: "[{Timestamp:HH:mm:ss}] {Message:lj}{NewLine}");

		static Serilog.Core.Logger Logger = Config.CreateLogger();

		const string ERROR = "ERROR";
		const string INFO = "INFO";

		public string Entity;

		public LoggerService(string entity)
        {
			this.Entity = entity;
        }

		public void Info(string step, string message)
		{
			Logger.Information($"[{INFO}][{Entity}][{step}]".ToUpper().PadRight(50, '.') + $"{message}");
		}

		public void Error(string step, string message)
		{
			Logger.Error($"[{ERROR}][{Entity}][{step}]".ToUpper().PadRight(50, '.') + $"{message}");
		}
	}
}
