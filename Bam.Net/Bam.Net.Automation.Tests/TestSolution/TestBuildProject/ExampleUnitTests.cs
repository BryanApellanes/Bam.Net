/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Data;
using System.Data.Common;
using System.Data.Sql;
using System.Data.SqlClient;
using System.IO;
using KLGates.Core.CommandLine;
using KLGates.Core;
using KLGates.Core.Testing;
using KLGates.Core.Encryption;

namespace TestBuildProject
{
    [Serializable]
    public class ExampleUnitTests : CommandLineTestInterface
    {
        #region UnitTest samples

        /// <summary>
        /// A Basic unit test
        /// </summary>
        [UnitTest]
        public void BasicUnitTest()
        {
            string aRandomStringOfCharacters = "".RandomLetters(16);
            Expect.AreEqual(16, aRandomStringOfCharacters.Length);
        }

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

        [UnitTest]
        public void SampleUnitTestWithoutSetup()
        {
            When.A<object>("has its toString method called", (o) =>
            // o is an instance of the generic
            // type passed to When.A.  In this case
            // its just an instance of an object
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
                because.ItsTrue(/* success message */"the object under test was not null", testObj != null, /* failure message */"the object under test was null");
                because.ItsTrue(/* success message */"Big-Bird is KLGates's favorite", true, /* failure message */"big bird is not cool");
                because.ItsTrue(/* success message */"Oscar-the-Grouch is a very grouchy guy", true, /* failure message */"Oscar-the-Grouch is happy");
            })
            .SoBeHappy(c =>
            {
                // no setup so nothing to clean up.
                // clean up isn't always necessary.
            })
            .UnlessItFailed();
        }

        [UnitTest]
        public void TestFailure()
        {
            When.A<object>("is instantiated", (o) =>
            {

            })
            .TheTest
            .ShouldPass(because =>
            {
                because.ItsFalse("the test passed", true, "the test failed");
            })
            .SoBeHappy()
            .UnlessItFailed("The test failed, but in this case that's a good thing since that's what we're testing");
        }
        #endregion
    }
}
