/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Logging
{
	public class LogReaderFactory
	{
		public LogReaderFactory() { }

		static LogReaderFactory _default;
		static object _defaultLock = new object();
		public static LogReaderFactory Default
		{
			get
			{
				return _defaultLock.DoubleCheckLock(ref _default, () => new LogReaderFactory());
			}
		}

		Dictionary<Type, Func<ILogReader>> _readers = new Dictionary<Type, Func<ILogReader>>();

        /// <summary>
        /// Registers the specified resolver for the spcified generic ILogger T.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="resolver">The resolver.</param>
        public void Register<T>(Func<ILogReader> resolver) where T: ILogger
		{
			if (_readers.ContainsKey(typeof(T)))
			{
				_readers[typeof(T)] = resolver;
			}
			else
			{
				_readers.Add(typeof(T), resolver);
			}
		}

		public ILogReader GetLogReader(ILogger logger)
		{
			Args.ThrowIfNull(logger, "logger");
			Type loggerType = logger.GetType();
			if (_readers.ContainsKey(loggerType))
			{
				return _readers[loggerType]();
			}
			return null;
		}
	}
}
