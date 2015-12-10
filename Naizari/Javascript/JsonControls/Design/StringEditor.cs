/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Design;

namespace Naizari.Javascript.JsonControls.Design
{
    public class StringEditor: ArrayEditor
    {
        public StringEditor()
            : base(typeof(string))
        { }
    }
}
