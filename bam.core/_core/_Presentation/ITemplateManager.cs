/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using Bam.Net.ServiceProxy;
using Bam.Net.Presentation;
using System.Collections.Generic;

namespace Bam.Net.Presentation
{
	public interface ITemplateManager: ITemplateRenderer
	{
		void SetContentType(IResponse response);

        
        string ContentRoot { get; }
		void RenderLayout(LayoutModel toRender, System.IO.Stream output);
        void EnsureDefaultTemplate(Type type);
	}
}
