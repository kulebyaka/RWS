﻿using System;
using System.Collections.Generic;

namespace RWS.Data.Abstractions
{
	public class SimpleFactory<TParam, TOut> : IFactory<TParam, TOut>, IFactoryInfo<TParam>
	{
		private readonly IDictionary<TParam, Func<TOut>> _factoryMap;
		
		public SimpleFactory(IDictionary<TParam, Func<TOut>> factoryMap)
		{
			_factoryMap = factoryMap ?? throw new ArgumentNullException(nameof(factoryMap));
		}

		protected internal SimpleFactory()
		{
		}

		public IEnumerable<TParam> Keys => _factoryMap.Keys;
		
		/// <summary>
		/// Creates the specified parameter.
		/// </summary>
		public virtual TOut Create(TParam param)
		{
			return _factoryMap.ContainsKey(param) ? _factoryMap[param]() : default;
		}
	}
}