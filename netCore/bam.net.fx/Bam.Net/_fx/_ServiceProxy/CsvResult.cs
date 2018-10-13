/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.IO;
using Bam.Net.Incubation;
using System.Reflection;

namespace Bam.Net.ServiceProxy
{
    public class CsvResult: ActionResult
    {
        public CsvResult()
        {
            CsvWriter = new CsvWriter();
        }

        public CsvResult(object data)
            : this()
        {
            Data = data;
            FileName = data.GetType().Name;
            CsvWriter = new CsvWriter { Data = data };
        }

        public CsvResult(object data, string fileName)
            : this()
        {
            Data = data;
            FileName = fileName;
            CsvWriter = new CsvWriter { Data = data };
        }
        
        public string FileName { get; set; }
        public object Data { get; set; }
        public CsvWriter CsvWriter { get; set; }

        public override void ExecuteResult(ControllerContext context)
        {
            context.HttpContext.Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName + ".csv");
            context.HttpContext.Response.AddHeader("Content-Type", "application/vnd.ms-excel");
            Stream output = context.HttpContext.Response.OutputStream;

            CsvWriter.Data = Data;
            CsvWriter.WriteCsv(output);
        }
        
        public void WriteCsv(Stream output)
        {
            CsvWriter.Data = Data;
            CsvWriter.WriteCsv(output);
        }

        public void WriteCsv(object[] toBeWritten, Stream output)
        {
            CsvWriter.WriteCsv(toBeWritten, output);
        }
    }
}
