/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using Naizari.Extensions;
using Naizari.Helpers;

namespace Naizari.Javascript.JsonControls
{
    public class JsonImageSequence: JsonControl
    {
        class ImageNameSrcPair
        {
            public ImageNameSrcPair(string name, string src)
            {
                this.Name = name;
                this.Src = src;
            }

            public string Name{get;set;}
            public string Src{get;set;}

            public override bool Equals(object obj)
            {
                if (obj.GetType() == typeof(ImageNameSrcPair))
                {
                    ImageNameSrcPair srcPair = (ImageNameSrcPair)obj;
                    return srcPair.Name.Equals(this.Name) && srcPair.Src.Equals(this.Src);
                }
                return base.Equals(obj);
            }

            public override int GetHashCode()
            {
                return base.GetHashCode();
            }

            public override string ToString()
            {
                return this.Name + "=" + this.Src;
            }

        }
        List<ImageNameSrcPair> images;

        public JsonImageSequence()
            : base()
        {
            this.images = new List<ImageNameSrcPair>();
            this.Width = -1;
            this.Height = -1;
        }

        public JsonImageSequence(string initialImageSource)
            : this()
        {
            this.InitialImageSource = initialImageSource;
        }

        /// <summary>
        /// Adds an image to the current JsonImageSequence instance.
        /// </summary>
        /// <param name="imageName">The name to assign to the image.  This can be used to
        /// get a reference to the image on the client using the JSUI.Images object hash.</param>
        /// <param name="src"></param>
        public void AddImage(string imageName, string src)
        {
            ImageNameSrcPair item = new ImageNameSrcPair(imageName, src);
            if (!images.Contains(item))
                images.Add(item);
        }

        public string InitialImageSource
        {
            get;
            set;
        }

        public override string JsonId
        {
            get
            {
                return base.JsonId;
            }
            set
            {
                base.JsonId = value;
                this.DomId = value;
            }
        }

        public int Width { get; set; }
        public int Height { get; set; }
        /// <summary>
        /// The name of a 
        /// client side javascript function or the JsonId of a JsonFunction to execute.
        /// </summary>
        public string ClientClickFunction { get; set; }

        public JsonFunction WireUp
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a semi-colon or dollar sign delimited list of 
        /// name to source url pairs of the images to be included in this image
        /// sequence.
        /// </summary>
        public string Images
        {
            get
            {
                string retVal = "";
                foreach (ImageNameSrcPair image in this.images)
                {
                    retVal += image.ToString() + ";";
                }

                return retVal;
            }
            set
            {
                this.images.Clear();
                string[] nameSrcPairs = StringExtensions.DelimitSplit(value, ";", "$");
                foreach (string nameSrcPair in nameSrcPairs)
                {
                    string[] split = nameSrcPair.Split('=');
                    if (split.Length != 2)
                        ExceptionHelper.Throw<JsonInvalidOperationException>("Unrecognized image name src pair specified: {0}\r\n{1}\r\n{2}", value, nameSrcPair, this.ToString());

                    this.images.Add(new ImageNameSrcPair(split[0], split[1]));
                }
            }
        }

        public bool EnableClick { get; set; }

        bool wired;
        public override void WireScriptsAndValidate()
        {
            if (!wired)
            {
                if (string.IsNullOrEmpty(InitialImageSource))
                    ExceptionHelper.ThrowInvalidOperation("The InitialImageSource property was not set: ['{0}']", this.ToString());

                JsonFunction wireup = new JsonFunction();
                string functionBody = string.Format("window.{0} = new Effects.ImageSequenceClass('{1}', {2});", this.JsonId, this.DomId, this.EnableClick ? "true": "false");
                functionBody += string.Format("\r\n{0}.AddImage('{1}', '{2}');", this.JsonId, "initial_" + this.JsonId, this.InitialImageSource);

                foreach (ImageNameSrcPair imageNameSrcPair in this.images)
                {
                    functionBody += string.Format("\r\n{0}.AddImage('{1}', '{2}');", this.JsonId, imageNameSrcPair.Name, imageNameSrcPair.Src);
                }

                if (!string.IsNullOrEmpty(this.ClientClickFunction))
                    functionBody += string.Format("\r\nJSUI.AddEventHandler('{0}', {1}, 'click');", this.DomId, this.ClientClickFunction);
                
                wireup.FunctionBody = functionBody;
                //wireup.ExecutionType = JavascriptExecutionTypes.OnWindowLoad;
                this.AddJsonFunction(wireup);
                this.WireUp = wireup;
                wired = true;
            }
        }

        protected override void Render(HtmlTextWriter writer)
        {
            base.Render(writer);

            controlToRender.TagName = "img";
            controlToRender.Attributes["src"] = this.InitialImageSource;
            controlToRender.Attributes["id"] = this.DomId;
            controlToRender.Attributes["jsonid"] = this.JsonId;
            //controlToRender.Attributes.Add("src", this.InitialImageSource);
            //controlToRender.Attributes.Add("id", this.DomId);
            //controlToRender.Attributes.Add("jsonid", this.JsonId);

            if (this.Width != -1)
                controlToRender.Attributes.Add("WIDTH", this.Width.ToString());
            if (this.Height != -1)
                controlToRender.Attributes.Add("HEIGHT", this.Height.ToString());

            controlToRender.RenderControl(writer);

           
            if (RenderScripts)
            {
                this.RenderConglomerateScript(writer);
            }
        }
    }
}
