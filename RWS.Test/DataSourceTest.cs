using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;
using RWS.Data;

namespace RWS.Test
{
	
	[TestFixture]
	[Parallelizable(ParallelScope.All)]
	public class DataSourceTest
	{
		[SetUp]
		public void SetUp()
		{
		}
		[Test]
		public void Test1()
		{
			var x = new[] {"0", "1"};
			x.Should().BeEquivalentTo(new List<string>() {"1","0"}, options=>options.WithStrictOrdering());
		}
	}
}