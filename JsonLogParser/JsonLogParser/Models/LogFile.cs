using System;
using System.Collections.Generic;
using System.Text;

namespace JsonLogParser.Models
{
	/// <summary>
	///  The JSON format of the source log file
	/// </summary>
	class LogFile
	{
		public List<LogLine> logs { get; set; }
		public Guid id { get; set; }

		LogFile()
		{
			logs = new List<LogLine>();
		}
	}
}
