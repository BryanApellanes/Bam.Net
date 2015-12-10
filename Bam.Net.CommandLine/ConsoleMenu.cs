/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Bam.Net.CommandLine
{

    public class ConsoleMenu
    {
        public ConsoleMenu()
        {
        }        
        public string Name { get; set; }
        public char MenuKey { get; set; }
        public ConsoleMenuDelegate MenuWriter { get; set; }
        public Assembly AssemblyToAnalyze { get; set; }
        public string HeaderText { get; set; }
        public string FooterText { get; set; }

    }


}
