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
    public class Url: Text
    {
        public Url()
        {
            this.Name = "Url";
        }

        public Url(string value)
        {
            this.Value = value;
        }

        public static implicit operator Uri(Url url)
        {
            return url.Uri;
        }

        public static implicit operator Url(Uri uri)
        {
            return new Url(uri.ToString());
        }

        public static implicit operator URL(Url url)
        {
            return new URL(url.Value);
        }

        public static implicit operator Url(URL url)
        {
            return new Url(url.Value);
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
