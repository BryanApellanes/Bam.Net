using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.CommandLine
{
    public class ConsoleMessage
    {
        public ConsoleMessage(string msg)
        {
            Text = msg;
        }

        public ConsoleMessage(string msg, ConsoleColorCombo colors): this(msg)
        {
            Colors = colors;
        }

        public ConsoleMessage(string msg, ConsoleColor textColor, ConsoleColor background = ConsoleColor.Black) : this(msg, new ConsoleColorCombo(textColor, background))
        {
        }

        public string Text { get; set; }
        public ConsoleColorCombo Colors { get; set; }
    }
}
