using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Hosting.Internal;
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
			IConfigurationBuilder builder = new ConfigurationBuilder()
				.SetBasePath(Directory.GetCurrentDirectory())
				.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
				.AddJsonFile("appsettings.development.json", optional: true)
				.AddEnvironmentVariables();
			_configuration = builder.Build();
			
			
			this.AppSettings = _configuration.GetSection(nameof(AppSettings)).Get<AppSettings>();

			// instantiate
			var services = new ServiceCollection();
			services.AddSingleton<IConfiguration>(_configuration);
			
			services.AddTransient(s => new AzureFileDataSource(AppSettings.AzureConnectionString));
			services.AddTransient(s => new FileSystemDataSource(AppSettings.InputPath));
			services.AddTransient(s => new JsonSerializer());
			services.AddTransient(s => new XmlSerializer());
			
			services.AddSingleton<IFactory<DataSerializerType, IDataSerialize>>
				(serviceprovider => new DataSerializerFactory(new Dictionary<DataSerializerType, Func<IDataSerialize>>() {
					{ DataSerializerType.Json, serviceprovider.GetService<JsonSerializer> },
					{ DataSerializerType.Xml, serviceprovider.GetService<XmlSerializer> }
				}));
			
			services.AddSingleton<IFactory<DataSourceType, IDataSource>>
				(serviceprovider => new DataSourceFactory(new Dictionary<DataSourceType, Func<IDataSource>>() {
					{ DataSourceType.AzureStorage, serviceprovider.GetService<AzureFileDataSource> },
					{ DataSourceType.FileSystem, serviceprovider.GetService<FileSystemDataSource> }
				}));

			services.AddScoped<IMainService, MainService>();

			// build the pipeline
			_provider = services.BuildServiceProvider();
		}

		
	}
}