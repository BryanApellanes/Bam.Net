/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bam.Net.CommandLine;

namespace Bam.Net.Testing
{
    public class ConsoleBecauseWriter: IBecauseWriter, IAssertionWriter
    {
        #region IBecauseWriter Members

        public void Write(Because because)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("{0} ", because.TestDescription);
            string result = because.Passed ? "passed" : "failed";
            ConsoleColor color = because.Passed ? ConsoleColor.Green : ConsoleColor.Red;            
            Console.ForegroundColor = color;
            Console.WriteLine("{0} ", result);            
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("\tbecause ");

            if (because.Passed)
            {
                Assertion[] passedAssertions = because.Assertions;
                WritePassedAssertions(passedAssertions);
            }
            else
            {
                Assertion[] failed = (from assertion in because.Assertions
                             where !assertion.Passed
                             select assertion).ToArray();                
                Console.ForegroundColor = ConsoleColor.Red;
                WriteFailedAssertions(failed);
            }

            Console.WriteLine();
            Console.ResetColor();
        }

        public void WriteFailedAssertions(Assertion[] failed)
        {
            for (int i = 0; i < failed.Length; i++)
            {
                if (i >= 1)
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write("\tand ");
                }
                Assertion assertion = failed[i];
                Console.WriteLine("{0}", assertion.FailureMessage);
            }
        }

        public void WritePassedAssertions(Assertion[] passedAssertions)
        {
            for (int i = 0; i < passedAssertions.Length; i++)
            {
                Assertion assertion = passedAssertions[i];
                if (i >= 1)
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write("\tand ");
                }

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("{0}", assertion.SuccessMessage);
            }
        }

        #endregion
    }
}
