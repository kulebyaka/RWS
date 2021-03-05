using System.IO;

namespace RWS.Data.DataSources
{
	public class FileSystemDataSource : IDataSource
	{
		private readonly string _path;

		public FileSystemDataSource(string path)
		{
			this._path = path;
		}

		public string GetData()
		{
			using var stream = new FileStream(_path, FileMode.Open, FileAccess.Read);
			using var reader = new StreamReader(stream);
			return reader.ReadToEnd();
		}

		public void WriteData(string data)
		{
			using var sw = new StreamWriter(data);
			sw.Write(data);
		}
	}
}