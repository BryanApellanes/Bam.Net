/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Schema.Org.DataTypes
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

        public Uri Uri
        {
            get
            {
                return new Uri(this.Value);
            }
        }
    }
}
