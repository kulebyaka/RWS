using System;
using System.Collections.Generic;
using RWS.Data.Abstractions;

namespace RWS.Data.Serializers
{
	public class DataSerializerFactory : SimpleFactory<DataSerializerType, IDataSerialize>
	{
		public DataSerializerFactory(IDictionary<DataSerializerType, Func<IDataSerialize>> factoryMap) : base(factoryMap)
		{
		}
	}
}