/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace Naizari.Extensions.Office
{
    public class ExcelCellWrapper: PropertyExtender
    {
        //List<string> ignoreProperties;

        public ExcelCellWrapper(object arg)
            : base(arg) 
        {
            //ignoreProperties = new List<string>();
        }

        public static ExcelCellWrapper FromObject(object obj)
        {
            return new ExcelCellWrapper(obj);
        }

        public static ExcelCellWrapper[] FromArray(object[] array)
        {
            List<ExcelCellWrapper> retVal = new List<ExcelCellWrapper>();
            foreach (object arg in array)
            {
                retVal.Add(new ExcelCellWrapper(arg));
            }

            return retVal.ToArray();
        }

        public string GetStyle(string propertyName)
        {
            string ext = GetExt(propertyName) as string;
            if (!string.IsNullOrEmpty(ext))
                return ext;
            else
                return string.Empty;
        }

        public override void SetExt(string propertyName, object ext)
        {
            if (ext.GetType() != typeof(string))
                throw new ArgumentException("ext must be a string", "ext");

            base.SetExt(propertyName, ext);
        }

        public void SetStyle(string propertyName, string styleId)
        {
            this.SetExt(propertyName, styleId);
        }

        //public string[] IgnoreProperties
        //{
        //    get { return ignoreProperties.ToArray(); }
        //}

        //public void Ignore(string propertyName)
        //{
        //    ignoreProperties.Add(propertyName);
        //}
    }
}
