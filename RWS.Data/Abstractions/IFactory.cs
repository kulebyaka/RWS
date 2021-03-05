namespace RWS.Data.Abstractions
{
	public interface IFactory<in TParam, out TOut>
	{
		TOut Create(TParam param);
	}
}