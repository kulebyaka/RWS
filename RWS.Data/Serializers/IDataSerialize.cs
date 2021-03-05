namespace RWS.Data.Serializers
{
	public interface IDataSerialize
	{
		/// <summary>
		/// Serializes the specified data to string.
		/// </summary>
		/// <param name="data">The data.</param>
		/// <returns></returns>
		string Serialize(object data);

		/// <summary>
		/// Deserializes the specified serialized data.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="serializedData">The serialized data.</param>
		/// <returns></returns>
		T Deserialize<T>(string serializedData) where T : class, new();
	}
}