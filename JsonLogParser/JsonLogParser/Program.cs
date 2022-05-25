using JsonLogParser.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace JsonLogParser
{
	class Program
	{
		static void Main(string[] args)
		{
			List<DistinctLogEntry> emailAddresses = new List<DistinctLogEntry>();

			string debug = string.Empty;
			try
			{
				foreach (string fileName in Directory.GetFiles("../../../logs", "logs*.json"))
				{
					debug = $"Problem parsing file {fileName}";

					string fileContent = File.ReadAllText(fileName);

					LogFile lf = JsonConvert.DeserializeObject<LogFile>(fileContent);

					foreach (LogLine ll in lf.logs)
					{
						DistinctLogEntry dle = emailAddresses.Where(e => e.EmailAddress == ll.email && e.ID == lf.id).FirstOrDefault();

						if (dle == null)
						{
							emailAddresses.Add(new DistinctLogEntry() { EmailAddress = ll.email, ID = lf.id, Count = 1 });
						}
						else
						{
							dle.Count++;
						}
					}
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Exception {ex.Message}, {debug}");
			}

			Console.WriteLine($"There are {emailAddresses.Where(e => e.Count > 1).Count()} records with more than 1 line.");

			List<LogTallyMessage> listLogTallyMessages = TallyAllLines(emailAddresses);

			string outputFilename = "../../../logs/summary.json";			
			string jsonOutput = JsonConvert.SerializeObject(listLogTallyMessages);
			if (File.Exists(outputFilename)) File.Delete(outputFilename);
			File.WriteAllText(outputFilename, jsonOutput);

			// This line just for debugging... 
			Console.WriteLine(jsonOutput);
		}

		private static List<LogTallyMessage> TallyAllLines(List<DistinctLogEntry> emailAddresses)
		{
			List<LogTallyMessage> lltm = new List<LogTallyMessage>();

			List<Guid> DistinctIDs = emailAddresses.Select(e => e.ID).Distinct().ToList();

			foreach (Guid id in DistinctIDs)
			{
				LogTallyMessage ltm = new LogTallyMessage(id);

				List<DistinctLogEntry> distinctLogsForID = emailAddresses.Where(e => e.ID == id).ToList();

				foreach (DistinctLogEntry log in distinctLogsForID)
				{
					ltm.tally.Add(new LogTallyValue() { email = log.EmailAddress, total = log.Count });	
				}

				lltm.Add(ltm);
			}

			return lltm;
		}
	}



}
