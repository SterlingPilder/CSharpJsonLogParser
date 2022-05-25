using System;
using System.Collections.Generic;
using System.Text;

namespace JsonLogParser.Models
{
	/// <summary>
	/// The individual line of a log file.
	/// </summary>
	class LogLine
	{
		public Guid id { get; set; }
		public string email { get; set; }
		public string message { get; set; }
	}
}
