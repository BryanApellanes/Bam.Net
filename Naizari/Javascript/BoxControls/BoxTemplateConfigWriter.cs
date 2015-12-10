/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Web.UI;
using System.Web;
using Naizari.Extensions;
using Naizari.Configuration;
using System.Xml.Serialization;

namespace Naizari.Javascript.BoxControls
{
    
    /// <summary>
    /// Used to write the default template configuration for the Type specified.
    /// This class may be obsoleced by a new concept I just thought of to "evolve" the
    /// WebControl.ascx scheme of doing things.  This class may still be useful as 
    /// a utility.
    /// </summary>
    [Serializable]
    public class BoxTemplateConfigWriter
    {
        List<PropertyInfo> propertyInfoList;
        Type typeToWrite;
        List<string> propertyVariableList;
        
        // -- begin init
        public BoxTemplateConfigWriter(Type type)
        {
            this.propertyInfoList = new List<PropertyInfo>(type.GetProperties());
            this.propertyVariableList = new List<string>(GetPropertyVariableList());

            this.typeToWrite = type;
            this.AssemblyQualifiedName = type.AssemblyQualifiedName;

            this.FileName = string.Format("{0}.{1}", type.Namespace, type.Name);
        }

        private string[] GetPropertyVariableList()
        {
            List<string> propVariables = new List<string>();
            foreach (PropertyInfo info in propertyInfoList)
            {
                propVariables.Add(string.Format("$${0}$$", info.Name));
            }
            return propVariables.ToArray();
        }
        // -- end init
       
        object lockObj = new object();
        public void WriteDefaultTemplateConfig()
        {           
            string filePath = string.Format(".\\{0}.xml", this.FileName);
            HttpContext context = HttpContext.Current;
            if (context != null)
            {
                filePath = context.Server.MapPath(string.Format("~/App_Data/DefaultTemplates/{0}.xml", this.FileName));
            }

            this.Save(filePath);
        }

        public string AssemblyQualifiedName { get; set; }

        [XmlIgnore]
        public string FileName { get; set; }

        public string[] PropertyVariables
        {
            get
            {
                return this.propertyVariableList.ToArray();
            }
            set
            {
                this.propertyVariableList.Clear();
                this.propertyVariableList.AddRange(value);
            }
        }

        public void Save(string filePath)
        {
            SerializationUtil.XmlSerialize(this, filePath);
        }
    }
}
