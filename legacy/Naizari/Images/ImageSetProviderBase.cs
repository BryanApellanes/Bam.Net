/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using Naizari.Extensions;
using Naizari.Helpers;

namespace Naizari.Images
{
    public abstract class ImageSetProviderBase: IImageSetProvider
    {
        #region IImageSetProvider Members

        public IImageSet GetImageSet(ImageSetParameters parameters)
        {
            string classTypeString = parameters["classtype"];
            if (string.IsNullOrEmpty(classTypeString))
                throw ExceptionHelper.CreateException<PixelServerException>("No classtype was specified.");

            string methodName = "Get" + StringExtensions.CamelCase(classTypeString) + this.GetType().Name;
            MethodInfo method = this.GetType().GetMethod(methodName);
            return (IImageSet)method.Invoke(this, new object[] { parameters });
        }

        #endregion
    }
}
