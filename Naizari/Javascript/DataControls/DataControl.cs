/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Naizari.Javascript.JsonControls;
using Naizari.Roles;
using Naizari.Data.Access;
using Naizari.Helpers.Web;
using Naizari.Helpers;
using Naizari.Test;
using Naizari.Logging;
using Naizari.Extensions;
using System.Runtime.Serialization;
using Naizari.Data.Common;
using System.Web;
using System.Web.UI;
using System.IO;
using Naizari.Javascript.BoxControls;
using System.Reflection;

namespace Naizari.Javascript.DataControls
{
    public abstract class DataControl: JsonControl, IDataControl
    {
        public delegate Doodad DoodadInitDelegate(IDataControl control);

        public DataControl()
            : base()
        {
            string applicationName = LogManager.Current.ApplicationName;

            this.UserDatabase = UserDb.Current;
            this.UserDatabase.EnsureSchema<Doodad>();
            this.ApplicationDatabase = AppDb.Current;//new SQLiteAgent(@"Data Source=.\StateData\" + applicationName + ".db3;Version=3;");
            this.ApplicationDatabase.EnsureSchema<Doodad>();
            this.CrossSessionMode = CrossSessionMode.Application;
            JavascriptServer.RegisterProvider(this);
            this.DoodadInit = (d) =>
            {
                return Doodad.GetDoodad(this.jsonId, GetAgent());
            };
        }

        public DoodadInitDelegate DoodadInit
        {
            get;
            set;
        }

        object serializedConfig;
        /// <summary>
        /// Hard to visualize at this point how I'm going to 
        /// use this but, this object will be serialized to the application
        /// or user persistence database.
        /// </summary>
        protected object SerializedConfig
        {
            get
            {
                return this.serializedConfig;
            }
            set
            {
                Expect.IsTrue(CustomAttributeExtension.ClassHasAttributeOfType<SerializableAttribute>(value), "The specified config object is not marked serializable.");
                this.serializedConfig = value;
            }
        }

        string templateName;
        public string TemplateName
        {
            get
            {
                if (string.IsNullOrEmpty(this.templateName))
                    return "default";

                return this.templateName;
            }
            set
            {
                this.templateName = value;
            }

        }

        static object writeLock = new object();
        public static void WriteDefaultTemplateToDisk(Type typeToTemplate)
        {
            string filePath = HttpContext.Current.Server.MapPath(BoxServer.GetVirtualDataBoxPathWithoutFileCheck(typeToTemplate, "default"));
            string csPath = filePath + ".cs";
            if (!File.Exists(filePath) || !File.Exists(csPath))
            {
                lock (writeLock)
                {
                    string dirPath = Path.GetDirectoryName(filePath);
                    if (!Directory.Exists(dirPath))
                        Directory.CreateDirectory(dirPath);

                    string ascxRes = "naizari.javascript.datacontrols." + typeToTemplate.Name.ToLower() + ".ascx";
                    if (Resources.TextFiles.ContainsKey(ascxRes))
                    {
                        if (!File.Exists(filePath))
                        {
                            string ascx = Resources.TextFiles[ascxRes];

                            using (StreamWriter sw = new StreamWriter(filePath))
                            {
                                sw.Write(ascx.Replace("$$TypeName$$", StringExtensions.PascalCase(typeToTemplate.Name)));
                            }
                        }

                        if (!File.Exists(csPath))
                        {
                            WriteCodeBehind(typeToTemplate, csPath);
                        }    
                    }
                    
                }
            }
        }

        private static void WriteCodeBehind(Type typeToTemplate, string csPath)
        {
            string text = @"
using System;
using Naizari.Javascript.BoxControls;
using Naizari.Javascript;

[JsonProxy(""$$TypeName$$"")]
public partial class $$TypeName$$DefaultTemplate : BoxUserControl
{


    protected void Page_Load(object sender, EventArgs e)
    {

    }

}";
            using (StreamWriter sw = new StreamWriter(csPath))
            {
                sw.Write(text.Replace("$$TypeName$$", StringExtensions.PascalCase(typeToTemplate.Name)));
            }
        }

        /// <summary>
        /// Gets the HTML representing the current control.  Intended for use by
        /// the client side javascripts to "refresh" the display.
        /// </summary>
        /// <returns></returns>
        [JsonMethod]
        public virtual string GetHtml(string jsonid)
        {
            return ControlHelper.GetRenderedString(this);
        }

        /// <summary>
        /// If an implementation of this class includes an administrative "edit" mode
        /// this method is intended for use by the client side javascripts to "refresh"
        /// the display in edit mode.
        /// </summary>
        /// <returns></returns>
        [JsonMethod]
        public virtual string GetEditHtml(string jsonid)
        {
            return this.GetHtml(jsonid);
        }

        /// <summary>
        /// Get or set the mode to save cross session data. 
        /// </summary>
        public CrossSessionMode CrossSessionMode
        {
            get;
            set;
        }

        public string ApplicationPath
        {
            get
            {
                return HttpContextHelper.ApplicationPath;
            }
        }

        public string PageUrl
        {
            get
            {
                return HttpContextHelper.PageUrl;
            }
        }

        protected Doodad doodad;
        public Doodad BackingDoodad
        {
            get
            {
                if (doodad == null)
                {
                    Expect.IsNotNullOrEmpty(this.jsonId, "Unable to retrieve Doodad.  The JsonId must be explicitly set.");
                    doodad = this.DoodadInit(this);
                }
                return doodad;
            }

            set
            {
                doodad = value;
            }
        }

        public override void WireScriptsAndValidate()
        {
            base.WireScriptsAndValidate();
            this.DoodadInit(this);
        }
        /// <summary>
        /// Returns the backing database agent for the current instance.
        /// This is based on the value of the CrossSessionMode property.
        /// </summary>
        /// <returns></returns>
        public DatabaseAgent GetAgent()
        {
            DatabaseAgent agent = null;
            switch (this.CrossSessionMode)
            {
                case CrossSessionMode.Invalid:
                    break;
                case CrossSessionMode.Application:
                    agent = this.ApplicationDatabase;
                    break;
                case CrossSessionMode.User:
                    agent = this.UserDatabase;
                    break;
            }
            return agent;
        }

        protected override void OnLoad(EventArgs e)
        {
            if (this.SerializedConfig != null)
            {
                switch (this.CrossSessionMode)
                {
                    case CrossSessionMode.Invalid:
                        break;
                    case CrossSessionMode.Application:
                        SerializedObjectManager.Current.SaveAppObject(this.JsonId, this.SerializedConfig);
                        break;
                    case CrossSessionMode.User:
                        SerializedObjectManager.Current.SaveUserObject(this.JsonId, this.SerializedConfig);
                        break;
                }
            }

            base.OnLoad(e);
        }

        public SQLiteAgent UserDatabase
        {
            get;
            internal set;
        }

        public SQLiteAgent ApplicationDatabase
        {
            get;
            internal set;
        }
        
    }
}
