using System.IO;
using Azure;
using Azure.Storage.Files.Shares;
using Azure.Storage.Files.Shares.Models;

namespace RWS.Data.DataSources
{
	public class AzureFileDataSource : IDataSource
	{

		private readonly string _connectionString;

		public AzureFileDataSource(string connectionString)
		{
			_connectionString = connectionString;
		}

		public string GetData()
		{

			// Name of the share, directory, and file we'll download from
			const string shareName = "sample-share";
			const string dirName = "sample-dir";
			const string fileName = "sample-file";

			// Path to the save the downloaded file
			const string localFilePath = @"<path_to_local_file>";

			// Get a reference to the file
			var share = new ShareClient(_connectionString, shareName);
			ShareDirectoryClient directory = share.GetDirectoryClient(dirName);
			ShareFileClient file = directory.GetFileClient(fileName);

			// Download the file
			ShareFileDownloadInfo download = file.Download();
			using FileStream stream = File.OpenWrite(localFilePath);
			download.Content.CopyTo(stream);
			
			var reader = new StreamReader( stream );
			string text = reader.ReadToEnd();
			return text;
		}

		public void WriteData(string str)
		{

			// Name of the share, directory, and file we'll create
			string shareName = "sample-share";
			string dirName = "sample-dir";
			string fileName = "sample-file";

			// Get a reference to a share and then create it
			var share = new ShareClient(_connectionString, shareName);
			try
			{
				// Try to create the share again
				share.Create();
			}
			catch (RequestFailedException ex)
				when (ex.ErrorCode == ShareErrorCode.ShareAlreadyExists)
			{
				// Ignore any errors if the share already exists
			}

			// Get a reference to a directory and create it
			ShareDirectoryClient directory = share.GetDirectoryClient(dirName);
			directory.Create();

			// Get a reference to a file and upload it
			ShareFileClient file = directory.GetFileClient(fileName);
			using Stream stream = GenerateStreamFromString(str);
			file.Create(stream.Length);
			file.UploadRange(new HttpRange(0, stream.Length), stream);
		}
		
		private static Stream GenerateStreamFromString(string s)
		{
			var stream = new MemoryStream();
			var writer = new StreamWriter(stream);
			writer.Write(s);
			writer.Flush();
			stream.Position = 0;
			return stream;
		}
	}
}