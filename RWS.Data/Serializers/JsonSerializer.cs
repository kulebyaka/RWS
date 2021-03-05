using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace RWS.Data.Serializers
{
	public class JsonSerializer : IDataSerializer
	{
		public T Deserialize<T>(string serializedData) where T : class, new()
		{
			if (string.IsNullOrWhiteSpace(serializedData))
				return null;

			return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(serializedData);
		}

		public string Serialize(object data)
		{
			var jsonSettings = new JsonSerializerSettings();

			jsonSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
			jsonSettings.NullValueHandling = NullValueHandling.Ignore;
			jsonSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;

			return Newtonsoft.Json.JsonConvert.SerializeObject(data, jsonSettings);
		}
	}
}