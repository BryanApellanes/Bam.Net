/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Schema.Org
{
    public class URL: Text
    {
        public URL()
        {
            this.Name = "Url";
        }

        public URL(string value)
        {
            this.Value = value;
        }

        public static implicit operator Uri(URL url)
        {
            return url.Uri;
        }

        public static implicit operator URL(Uri uri)
        {
            return new URL(uri.ToString());
        }

        public Uri Uri
        {
            get
            {
                return new Uri(this.Value);
            }
        }
    }
}
