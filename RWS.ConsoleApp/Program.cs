using Microsoft.Extensions.DependencyInjection;
using RWS.Data.Services;

namespace RWS.ConsoleApp
{
	class Program
	{
		static void Main(string[] args)
		{
			var startup = new Startup();
			var service = startup.Provider.GetRequiredService<IMainService>();
			service.Convert();
		}
	}
}