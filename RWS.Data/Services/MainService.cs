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
		private readonly IFactory<DataSerializerType, IDataSerializer> _dataSerialzeFactory;
		
		private readonly IDataSourceReader _dataSourceReader;
		private readonly IDataSourceWriter _dataSourceWriter;
		private readonly IInputSettings _inputTypeSettings;
		private readonly IOutputSettings _outputTypeSettings;

		public MainService(IFactory<DataSerializerType, IDataSerializer> dataSerialzeFactory, 
			IDataSourceReader dataSourceReader, IDataSourceWriter dataSourceWriter, IInputSettings inputTypeSettings, IOutputSettings outputTypeSettings)
		{
			_dataSerialzeFactory = dataSerialzeFactory;
			_dataSourceReader = dataSourceReader;
			_dataSourceWriter = dataSourceWriter;
			_inputTypeSettings = inputTypeSettings;
			_outputTypeSettings = outputTypeSettings;
		}

		public void Convert()
		{
			string data = _dataSourceReader.GetData();
			var document = _dataSerialzeFactory.Create(_inputTypeSettings.Type).Deserialize<Document>(data ?? "");
			string outputString = _dataSerialzeFactory.Create(_outputTypeSettings.Type).Serialize(document);
			_dataSourceWriter.WriteData(outputString);
		}
	}

	public interface ITypeSettings
	{
		DataSerializerType Type { get; set; }
	}
	
	public interface IInputSettings : ITypeSettings
	{
	}

	public class InputSettings : IInputSettings
	{
		public DataSerializerType Type { get; set; }
	}
	public interface IOutputSettings : ITypeSettings
	{
	}
	
	public class OutputSettings : IOutputSettings
	{
		public DataSerializerType Type { get; set; }
	}
	
}