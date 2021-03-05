using System;
using System.Collections.Generic;
using RWS.Data.Abstractions;

namespace RWS.Data.Serializers
{
	public class DataSerializerFactory : SimpleFactory<DataSerializerType, IDataSerializer>
	{
		public DataSerializerFactory(IDictionary<DataSerializerType, Func<IDataSerializer>> factoryMap) : base(factoryMap)
		{
		}
	}
}