/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bam.Net.Presentation.Html
{
    public static class StringBuilderExtensions
    {
        /// <summary>
        /// Short for Append, returns current StringBuilder to 
        /// allow for "fluent" syntax.
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static StringBuilder A(this StringBuilder builder, string value)
        {
            builder.Append(value);
            return builder;
        }

        /// <summary>
        /// Short for AppendFormat
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="format"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public static StringBuilder Af(this StringBuilder builder, string format, params string[] values)
        {
            builder.AppendFormat(format, values);
            return builder;
        }

        /// <summary>
        /// Short for AppendLine returns current StringBuilder to allow
        /// for "fluent" syntax.
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static StringBuilder Al(this StringBuilder builder, string value)
        {
            builder.AppendLine(value);
            return builder;
        }
    }
}
