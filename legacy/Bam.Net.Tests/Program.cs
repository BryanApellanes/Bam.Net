/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Reflection;
using Bam.Net.Testing;

namespace Bam.Net.Tests
{
    [Serializable]
    class Program: CommandLineTestInterface
    {
        static void Main(string[] args)
        {
            DefaultMethod = typeof(Program).GetMethod("Start");

            Initialize(args);
        }

        #region do not modify
        public static void Start()
        {
            if (Arguments.Contains("t"))
            {
                RunAllUnitTests(Assembly.GetExecutingAssembly());
            }
            else
            {
                Interactive();
            }
        }
        #endregion
    }
}
