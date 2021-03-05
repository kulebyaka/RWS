using System.Collections.Generic;

namespace RWS.Data.Abstractions
{
	public interface IFactoryInfo<TParam>
	{
		IEnumerable<TParam> Keys { get; }
	}
}