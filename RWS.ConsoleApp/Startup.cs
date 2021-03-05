using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RWS.Data.Abstractions;
using RWS.Data.DataSources;
using RWS.Data.Serializers;
using RWS.Data.Services;

namespace RWS.ConsoleApp
{
	public class Startup
	{
		private readonly IConfiguration _configuration;
		private readonly IServiceProvider _provider;
		
		// access the built service pipeline
		public IServiceProvider Provider => _provider;

		// access the built configuration
		public IConfiguration Configuration => _configuration;
		public AppSettings AppSettings { get; }


		public Startup()
		{
			_configuration = new ConfigurationBuilder()
			IConfigurationBuilder builder = new ConfigurationBuilder()
				.SetBasePath(Directory.GetCurrentDirectory())
				.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
				.AddJsonFile($"appsettings.development.json", optional: true)
				.AddEnvironmentVariables();
			_configuration = builder.Build();
			
			this.AppSettings = _configuration.GetSection(nameof(AppSettings)).Get<AppSettings>();
			
			// instantiate
			var services = new ServiceCollection();
			// services.AddSingleton<IConfiguration>(_configuration);

			// build the pipeline
			_provider = services.BuildServiceProvider();
		}

		
	}
}