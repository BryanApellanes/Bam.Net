using System;
using System.Reflection;
using static Bam.Net.Presentation.Html.Tags;

namespace Bam.Net.Presentation.Html
{
    public class NumberInputProvider: InputProvider
    {
        public override Tag CreateInput(PropertyInfo propertyInfo, object data = null)
        {
            if (propertyInfo.HasCustomAttributeOfType<TemplateAttribute>(out TemplateAttribute templateAttribute))
            {
                return CreateFromTemplate(templateAttribute, data);
            }

            return InputTag.OfType(InputTypes.Number, data);
        }
    }
}