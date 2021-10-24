using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using Bam.Net.CommandLine;
using Bam.Net.Data.Repositories;
using Bam.Net.Testing;
using Bam.Net.Testing.Unit;

namespace Bam.Net.Data.Tests
{
    public class DtoTests : CommandLineTool
    {
        [ConsoleAction("dtoInstanceFor", "get dto instance for dictionary")]
        [UnitTest]
        [TestGroup("Dto")]
        public void CanGetDtoInstanceForDictionary()
        {
            dynamic val = Dto.InstanceFor("TestType", GetTestDictionary());
            Expect.IsNotNull(val);
            Expect.AreEqual(9, val.PropTwo);
            Expect.AreEqual("Hello", val.PropThree);
            OutLine(val.PropOne.ToString());
            OutLine(val.PropTwo.ToString());
            OutLine(val.PropThree.ToString());
            Pass("dtoInstanceFor");
        }
        
        [ConsoleAction("dtoTypeFor", "get dto type for dictionary")]
        [UnitTest]
        [TestGroup("Dto")]
        public void CanGetDtoForDictionary()
        {
            Type dtoType = Dto.TypeFor("TestType", GetTestDictionary());
            Expect.IsNotNull(dtoType);
            Expect.AreEqual(3, dtoType.GetProperties().Length);
            Pass("dtoTypeFor");
        }
        
        [ConsoleAction("dtoCompile", "compile dtoModel to assembly")]
        [UnitTest]
        [TestGroup("Dto")]
        public void CanCompileDtoModel()
        {
            Assembly dtoAssembly = Dto.AssemblyFor("Test.Namespace", "TestType", GetTestDictionary());
            Expect.IsNotNull(dtoAssembly);
            Expect.AreEqual(1, dtoAssembly.GetTypes().Length);
        }
        
        [ConsoleAction("dtoRender", "run dtoRender test")]
        [UnitTest]
        [TestGroup("Dto")]
        public void CanRenderDtoModel()
        {
            var dtoModel = GetTestDtoModel();

            string src = dtoModel.Render();
            
            OutLine(src);
        }

        private static DtoModel GetTestDtoModel()
        {
            Dictionary<object, object> props = GetTestDictionary();
            DtoModel dtoModel = new DtoModel("Test.Namespace", "TestType", props);
            return dtoModel;
        }

        private static Dictionary<object, object> GetTestDictionary()
        {
            Dictionary<object, object> props = new Dictionary<object, object>();
            props.Add("PropOne", DateTime.Now);
            props.Add("PropTwo", 9);
            props.Add("PropThree", "Hello");
            return props;
        }
    }
}