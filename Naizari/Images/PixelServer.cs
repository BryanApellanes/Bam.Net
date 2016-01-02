/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Reflection;
using System.Web;
using Naizari.Configuration;
using Naizari.Helpers;
using Naizari.Logging;

namespace Naizari.Images
{
    public class PixelServer : IHttpHandler
    {
        static Bitmap notFoundImage;
        public const string EmbeddedPath = "naizari.images.embedded.";

        static PixelServer()
        {
            notFoundImage = GraphicsManager.GetStringImage(75, 75, "Image\r\nNot Found", new Font("Arial", 10), Brushes.White, Brushes.SteelBlue);
            Prefix = DefaultConfiguration.GetAppSetting(typeof(PixelServer).Name + ".Prefix", "Pixerve");
            //imageSetInstances = ImplementationSingletonManager.GetApplicationProvider<ImageSetInstances>(new ImageSetInstances());
        }

        /// <summary>
        /// The value to check for when determining whether the current request is intended to be
        /// fulfilled by the PixelServer.  For example:
        /// http://www.example.cxm/appName/&lt;Prefix&gt;
        /// </summary>
        public static string Prefix
        {
            get;
            set;
        }

        public static byte[] ProxyImage(string url)
        {
            return ProxyImage(url, null);
        }

        public static byte[] ProxyImage(string url, ICredentials networkCredentials)
        {
            WebClient client = new WebClient();
            if (networkCredentials != null)
                client.Credentials = networkCredentials;

            return client.DownloadData(url);
        }

        public bool IsReusable
        {
            get
            {
                return true;
            }
        }
        public void ProcessRequest(HttpContext context)
        {
            PixelServer.ProcessPixelRequest(context);
        }

        public static bool ProcessPixelRequest(HttpContext context)
        {
            HttpRequest request = context.Request;
            HttpResponse response = context.Response;

            if (!IsPixelRequest(request))
                return false;

            ImageSetParameters parameters = new ImageSetParameters();
            string imageName = string.Empty;
            string nameSpaceQualifiedTypeName = string.Empty;
            parameters = GetParameters(request, out imageName, out nameSpaceQualifiedTypeName);
            if (string.IsNullOrEmpty(nameSpaceQualifiedTypeName))
                return ProcessResourceRequest(imageName, parameters);

            try
            {               

                //object retVal = method.Invoke(null, (object[])parameters);
                IImageSet retVal = GetImageSet(nameSpaceQualifiedTypeName, parameters);
                if (retVal == null)
                {
                    response.Clear();
                    response.StatusCode = 404;
                    response.Status = "Image not found";
                    context.ApplicationInstance.CompleteRequest();
                    return true;
                }

                IImageSet imageSet = (IImageSet)retVal;
                MemoryStream ms = new MemoryStream();
                
                // saving to a memory stream isn't necessary for jpeg but I'm leaving this in just in case I change the format to png later
                imageSet[imageName].Save(ms, ImageFormat.Png);

                response.Clear();
                //TODO: Parameterize the content type/ImageFormat
                response.ContentType = "image/Png"; // this must match the ImageFormat in the Save call above
                ms.WriteTo(response.OutputStream);
                imageSet.Dispose();
                context.ApplicationInstance.CompleteRequest();
            }
            catch(Exception ex)
            {
                LogManager.CurrentLog.AddEntry("An error occurred processing PixelServer request:\r\n Parameters: {0}", ex, new string[] { parameters.ToString() });
            }

            return true;
        }

        private static bool ProcessResourceRequest(string imageName, ImageSetParameters parameters)
        {
            HttpContext context = HttpContext.Current;
            HttpRequest request = context.Request;
            HttpResponse response = context.Response;

            Resources.LoadResources(typeof(PixelServer).Assembly);
            
            //format valid values png, gif, jpg
            WebFormats webFormat = string.IsNullOrEmpty(parameters["f"]) ? WebFormats.gif : (WebFormats)Enum.Parse(typeof(WebFormats), parameters["f"]);
            ImageFormat imageFormat = ImageFormat.Gif;
            switch (webFormat)
            {
                case WebFormats.Invalid:
                    break;
                case WebFormats.gif:
                    break;
                case WebFormats.jpg:
                    imageFormat = ImageFormat.Jpeg;
                    break;
                case WebFormats.png:
                    imageFormat = ImageFormat.Png;
                    break;
            } 

            if (Resources.Images.ContainsKey(EmbeddedPath + imageName))
            {
                Bitmap image = Resources.Images[EmbeddedPath + imageName];
                MemoryStream ms = new MemoryStream();
                image.Save(ms, imageFormat);
                response.Clear();

                response.ContentType = "image/" + webFormat.ToString();

                if (webFormat == WebFormats.jpg)
                    image.Save(response.OutputStream, imageFormat);
                else
                    ms.WriteTo(response.OutputStream);
                context.ApplicationInstance.CompleteRequest();

                return true;
            }
            return false;
        }

        private static ImageSetParameters GetParameters(HttpRequest request, out string imageName, out string namespaceQualifiedTypeName)
        {
            ImageSetParameters retVal = new ImageSetParameters();
            imageName = string.Empty;
            namespaceQualifiedTypeName = string.Empty;

            foreach (string queryKey in request.QueryString.AllKeys)
            {
                //TODO: Enumify magic strings
                if (queryKey.Equals("in")) // Image Name
                    imageName = request.QueryString[queryKey];
                else if (queryKey.Equals("t"))
                    namespaceQualifiedTypeName = request.QueryString[queryKey];
                else
                    retVal[queryKey] = HttpUtility.UrlDecode(request.QueryString[queryKey]);
            }

            return retVal;
        }

        public static IImageSet GetImageSet(string namespaceQualifiedType, ImageSetParameters parameters)
        {
            Type type = Type.GetType(namespaceQualifiedType);
            if (type == null)
                throw ExceptionHelper.CreateException<PixelServerException>("The type {0} was not found.", namespaceQualifiedType);

            if (type.GetInterface("IImageSetProvider") == null)
                throw ExceptionHelper.CreateException<PixelServerException>("The type {0} doesn't implement interface IImageSetProvider.", namespaceQualifiedType);

            ConstructorInfo ctor = type.GetConstructor(Type.EmptyTypes);
            if (ctor == null)
                throw ExceptionHelper.CreateException<PixelServerException>("The type {0} doesn't have a parameterless constructor defined.", namespaceQualifiedType);

            object invokeTarget = ctor.Invoke(null);

            MethodInfo method = type.GetMethod("GetImageSet");

            IImageSet retVal = (IImageSet)method.Invoke(invokeTarget, new object[]{ parameters });
            
            return retVal;
        }



        //private static void ValidateParameterTypes(MethodInfo method)
        //{
        //    ParameterInfo[] parameters = method.GetParameters();
        //    foreach (ParameterInfo parameter in parameters)
        //    {
        //        if (!parameter.ParameterType.Equals(typeof(string)))
        //            throw ExceptionHelper.CreateException<PixelServerException>("The specified method ({0}) takes parameters of type(s) other than string.", method.Name);
        //    }
        //}
        
        //public static Bitmap GetRoundedTopLeftCorner(string lineWidth, string radius, string htmlLineColor, string htmlBackgroundColor, string htmlFillColor)
        //{
        //    int intLineWidth;
        //    int intRadius;
        //    if (int.TryParse(lineWidth, out intLineWidth))
        //    {
        //        if (int.TryParse(radius, out intRadius))
        //        {
        //            RoundedCorners corners = GetRoundedCorners(intLineWidth, intRadius, htmlLineColor, htmlBackgroundColor, htmlFillColor);
        //            return corners.TopLeft;
        //        }
        //    }

        //    return null;
        //}
        //public static IImageSet GetRoundedCorners(int lineWidth, int radius, string htmlLineColor, string htmlBackgroundColor, string htmlFillColor)
        //{
        //    string paramKey = lineWidth + radius + htmlLineColor + htmlBackgroundColor + htmlFillColor;
        //    ImageSetInstances instances = ImplementationSingletonManager.GetApplicationProvider<ImageSetInstances>();
        //    if (instances.ContainsKey(paramKey))
        //    {
        //        return instances[paramKey];
        //    }
        //    else
        //    {
        //        RoundedCorners corners = new RoundedCorners(lineWidth, radius);
        //        corners.Line.Color = ColorTranslator.FromHtml(htmlLineColor);
        //        corners.BackgroundBrush = new SolidBrush(ColorTranslator.FromHtml(htmlBackgroundColor));
        //        corners.FillBrush = new SolidBrush(ColorTranslator.FromHtml(htmlFillColor));
        //        instances.Add(paramKey, corners);
        //        return corners;
        //    }
        //}

        public static Bitmap NotFoundImage
        {
            get
            {
                return notFoundImage;
            }
            set
            {
                notFoundImage = value;
            }
        }

        internal static bool IsPixelRequest(HttpRequest request)
        {
            string pixString = request.QueryString["pix"];
            if (!string.IsNullOrEmpty(pixString))
            {
                if (pixString.Equals("true"))
                    return true;
            }
            return false;
            //return !string.IsNullOrEmpty(request.QueryString["pix"]) && request.QueryString["pix"].Equals(bool.TrueString);
            //List<string> splitRawUrl = new List<string>(request.RawUrl.Split(new string[]{"/", "?"}, StringSplitOptions.RemoveEmptyEntries));
            //splitRawUrl.RemoveAt(0);
           
            //return splitRawUrl[0].Equals(Prefix);
        }
    }

}
