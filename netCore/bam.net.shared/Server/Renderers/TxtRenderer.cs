/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Bam.Net.Server.Renderers
{
    public class TxtRenderer: Renderer
    {
        public TxtRenderer()
            : base("text/plain", ".txt")
        {

        }

        static Func<object, string> _defaultTextifier;
        static object _defaultTextifierLock = new object();
        public static Func<object, string> DefaultTextifier
        {
            get
            {
                return _defaultTextifierLock.DoubleCheckLock(ref _defaultTextifier, () =>
                {
                    return (data) =>
                    {
                        return data.PropertiesToString();
                    };
                });
            }
            set
            {
                _defaultTextifier = value;
            }
        }

        public override void Render(object toRender, Stream output)
        {
            if (toRender != null)
            {
                string text = DefaultTextifier(toRender);

                byte[] data = Encoding.UTF8.GetBytes(text);
                output.Write(data, 0, data.Length);
            }
        }
    }
}
