using System.IO;

namespace RWS.Data.DataSources
{
	public class FileSystemDataSource : IDataSource
	{
		private string path;
		public FileSystemDataSource(string path)
		{
			this.path = path;
		}
		public string GetData()
		{
			var stream = new FileStream(path, FileMode.Open, FileAccess.Read);
			var reader = new StreamReader(stream);
			return reader.ReadToEnd();
		}

		public void WriteData(string data)
		{
			using var sw = new StreamWriter(data);
			sw.Write(data);
		}
	}
}