/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.CommandLine;
using Bam.Net;
using Bam.Net.Testing;
using Bam.Net.Encryption;
using Bam.Net.Testing.Unit;

namespace Bam.Net.Documentation.Tests
{
	[Serializable]
	public class UnitTests : CommandLineTestInterface
	{
		#region UnitTest samples
		[UnitTest]
		public void SampleUnitTest()
		{
			string outter = "outer_";
			After.Setup(c =>
			// c represents the SetupContext which is itself 
			// an Incubator (dependency injection container)
			{
				c.Set<string>(() =>
				{
					return outter.RandomString(5);
				});
			})
			.WhenA<object>("has its toString method called", (o) =>
			// o is an instance of the generic
			// type passed to WhenA
			{
				o.ToString();
			})
			.TheTest
			.ShouldPass(because =>
			{
				// this is code, no way to make it fall in line, though there are other
				// ways to get at the object under test
				object testObj = because.ObjectUnderTest<object>();

				// this is an assertion
				because.ItsTrue("the object under test was not null", testObj != null, "the object under test was null");
				because.ItsFalse("the object under test was not null", testObj == null, "the object under test was null");
			})
			.SoBeHappy(c =>
			{
				OutFormat("This is from the cleanup (SoBeHappy) method, the outer text value is {0}", c.Get<string>());
			});
		}
		#endregion
	}
}
