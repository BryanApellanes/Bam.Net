/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bam.Net.Data.Model
{
    public class ConsoleMenuContext: MenuContext
    {
        public ConsoleMenuContext(Type type, IModelActionMenuWriter writer)
            : base(type, writer)
        { }

        public ConsoleMenuContext(object val, IModelActionMenuWriter writer)
            : base(val, writer)
        { }

        public override void Show()
        {
            string menu = this.Menu.Write(this.Writer);
            Console.WriteLine(menu);
        }
    }
}
