using System.Reflection;
using Bam.Net.Presentation.Handlebars;
using Bam.Net.Services;

namespace Bam.Net.Presentation.Html
{
    public abstract class InputProvider
    {
        public abstract Tag CreateInput(PropertyInfo propertyInfo, object data = null);

        public static Tag CreateFromTemplate(TemplateAttribute templateAttribute, object data = null)
        {
            HandlebarsDirectory directory = new HandlebarsDirectory(templateAttribute.DirectoryPath ?? "./Handlebars");
            Tag result = Tags.Span();
            result.Content = directory.Render(templateAttribute.Name, data);
            return result;
        }
        
        public static Tag CreateInput(InputTypes type, ParameterInfo parameterInfo, object value = null)
        {
            if (parameterInfo.HasCustomAttributeOfType<TemplateAttribute>(out TemplateAttribute attribute))
            {
                InputProvider.CreateFromTemplate(attribute, value);
            }

            return InputTag.OfType(type, value)
                .SetAttribute("name", parameterInfo.Name);
        }
        
        public static Tag CreateInput(InputTypes type, PropertyInfo propertyInfo, object value = null)
        {
            if (propertyInfo.HasCustomAttributeOfType<TemplateAttribute>(out TemplateAttribute attribute))
            {
                InputProvider.CreateFromTemplate(attribute, value);
            }
            return InputTag.OfType(type, value)
                .SetAttribute("name", propertyInfo.Name);
        }

        public static Tag CreateInput(InputTypes type, string name, object value = null)
        {
            return InputTag.OfType(type, value)
                .SetAttribute("name", name);
        }
        
        public static Tag CreateInput(string type, string name, object value = null)
        {
            return InputTag.OfType(type, value)
                .SetAttribute("name", name);
        }
    }
}