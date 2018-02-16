/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using Bam.Net.ServiceProxy;
using Bam.Net.Presentation;
using System.Collections.Generic;

namespace Bam.Net.Server.Renderers
{
	public interface ITemplateRenderer
	{
		void SetContentType(IResponse response);
		string CombinedCompiledLayoutTemplates { get; }
        string CombinedCompiledTemplates { get; }
        IEnumerable<ICompiledTemplate> CompiledTemplates { get; }
        string ContentRoot { get; }
		void Render(object toRender, System.IO.Stream output);
		void Render(string templateName, object toRender, System.IO.Stream output);
		void RenderLayout(LayoutModel toRender, System.IO.Stream output);
        void EnsureDefaultTemplate(Type type);
	}
}
