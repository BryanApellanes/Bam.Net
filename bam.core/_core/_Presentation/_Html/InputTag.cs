using System;
using System.Collections.Generic;
using UnityEngine;

namespace Bam.Net.Presentation.Html
{
    public class InputTag: Tag
    {
        protected InputTag(string tagName, Func<Tag> content) : base(tagName, content)
        {
        }

        protected InputTag(string tagName, string content) : base(tagName, content)
        {
        }

        protected InputTag(string tagName, object attributes = null, object content = null) : base(tagName, attributes, content)
        {
        }

        protected InputTag(string tagName, Dictionary<string, object> attributes = null, object content = null) : base(tagName, attributes, content)
        {
        }

        protected InputTag(string tagName, params Func<Tag>[] contents) : base(tagName, contents)
        {
        }

        protected InputTag(string tagName, params Tag[] contents) : base(tagName, contents)
        {
        }

        public static InputTag OfType(InputTypes type, object value = null)
        {
            return OfType(type.ToString().KabobCase().ToLowerInvariant(), value);
        }
        
        public static InputTag OfType(string type, object value = null)
        {
            Args.ThrowIf(type == null, "type can't be null");
            InputTag input = new InputTag("input", new {type = type});
            if (value != null)
            {
                input.SetAttribute("value", value.ToString());
            }
            
            input.SelfClosing = true;
            return input;
        }
    }
}