using Bam.Net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Bam.Net
{
    public class CsvWriter
    {
        public CsvWriter()
        {
            PropertyTypeFilter = (prop) => prop.DeclaringType.IsEnum || prop.DeclaringType == typeof(string) || prop.DeclaringType == typeof(int) || prop.DeclaringType == typeof(long);
        }

        public CsvWriter(object data):this()
        {
            Args.ThrowIfNull(data, "data");
            Data = data;
            Type = data.GetType();
        }

        public object Data { get; set; }

        public Type Type { get; set; }

        /// <summary>
        /// Gets or sets the property filter.
        /// </summary>
        /// <value>
        /// The property filter.
        /// </value>
        public Func<PropertyInfo, bool> PropertyTypeFilter
        {
            get;
            set;
        }
        
        public void WriteCsv(Stream output)
        {
            object[] toBeWritten = EnsureArray();
            WriteCsv(toBeWritten, output);
        }

        public void WriteCsv(object[] toBeWritten, Stream output)
        {
            TextWriter writer = new StreamWriter(output);
            WriteHeaders(writer);

            foreach (object toWrite in toBeWritten)
            {
                WriteCsvLine(toWrite, writer);
            }

            writer.Flush();
        }

        private void WriteHeaders(TextWriter writer)
        {
            bool first = true;
            foreach (PropertyInfo prop in Type.GetProperties().Where(PropertyTypeFilter))
            {
                if (!first)
                {
                    writer.Write(",");
                }

                writer.Write(prop.Name.PascalSplit(" "));

                first = false;
            }

            writer.WriteLine();
        }

        private void WriteCsvLine(object data, TextWriter writer)
        {
            bool first = true;
            foreach (PropertyInfo prop in Type.GetProperties().Where(PropertyTypeFilter))
            {
                string value = prop.GetValue(data, null).ToString();
                string format = value.Contains(',') ? "\"{0}\"" : "{0}";
                bool replaceQuotes = value.Contains('"');
                if (replaceQuotes)
                {
                    value = value.Replace("\"", "\"\"");
                }

                if (!first)
                {
                    writer.Write(",");
                }

                writer.Write(string.Format(format, value));

                first = false;
            }
            writer.WriteLine();
        }

        private object[] EnsureArray()
        {
            object[] toBeWritten = new object[] { Data };
            if (Data is Array)
            {
                toBeWritten = (object[])Data;
            }
            return toBeWritten;
        }
    }
}
