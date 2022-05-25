using System;
using System.Collections.Generic;
using System.Text;

namespace JsonLogParser.Models
{
	class LogTallyMessage
	{
		public Guid logs_id { get; set; }
		public List<LogTallyValue> tally { get; set; }

		public LogTallyMessage(Guid id)
		{
			logs_id = id;
			tally = new List<LogTallyValue>();
		}
	}
}
