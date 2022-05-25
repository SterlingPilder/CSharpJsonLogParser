using System;
using System.Collections.Generic;
using System.Text;

namespace JsonLogParser.Models
{
	class DistinctLogEntry
	{
		public Guid ID { get; set; }
		public string EmailAddress { get; set; }
		public int Count { get; set; }
	}
}
