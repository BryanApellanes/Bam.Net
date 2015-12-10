/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Naizari.Configuration;
using System.Web.UI;
using System.Web;
using System.IO;
using System.Xml.Serialization;
using System.Xml;

namespace Naizari.Javascript.JsonControls
{
    /// <summary>
    /// Intended purpose is to save a custom configuration
    /// to a read-write file accessible to the user account
    /// used by the app-pool.
    /// </summary>
    public abstract class JsonConfiguredControl: JsonControl
    {
        object fileLock = new object();

        object serializableConfig;
        Type configType;

        public object Config
        {
            get
            {
                return serializableConfig;
            }
            set
            {
                serializableConfig = value;
                configType = value.GetType();
            }
        }

        public Type ConfigType
        {
            get
            {
                return configType;
            }
            set
            {
                configType = value;
            }
        
        }

        [JsonMethod]
        public virtual void SaveConfig()
        {
            if (Config == null)
                throw new ArgumentNullException(string.Format("SerializableConfig property is not set on control of type {0}", this.GetType().Name));

            string filePath = GetDefaultFilePath();
            if (!File.Exists(filePath))
                File.Create(filePath);
            lock (fileLock)
            {
                SerializationUtil.XmlSerialize(serializableConfig, filePath);
            }
        }

        public virtual void RestoreConfig()
        {
            RestoreConfig(false);
        }


        public virtual void RestoreConfig(bool force)
        {
            if (Config == null || force)
            {
                string filePath = GetDefaultFilePath();
                Config = SerializationUtil.FromXml(filePath, ConfigType);
            }
        }

        protected string GetDefaultFilePath()
        {
            this.EnsureID();
            if (HttpContext.Current != null)
            {
                return "./";
            }
            else
            {
                HttpServerUtility server = HttpContext.Current.Server;
                return server.MapPath("~/App_Data/ControlConfig/" + this.ID + ".xml");
            }
        }
    }
}
