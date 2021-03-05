using System.Threading;
using System.Threading.Tasks;
using RWS.Data.Abstractions;
using RWS.Data.DataSources;
using RWS.Data.Models;
using RWS.Data.Serializers;

namespace RWS.Data.Services
{
	public class MainService : IMainService
	{
		private readonly IFactory<DataSerializerType, IDataSerialize> _dataSerialzeFactory;
		private readonly IFactory<DataSourceType, IDataSource> _dataSourceFactory;

		public MainService(IFactory<DataSerializerType, IDataSerialize> dataSerialzeFactory, IFactory<DataSourceType, IDataSource> dataSourceFactory)
		{
			_dataSerialzeFactory = dataSerialzeFactory;
			_dataSourceFactory = dataSourceFactory;
		}

		public void Convert()
		{
			
			var inputType = DataSerializerType.Xml;
			var outputType = DataSerializerType.Json;
			var inputDataSource = DataSourceType.FileSystem;
			var outputDataSource = DataSourceType.AzureStorage;

			IDataSource inputReader = _dataSourceFactory.Create(inputDataSource);
			string data = inputReader.GetData();
			
			var document = _dataSerialzeFactory.Create(inputType).Deserialize<Document>(data ?? "");
			string outputString = _dataSerialzeFactory.Create(outputType).Serialize(document);
			
			IDataSource outputWriter = _dataSourceFactory.Create(outputDataSource);
			outputWriter.WriteData(outputString);
		}
	}
}