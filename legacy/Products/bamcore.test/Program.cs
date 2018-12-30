using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Bam.Net.Logging;

namespace bamcore.test
{
    class Program
    {
        static Program()
        {
            BamResolver.Register();
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Press enter to continue");
            Console.ReadLine();
            Log.Default = new TextFileLogger { Folder = new DirectoryInfo(".\\TestLogs") };
            Log.Start();
            Log.AddEntry("Test entry");
            Log.BlockUntilEventQueueIsEmpty(1000);
            Console.WriteLine("Success");
        }
    }
}
