using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Presentation.Markdown;

namespace Bam.Net.Documentation.Markdown
{
    public class MethodReferenceDocumentation: ReferenceDocumentation
    {
        public MethodReferenceDocumentation(MethodInfo method)
        {
            TitleFormat = "{Name} Method";
            Method = method;
        }

        private MethodInfo _method;
        public MethodInfo Method
        {
            get => _method;
            set
            {
                _method = value;
                OnUpdated(new DocumentComponentEventArgs { DocumentComponent = this });
            }
        }

        public override string GetTitle()
        {
            return $"{new string('#', HeaderLevel)}{TitleFormat.NamedFormat(this)}";
        }

        public override string GetContent()
        {
            ParameterInfo[] parameters = Method.GetParameters();
            PipeTableDocumentComponent parameterTable = new PipeTableDocumentComponent("Name", "Description", "Type");
            foreach(ParameterInfo parameter in parameters)
            {
                parameterTable.AddRow(parameter.Name, DescriptionProvider.GetParameterDescription(parameter), parameter.ParameterType.Name);
            }
            return parameterTable.GetContent();
        }
    }
}
