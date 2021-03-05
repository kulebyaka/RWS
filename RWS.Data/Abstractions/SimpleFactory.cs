using System;
using System.Collections.Generic;

namespace RWS.Data.Abstractions
{
	public class SimpleFactory<TParam, TOut> : IFactory<TParam, TOut>, IFactoryInfo<TParam>
	{
		protected IDictionary<TParam, Func<TOut>> factoryMap;

		public SimpleFactory(IDictionary<TParam, Func<TOut>> factoryMap)
		{
			this.factoryMap = factoryMap ?? throw new ArgumentNullException(nameof(factoryMap));
		}

		protected internal SimpleFactory()
		{
		}

		/// <summary>
		/// Creates the specified parameter.
		/// </summary>
		/// <param name="param">The parameter.</param>
		/// <returns></returns>
		public virtual TOut Create(TParam param)
		{
			return factoryMap.ContainsKey(param) ? factoryMap[param]() : default(TOut);
		}

		public IEnumerable<TParam> Keys => factoryMap.Keys;
	}
}