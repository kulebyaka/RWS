using System;
using System.Collections.Generic;
using RWS.Data.Abstractions;
using RWS.Data.Serializers;

namespace RWS.Data.DataSources
{
	public class DataSourceFactory : SimpleFactory<DataSourceType, IDataSource>
	{
		public DataSourceFactory(IDictionary<DataSourceType, Func<IDataSource>> factoryMap) : base(factoryMap)
		{
		}
	}
}