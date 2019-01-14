using System;
using System.IO;
using System.Configuration;

namespace CEdotNetWsInfrastructure
{
	/// <summary>
	/// Provides methods for writing messages to a trace file configured
	/// in Web.config.
	/// </summary>
	public class Tracing
	{
		#region Write message to trace file
		/// <summary>
		/// Writes the specifies message to the trace file configured
		/// in Web.config.
		/// </summary>
		/// <param name="message">The message to write to the trace file.</param>
		public static void Write (string message)
		{
			// Retrieve the trace file from Web.config 
			string filePath = Config.GetString("TraceFile", "");

			VerifyDirectory(filePath);

			// Open the file for appending.
			StreamWriter w = File.AppendText(filePath);

			// Write the message to the trace file with time stamp.
			w.WriteLine("{0} (UTC) : {1}", DateTime.UtcNow.ToString(), message);

			// Close the file so others can get access to it.
			w.Close();
		}

		/// <summary>
		/// Verify the directory of the specified file path and
		/// create the directory if it does not exist.
		/// </summary>
		/// <param name="filePath">Path to the trace file.</param>
		private static void VerifyDirectory (string filePath)
		{
			string directoryName = "";

			// Check path to directory and create directory if necessary.
			int pos = filePath.LastIndexOf("\\");
			if (pos != -1)
			{
				// Directory path must be at least 4 characters (i.e. C:\A\).
				if (pos >= 4)
				{
					directoryName = filePath.Substring(0, pos);

					// Create the directory if it doesn't exist.
					if (!Directory.Exists(directoryName)) 
					{
						Directory.CreateDirectory(directoryName);
					}
				}
			}
		}
		#endregion
	}
}
