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
			
			services.AddScoped<IDataSourceReader, FileSystemDataSource>(_ => new FileSystemDataSource(AppSettings.InputPath));
			services.AddScoped<IDataSourceWriter, AzureFileDataSource>(_ => new AzureFileDataSource(AppSettings.OutputPath, AppSettings.AzureConnectionString));
			
			services.AddTransient(_ => new JsonSerializer());
			services.AddTransient(_ => new XmlSerializer());

			services.AddTransient<IInputSettings, InputSettings>(_ => new InputSettings() {Type = AppSettings.InputFileType});
			services.AddTransient<IOutputSettings, OutputSettings>(_ => new OutputSettings() {Type = AppSettings.OutputFileType});

			services.AddSingleton<IFactory<DataSerializerType, IDataSerializer>>
				(serviceprovider => new DataSerializerFactory(new Dictionary<DataSerializerType, Func<IDataSerializer>>() {
					{ DataSerializerType.Json, serviceprovider.GetService<JsonSerializer> },
					{ DataSerializerType.Xml, serviceprovider.GetService<XmlSerializer> }
				}));

			services.AddScoped<IMainService, MainService>();

			// build the pipeline
			_provider = services.BuildServiceProvider();
		}
	}
}