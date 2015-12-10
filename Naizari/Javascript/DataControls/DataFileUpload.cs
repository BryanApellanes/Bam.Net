/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Naizari.Javascript;
using System.Web;
using System.Web.UI;
using Naizari.Extensions;
using System.IO;
using System.Web.UI.HtmlControls;
using Naizari.Helpers;
using Naizari.Helpers.Web;
using Naizari.Logging;
using Naizari.Test;
using Naizari.Data.Common;
using System.ComponentModel;
using Naizari.Javascript.JsonControls.Exceptions;
using Naizari.Roles;
using Naizari.Data.Access;

namespace Naizari.Javascript.DataControls
{
    public delegate void UploadComplete(DataFileUpload sender, string filePath);
    public delegate void UploadError(DataFileUpload sender, Exception ex);
    
    public class DataFileUpload: DataControl
    {
        List<string> acceptedExtensions;
        public DataFileUpload()
            : base()
        {
            this.VirtualUploadPath = "~/App_Data/Uploads/";
            this.acceptedExtensions = new List<string>();
            this.AddRequiredScript("naizari.javascript.jsui.uploadmgr.js");
            this.MessageCssClass = "";
            this.WorkingDomId = "null";
            this.InvalidFileWarning = "That file type is not allowed.";
            this.MaxSize = 4096; // 4MB default setting for httpRuntime/maxRequestLength
        }

        public event UploadComplete UploadComplete;
        public event UploadError UploadError;

        protected virtual void OnUploadComplete(string filePath)
        {
            if (this.UploadComplete != null)
                this.UploadComplete(this, filePath);
        }

        protected virtual void OnUploadError(Exception ex)
        {
            if (this.UploadError != null)
                this.UploadError(this, ex);
        }

        protected override void OnLoad(EventArgs e)
        {

            if (HttpContext.Current != null &&
                !string.IsNullOrEmpty(HttpContext.Current.Request.Params["JsonId"]) &&
                HttpContext.Current.Request.Params["JsonId"].Equals(this.JsonId))
            {
                HttpRequest request = HttpContext.Current.Request;
                HttpResponse response = HttpContext.Current.Response;
                try
                {
                    HttpPostedFile file = request.Files["file"];
                    string filepath = file.FileName;
                    string filename = Path.GetFileName(filepath);
                    string path = HttpContext.Current.Server.MapPath(this.VirtualUploadPath + filename);
                    
                    double kilobyteSize = ByteSizeConverter.BytesToKilobytes(file.ContentLength);
                    if (kilobyteSize > this.MaxSize)
                    {
                        throw new MaxFileSizeExceededException(this.MaxSize, kilobyteSize);
                    }
                    else
                    {
                        using (FileStream fs = new FileStream(path, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite, file.ContentLength))
                        {
                            byte[] fileBytes = new byte[file.ContentLength];
                            file.InputStream.Read(fileBytes, 0, file.ContentLength);
                            fs.Write(fileBytes, 0, file.ContentLength);

                            FileUpload dbEntry = FileUpload.New(this.ApplicationDatabase);
                            dbEntry.File = fileBytes;
                            dbEntry.FileName = filename;
                            dbEntry.UploadedBy = UserUtil.GetCurrentUser(true);
                            dbEntry.UploadDate = DateTime.UtcNow;
                            dbEntry.Size = Convert.ToInt64(file.ContentLength);
                            dbEntry.FullName = filepath;
                            dbEntry.ContentType = file.ContentType;
                            //dbEntry.Description = // not recording this info yet
                            if (dbEntry.Insert() == -1)
                                LogManager.CurrentLog.AddEntry("An error occurred inserting new FileUpload record (fileName={0},fileSize={1})", dbEntry.LastException, filename, kilobyteSize.ToString());
                        }
                        WriteSuccess("File uploaded successfully.");
                        this.OnUploadComplete(path);
                    }
                }
                catch (MaxFileSizeExceededException mfsee)
                {
                    LogManager.CurrentLog.AddEntry("An error occurred uploading/receiving file, max size {0} Kb, file size {1} Kb.", LogEventType.Warning, mfsee.AllowedSize.ToString(), mfsee.AttemptedSize.ToString());
                    WriteError(string.Format("File is too large, maximum size allowed is {0}.", mfsee.AllowedSize));
                    this.OnUploadError(mfsee);
                }
                catch (Exception ex)
                {
                    LogManager.CurrentLog.AddEntry("An error occurred uploading/receiving file: {0}", ex, ex.Message);
                    WriteError("An error occurred uploading your file.  We are working to correct the problem and apologize for the inconvenience.  Please try again in a few minutes.");
                    this.OnUploadError(ex);
                }
            }
            else
            {
                base.OnLoad(e);
            }
        }

        private void WriteError(string message)
        {
            Expect.IsNotNullOrEmpty(this.jsonId, "The JsonId must be explicityly set.");
            string script =
@"window.parent.JSUI.uploadMgr.uploadError('" + this.jsonId + @"', '" + message + "');";
            SendScript(script);
        }

        private static void SendScript(string script)
        {
            HtmlGenericControl scriptElement = new HtmlGenericControl("script");
            scriptElement.Attributes.Add("type", "text/javascript");
            scriptElement.Attributes.Add("language", "javascript");
            scriptElement.InnerText = script;
            HttpContextHelper.SendOnly(ControlHelper.GetRenderedString(scriptElement));
            //HttpResponse response = HttpContext.Current.Response;
            //response.Clear();
            //response.Write(ControlHelper.GetRenderedString(scriptElement));
            ////response.End();
        }

        private void WriteSuccess(string message)
        {
            Expect.IsNotNullOrEmpty(this.jsonId, "The JsonId must be explicitly set.");
            string script =
@"window.parent.JSUI.uploadMgr.uploadComplete('" + this.jsonId + @"', '" + message + "');";
            SendScript(script);
        }

        /// <summary>
        ///  This value should be less than or equal to the maxRequestLength attribute of the 
        ///  httpRuntime element of the system.web section of the web.config file.  The default
        ///  value of maxRequestLength is 4096 as well as the value of this property.
        /// </summary>
        [Description("The maximum size in kilobytes of acceptable files.")]
        public int MaxSize
        {
            get;
            set;
        }
        
        string commaSeparatedExtensions;
        /// <summary>
        /// A semicolon list of acceptable file extensions. example:
        /// .jpg;.gif;.png
        /// </summary>
        public string AcceptExtensions
        {
            get
            {
                return commaSeparatedExtensions;
            }
            set
            {
                this.commaSeparatedExtensions = value;
                this.acceptedExtensions = new List<string>(StringExtensions.SemiColonSplit(value));
            }
        }

        public string AcceptExtensionsJsRegEx
        {
            get
            {
                if (this.acceptedExtensions.Count == 0)
                    return "null"; // used by javascript

                string retVal = "/\\";
                for (int i = 0; i < this.acceptedExtensions.Count; i++)
                {
                    string ext = this.acceptedExtensions[i];
                    retVal += ext;
                    if (i != this.acceptedExtensions.Count - 1)
                        retVal += "|\\";
                }
                retVal += "/i";
                return retVal;
            }
        }

        public string VirtualUploadPath
        {
            get;
            set;
        }

        public string MessageCssClass
        {
            get;
            set;
        }

        public string WorkingDomId
        {
            get;
            set;
        }

        public string InvalidFileWarning
        {
            get;
            set;
        }

        protected override void Render(HtmlTextWriter writer)
        {
            HtmlGenericControl iFrame = new HtmlGenericControl("iframe");
            iFrame.Attributes.Add("name", this.JsonId + "_iframe");
            HtmlGenericControl hidden = new HtmlGenericControl("input");
            hidden.Attributes.Add("type", "hidden");
            hidden.Attributes.Add("name", "JsonId");
            hidden.Attributes.Add("value", this.JsonId);

            //HtmlGenericControl fileName = new HtmlGenericControl("input");
            //fileName.Attributes.Add("type", "hidden");
            //fileName.Attributes.Add("name", "filename");
            //fileName.Attributes.Add("value", this.JsonId + "_value");

            HtmlGenericControl form = new HtmlGenericControl("form");
            form.Attributes.Add("enctype", "multipart/form-data");
            form.Attributes.Add("action", HttpContextHelper.AbsolutePageUrl);
            form.Attributes.Add("target", this.JsonId + "_iframe");
            form.Attributes.Add("method", "post");
            form.Attributes.Add("jsonid", this.JsonId);


            HtmlGenericControl file = new HtmlGenericControl("input");
            file.Attributes.Add("type", "file");
            file.Attributes.Add("name", "file");
            file.Attributes.Add("onchange", "JSUI.uploadMgr.setFileName('" + this.JsonId + "');");
            form.Controls.Add(hidden);
            //form.Controls.Add(fileName);
            form.Controls.Add(file);
            iFrame.RenderControl(writer);

            HtmlGenericControl msg = ControlHelper.NewDiv(this.JsonId + "_message");
            msg.Style.Add("display", "none");
            if (!string.IsNullOrEmpty(this.MessageCssClass))
                msg.Attributes.Add("class", this.MessageCssClass);
            
            form.Controls.Add(msg);
            form.RenderControl(writer);

            if (this.RenderScripts)
                this.RenderConglomerateScript(writer);
        }
    }
}
