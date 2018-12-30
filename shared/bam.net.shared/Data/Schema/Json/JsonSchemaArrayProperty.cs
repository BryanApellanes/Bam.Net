using System;
using System.Collections.Generic;
using System.Text;

namespace Bam.Net.Data.Schema.Json
{
    public class JsonSchemaArrayProperty: JsonSchemaProperty
    {
        /// <summary>
        /// Gets or sets the property descriptor for the type of 
        /// items in an array.
        /// </summary>
        /// <value>
        /// The items.
        /// </value>
        public string Items { get; set; }
    }
}
