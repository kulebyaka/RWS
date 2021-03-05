using System.IO;
using Azure;
using Azure.Storage.Files.Shares;
using Azure.Storage.Files.Shares.Models;

namespace RWS.Data.DataSources
{
	public class AzureFileDataSource : IDataSource
	{

		private readonly string _connectionString;
		private readonly string _path;
		
		// TODO[EK]: shareName => settings
		private const string ShareName = "sample-share";
		private const string DefaultDirName = "DefaultDirName";
		private string DirName => new FileInfo(_path).Directory?.Name ?? DefaultDirName;
		private string FileName => Path.GetFileName(_path);

		public AzureFileDataSource(string path, string appSettingsAzureConnectionString)
		{
			_path = path;
			_connectionString = appSettingsAzureConnectionString;
		}

		public string GetData()
		{
			// Get a reference to the file
			var share = new ShareClient(_connectionString, ShareName);
			ShareDirectoryClient directory = share.GetDirectoryClient(DirName);
			ShareFileClient file = directory.GetFileClient(FileName);

			// Download the file
			ShareFileDownloadInfo download = file.Download();
			using FileStream stream = File.OpenWrite(_path);
			download.Content.CopyTo(stream);
			
			var reader = new StreamReader( stream );
			string text = reader.ReadToEnd();
			return text;
		}

		public void WriteData(string str)
		{
			// Get a reference to a share and then create it
			var share = new ShareClient(_connectionString, ShareName);
			share.CreateIfNotExists();

			// Get a reference to a directory and create it
			ShareDirectoryClient directory = share.GetDirectoryClient(DirName);
			directory.CreateIfNotExists();

			// Get a reference to a file and upload it
			ShareFileClient file = directory.GetFileClient(FileName);
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