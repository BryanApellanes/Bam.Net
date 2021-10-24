/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Bam.Net.Presentation.Html
{
    /// <summary>
    /// The base class used for rendering inputs for string object 
    /// properties.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple=false)]
    public abstract partial class StringInputAttribute: Attribute
    {
        public virtual bool? BreakAfterLabel { get; set; }
        public virtual bool? AddValue { get; set; }
        public virtual bool? IsHidden { get; set; }
        public virtual bool? AddLabel { get; set; }

        internal string PropertyName { get; set; }
        public object Default { get; set; }

        public virtual Tag CreateInput()
        {
            return new Tag("input", new {type="text"}, new {});
        }

        public virtual Tag CreateInput(object data)
        {
            return InputProvider.CreateInput(InputTypes.Text, data.GetType().Name, data);
        }
    }
}
