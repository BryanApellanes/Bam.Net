using Bam.Net.Testing;
using Bam.Net.Testing.Unit;
using System;
using System.Collections.Generic;
using System.Text;
using Dt = Bam.Net.Schema.Org.DataTypes;

namespace Bam.Net.Schema.Org.Tests
{
    [Serializable]
    public class UnitTests : CommandLineTestInterface
    {
        [UnitTest("DataType.GetDataType should get expected Types")]
        public void DataTypeGetDataType()
        {
            string[] types = new string[] { "Boolean", "Date", "Number", "Text", "Time", "Url" };
            foreach (string typeName in types)
            {
                object dataType = Dt.DataType.GetDataType(typeName);
                Type sysType = Dt.DataType.GetTypeOfDataType(typeName);
                Expect.AreSame(sysType, dataType.GetType());
            }
        }

        [UnitTest("Schema Integer should be implicitly int")]
        public void SchemaInteger()
        {
            Dt.Integer three = new Dt.Integer(3);
            Dt.Integer four = new Dt.Integer(4);
            int result = AddTest(three, four);
            Expect.IsTrue(result == 7);
        }

        [UnitTest("DataType.GetDataType should get expected generic types")]
        public void DataTypeGetGenericDataType()
        {
            Dt.Boolean b = Dt.DataType.GetDataType<Dt.Boolean>("Boolean");
            Expect.IsNotNull(b);

            Dt.Date d = Dt.DataType.GetDataType<Dt.Date>("Date");
            Expect.IsNotNull(d);

            Dt.Number n = Dt.DataType.GetDataType<Dt.Number>("Number");
            Expect.IsNotNull(n);

            Dt.Text t = Dt.DataType.GetDataType<Dt.Text>("Text");
            Expect.IsNotNull(t);

            Dt.Url u = Dt.DataType.GetDataType<Dt.Url>("Url");
            Expect.IsNotNull(u);
        }

        private int AddTest(int one, int two)
        {
            return one + two;
        }
    }
}
