namespace RWS.Data.DataSources
{
	public interface IDataSource
	{
		string GetData();

		void WriteData(string data);
	}
	
	public enum DataSourceType
	{
		FileSystem,
		AzureStorage
	}
}