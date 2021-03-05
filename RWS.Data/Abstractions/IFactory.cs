namespace RWS.Data.Serializers
{
	public interface IFactory<in TParam, out TOut>
	{
		TOut Create(TParam param);
	}
}