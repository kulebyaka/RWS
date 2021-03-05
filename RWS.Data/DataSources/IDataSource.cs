namespace RWS.Data.DataSources
{
	public interface IDataSource : IDataSourceReader, IDataSourceWriter
	{
	}

	public interface IDataSourceReader
	{
		string GetData();
	}

	public interface IDataSourceWriter
	{
		void WriteData(string data);
	}

	public enum DataSourceType
	{
		FileSystem,
		AzureStorage
	}
}