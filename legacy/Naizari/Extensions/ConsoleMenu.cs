/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Naizari.Extensions
{
    public delegate void ConsoleMenuDelegate(Assembly assemblyToAnalyze, ConsoleMenu[] otherMenus, string header);

    public class ConsoleMenu
    {
        public string Name { get; set; }
        public char MenuKey { get; set; }
        public ConsoleMenuDelegate Menu { get; set; }
        public Assembly AssemblyToAnalyze { get; set; }
        public string HeaderText { get; set; }
        public string FooterText { get; set; }
    }
}
