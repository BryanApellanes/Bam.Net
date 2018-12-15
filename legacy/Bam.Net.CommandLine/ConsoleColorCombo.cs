/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.CommandLine
{
    public class ConsoleColorCombo
    {
        public ConsoleColorCombo(ConsoleColor foreground)
        {
            this.ForegroundColor = foreground;
        }

        public ConsoleColorCombo(ConsoleColor foreground, ConsoleColor background)
            : this(foreground)
        {
            this.BackgroundColor = background;
        }

        public ConsoleColor ForegroundColor { get; set; }
        public ConsoleColor BackgroundColor { get; set; }
    }
}
