using System.Collections;
using System.Xml.Linq;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using RWS.Data.Serializers;

namespace RWS.Test
{
	[TestFixture]
	[Parallelizable(ParallelScope.All)]
	[TestFixtureSource(typeof(FixtureTestData), nameof(FixtureTestData.Fixtureparams))]
	public class SerializerTestFixture
	{
		
		private IDataSerializer _dataSerializer;
		private readonly SerializerTestClass _serializableObject;

		public SerializerTestFixture(SerializerTestClass serializableObject)
		{
			this._serializableObject = serializableObject;
		}
 
		[SetUp]
		public void SetUp()
		{
			var services = new ServiceCollection();
			services.AddTransient<IDataSerializer, XmlSerializer>();
			ServiceProvider serviceProvider = services.BuildServiceProvider();
			_dataSerializer = serviceProvider.GetService<IDataSerializer>();
		}

		[Test]
		public void Test1()
		{
			string x = _dataSerializer.Serialize(_serializableObject);
			XDocument xDocument = XDocument.Parse(x);
			xDocument.Should().HaveRoot(nameof(SerializerTestClass));
			XElement xElement = xDocument.Root;
			xElement.Should().HaveElement(nameof(_serializableObject.PropInt))
				.Subject.Value.Should().Match(_serializableObject.PropInt.ToString());
			xElement.Should().HaveElement(nameof(_serializableObject.PropString))
				.Subject.Value.Should().Match(_serializableObject.PropString);
		}
	}
	
	public class FixtureTestData
	{
		public static IEnumerable Fixtureparams
		{
			get
			{
				yield return new TestFixtureData(new SerializerTestClass()
				{
					PropInt = 123,
					PropString = "P"
				});
				yield return new TestFixtureData(new SerializerTestClass()
				{
					PropInt = 444,
					PropString = "x"
				});
			}
		}  
	}
}