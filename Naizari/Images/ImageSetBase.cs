/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Reflection;

namespace Naizari.Images
{
    public class ImageSetBase: IImageSet
    {
        Dictionary<string, Bitmap> imageInstances;

        public ImageSetBase()
        {
            imageInstances = new Dictionary<string, Bitmap>();
        }

        #region IImageSet Members

        public void RefreshState()
        {
            PropertyInfo[] properties = this.GetType().GetProperties();
            foreach (PropertyInfo property in properties)
            {
                ParameterInfo[] paramInfo = property.GetIndexParameters();
                if (property.CanWrite && paramInfo.Length == 0 && property.PropertyType.Equals(typeof(Bitmap)))
                {
                    this[property.Name] = (Bitmap)property.GetValue(this, null);
                }
            }
        }        

        public Bitmap this[string imageName]
        {
            get
            {
                if (this.imageInstances.ContainsKey(imageName))
                    return this.imageInstances[imageName];

                return null;
            }
            set
            {
                if (this.imageInstances.ContainsKey(imageName))
                    this.imageInstances[imageName] = value;
                else
                    this.imageInstances.Add(imageName, value);
            }
        }

        #endregion

        public void Dispose()
        {
            foreach (Bitmap image in this.imageInstances.Values)
            {
                image.Dispose();
            }
        }
    }
}
