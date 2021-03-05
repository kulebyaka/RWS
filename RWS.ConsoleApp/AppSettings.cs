using System;
using System.IO;
using RWS.Data.Serializers;

namespace RWS.ConsoleApp
{
	public class AppSettings
	{
		public string InputPath { get; set; }
		public string OutputPath { get; set; }
		public string AzureConnectionString { get; set; }

		public DataSerializerType InputFileType
		{
			get { return DataSerializerType(InputPath); }
		}
		
		public DataSerializerType OutputFileType
		{
			get { return DataSerializerType(OutputPath); }
		}

		private DataSerializerType DataSerializerType(string path)
		{
			bool success = Enum.TryParse(Path.GetExtension(path).Substring(1), ignoreCase: true, out DataSerializerType myStatus);
			if (!success)
				throw new Exception("Unsupported file extension");
			return myStatus;
		}
	}
}