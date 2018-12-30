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
    public class AppTemplateRenderer: CommonTemplateRenderer
    {
		public const string DefaultFileName = "default.dust";

        public AppTemplateRenderer(AppContentResponder appResponder)
            : base(appResponder.ContentResponder)
        {
            this.AppContentResponder = appResponder;
        }

        public AppContentResponder AppContentResponder
        {
            get;
            set;
        }

        protected override DirectoryInfo GetViewRoot()
        {
            DirectoryInfo dustDir = new DirectoryInfo(Path.Combine(AppContentResponder.AppRoot.Root, ViewFolderName));
            return dustDir;
        }

        public bool Overwrite
        {
            get;
            set;
        }

        object _renderLock = new object();
        public override void Render(object toRender)
        {
            Render(toRender, OutputStream);
            DirectoryInfo dustDir = GetViewRoot();
            if (!dustDir.Exists)
            {
                dustDir.Create();
            }

            lock (_renderLock)
            {
                string typeName = toRender.GetType().Name;
                string fileName = DefaultFileName;
                FileInfo file = new FileInfo(Path.Combine(dustDir.FullName, typeName, fileName));
                if (!file.Directory.Exists)
                {
                    file.Directory.Create();
                }

                if(!file.Exists || Overwrite)
                {
                    using (FileStream fs = File.Create(file.FullName, (int)OutputStream.Length))
                    {
                        OutputStream.Seek(0, SeekOrigin.Begin);
                        OutputStream.CopyTo(fs);
                    }
                }
            }
        }
    }
}
