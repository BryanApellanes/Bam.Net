/*
	Copyright © Bryan Apellanes 2015  
*/
/*
 * Copyright (c) 2007, Ted Elliott
 * Code licensed under the New BSD License:
 * http://code.google.com/p/jsonexserializer/wiki/License
 */
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Configuration;
using System.IO;
using System.Collections.Specialized;
using System.Reflection;
using Naizari.Javascript.JsonExSerialization.TypeConversion;
using Naizari.Javascript.JsonExSerialization.Collections;
using System.Globalization;

namespace Naizari.Javascript.JsonExSerialization
{
    public class XmlConfigurator
    {
        //private XmlNode configXml;
        private SerializationContext context;
        private XmlReader reader;
        private delegate void MapHandler();
        private Dictionary<string, MapHandler> handlers = new Dictionary<string, MapHandler>();
        private string sectionName;
        private int _collectionInsertPoint;

        private XmlConfigurator(XmlReader reader, SerializationContext context, string sectionName)
        {
            this.reader = reader;
            this.context = context;
            this.sectionName = sectionName;

            handlers["IsCompact"] = delegate() { context.IsCompact = reader.ReadElementContentAsBoolean(); };
            handlers["OutputTypeComment"] = delegate() { context.OutputTypeComment = reader.ReadElementContentAsBoolean(); };
            handlers["OutputTypeInformation"] = delegate() { context.OutputTypeInformation = reader.ReadElementContentAsBoolean(); };
            handlers["ReferenceWritingType"] = new MapHandler(HandleReferenceWritingType);
            handlers["TypeBindings"] = new MapHandler(HandleTypeBindings);
            handlers["TypeConverters"] = new MapHandler(HandleTypeConverters);
            handlers["CollectionHandlers"] = new MapHandler(HandleCollectionHandlers);
            handlers["IgnoreProperties"] = new MapHandler(HandleIgnoreProperties);
        }

        public static void Configure(SerializationContext context, string configSection)
        {
            XmlConfigSection section = (XmlConfigSection)ConfigurationManager.GetSection(configSection);
            if (section == null && configSection != "JsonExSerializer")
                throw new ArgumentException("Unable to find config section " + configSection);
            if (section == null)
                return;

            string xml = section.RawXml;
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml);
            Configure(context, XmlReader.Create(new StringReader(xml)), configSection);
        }

        public static void Configure(SerializationContext context, XmlReader reader, string sectionName)
        {
            new XmlConfigurator(reader, context, sectionName).Configure();
        }

        private void Configure()
        {
            try
            {
                while (reader.Read())
                {
                    if (reader.NodeType == XmlNodeType.Element)
                    {
                        if (handlers.ContainsKey(reader.Name))
                            handlers[reader.Name]();
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception("Error configuring serializer from config section " + sectionName + ". " + e.Message, e);
            }
        }

        /// <summary>
        /// Handler for TypeBinding/add tag
        /// </summary>
        /// <param name="tag">tag name</param>
        /// <param name="values">attribute values</param>
        private void AddTypeBinding(string tag, string[] values)
        {
            string alias = values[0];
            string type = values[1];

            if (string.IsNullOrEmpty(alias))
                throw new ArgumentException("Must specify 'alias' for TypeBinding add");
            if (string.IsNullOrEmpty(type))
                throw new ArgumentException("Must specify 'type' for TypeBinding add");

            context.TypeAliases.Add(Type.GetType(type, true), alias);
        }

        /// <summary>
        /// Handler for TypeBinding/remove tag
        /// </summary>
        /// <param name="tag">tag name</param>
        /// <param name="values">attribute values</param>
        private void RemoveTypeBinding(string tag, string[] values)
        {
            string alias = values[0];
            string type = values[1];
            if (!string.IsNullOrEmpty(type))
                context.TypeAliases.Remove(Type.GetType(type, true));
            else if (!string.IsNullOrEmpty(alias))
                context.TypeAliases.Remove(alias);
            else
                throw new ArgumentException("Must specify either alias or type argument to remove TypeBinding");
        }

        /// <summary>
        /// Handles the "TypeBindings" tag of the config
        /// </summary>
        private void HandleTypeBindings()
        {
            SectionHandler handler = new SectionHandler(reader, "TypeBindings");
            handler.AddMethod(new MethodMap("add", "alias type", new MethodDelegate(AddTypeBinding)));
            handler.AddMethod(new MethodMap("remove", "alias type", new MethodDelegate(RemoveTypeBinding)));
            handler.AddMethod(new MethodMap("clear", "", delegate(string t, string[] v) { context.TypeAliases.Clear(); }));
            handler.Process();            
        }

        /// <summary>
        /// Handles the TypeConverters/add tag of the config file
        /// </summary>
        /// <param name="tagName">name of the tag, should be "add"</param>
        /// <param name="values">attribute values in the order: type, property, converter</param>
        private void AddTypeConverter(string tagName, string[] values)
        {
            string type = values[0];
            string property = values[1];
            string converter = values[2];

            if (string.IsNullOrEmpty(type))
                throw new ArgumentException("Must specify 'type' for TypeConverters add");
            if (string.IsNullOrEmpty(converter))
                throw new ArgumentException("Must specify 'converter' for TypeConverters add");

            // load the specified types
            Type objectType = Type.GetType(type, true);
            Type converterType = Type.GetType(converter, true);

            // check for the property element, if it exists, the converter is for a property on the type
            IJsonTypeConverter converterObj = (IJsonTypeConverter)Activator.CreateInstance(converterType);
            if (!string.IsNullOrEmpty(property))
                context.RegisterTypeConverter(objectType, property, converterObj);
            else
                context.RegisterTypeConverter(objectType, converterObj);

        }
        /// <summary>
        /// Handles the configuration of Type Converters for the TypeConverters node
        /// </summary>
        private void HandleTypeConverters()
        {
            SectionHandler handler = new SectionHandler(reader, "TypeConverters");
            handler.AddMethod(new MethodMap("add", "type property converter", new MethodDelegate(AddTypeConverter)));
            handler.Process();
        }

        private void AddCollectionHandler(string tagName, string[] values)
        {
            string type = values[0];

            if (string.IsNullOrEmpty(type))
                throw new ArgumentException("Must specify 'type' for CollectionHandlers add");

            // load the specified types
            Type handlerType = Type.GetType(type, true);

            CollectionHandler collHandler = (CollectionHandler)Activator.CreateInstance(handlerType);
            context.CollectionHandlers.Insert(_collectionInsertPoint++, collHandler);
        }

        /// <summary>
        /// Handles the configuration of Type Converters factories for the TypeConverterFactories node
        /// </summary>
        private void HandleCollectionHandlers()
        {
            SectionHandler handler = new SectionHandler(reader, "CollectionHandlers");
            handler.AddMethod(new MethodMap("add", "type", new MethodDelegate(AddCollectionHandler)));
            handler.Process();
        }

        private void AddIgnoreProperty(string tagName, string[] values)
        {
            string type = values[0];
            string property = values[1];

            if (string.IsNullOrEmpty(type))
                throw new ArgumentException("Must specify 'type' for IgnoreProperties add");
            if (string.IsNullOrEmpty(property))
                throw new ArgumentException("Must specify 'property' for IgnoreProperties add");


            Type classType = Type.GetType(type, true);
            context.IgnoreProperty(classType, property);
        }

        private void HandleIgnoreProperties()
        {
            SectionHandler handler = new SectionHandler(reader, "IgnoreProperties");
            handler.AddMethod(new MethodMap("add", "type property", new MethodDelegate(AddIgnoreProperty)));
            handler.Process();
        }

        private void HandleReferenceWritingType()
        {
            string value = reader.ReadElementContentAsString();
            context.ReferenceWritingType = (SerializationContext.ReferenceOption) Enum.Parse(context.ReferenceWritingType.GetType(), value);
        }

        private delegate void MethodDelegate(string tagName, params string[] values);

        /// <summary>
        /// encapsulates a method delegate for processing a tag.  The
        /// parameters list is a list of valid attributes for the tag.  They will be
        /// passed in the order listed to the delegate.
        /// </summary>
        private class MethodMap
        {
            private string _name;
            private NameValueCollection _parameters;
            private MethodDelegate _method;

            /// <summary>
            /// Creates a method map for the tag
            /// </summary>
            /// <param name="name">the name of the method</param>
            /// <param name="parameters">space separated list of attributes for the tag</param>
            /// <param name="method">the delegate</param>
            public MethodMap(string name, string parameters, MethodDelegate method) {
                this._name = name;
                _parameters = new NameValueCollection();
                int i = 0;
                foreach (string s in parameters.Split(' ')) {
                    _parameters.Add(s, i.ToString());
                    i++;
                }

                this._method = method;
            }

            /// <summary>
            /// The name of the tag
            /// </summary>
            public string Name
            {
                get { return this._name; }
            }

            /// <summary>
            /// The valid attributes for the tag
            /// </summary>
            public NameValueCollection Parameters
            {
                get { return this._parameters; }
            }

            /// <summary>
            /// The delegate to call when the tag is encountered
            /// </summary>
            public MethodDelegate Method
            {
                get { return this._method; }
            }
        }

        /// <summary>
        /// Handles a given section of the xml file
        /// </summary>
        private class SectionHandler
        {
            private XmlReader reader;
            private Dictionary<string, MethodMap> tagMap = new Dictionary<string, MethodMap>();
            private string outerTag;

            public SectionHandler(XmlReader reader, string outerTag)
            {
                this.outerTag = outerTag;
                this.reader = reader;
            }

            public void AddMethod(MethodMap method) {
                tagMap.Add(method.Name, method);
            }

            public void Process()
            {
                while (reader.Read())
                {
                    if (reader.NodeType == XmlNodeType.EndElement && reader.Name == outerTag)
                        break;

                    if (reader.IsStartElement())
                    {
                        if (!tagMap.ContainsKey(reader.Name))
                            throw new ArgumentException("Unrecognized element " + reader.Name + " within " + outerTag + " tag");

                        string tag = reader.Name;

                        MethodMap method = tagMap[tag];
                        string[] values = new string[method.Parameters.Count];

                        while (reader.MoveToNextAttribute())
                        {
                            if (method.Parameters.Get(reader.Name) == null)
                                throw new ArgumentException("Unrecognized attribute " + reader.Name + " to " + tag + " tag within " + outerTag + " tag");

                            values[int.Parse(method.Parameters.Get(reader.Name))] = reader.ReadContentAsString();
                        }
                        method.Method(tag, values);
                    }
                }
            }
        }       
    }

    public class XmlConfigSection : ConfigurationSection
    {
        private string xml;

        protected override void DeserializeElement(XmlReader reader, bool serializeCollectionKey)
        {
            xml = reader.ReadOuterXml();
        }

        
        public string RawXml
        {
            get { return xml; }
        }
    }
}
