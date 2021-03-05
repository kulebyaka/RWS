using RWS.Data.Serializers;

namespace RWS.Data.Services
{
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