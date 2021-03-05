using Microsoft.Extensions.DependencyInjection;
using RWS.Data.Services;

namespace RWS.ConsoleApp
{
	internal class Program
	{
		private static void Main(string[] args)
		{
			var startup = new Startup();
			var service = startup.Provider.GetRequiredService<IMainService>();
			service.Convert();
		}
	}
}