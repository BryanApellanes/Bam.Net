/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Bam.Net;
using Bam.Net.Logging;
using System.IO;
using Bam.Net.ServiceProxy;

namespace Bam.Net.Documentation
{
    public class DocInfo
    {
        internal DocInfo()
        { }

        public DocInfo(Type type)
        {
            this.DeclaringType = type;
            this.MemberType = ClassMemberType.Type;
            this.MemberName = "{0}.{1}"._Format(type.Namespace, type.Name);
            this.From = DocInfoFrom.Reflection;

            if (type.HasCustomAttributeOfType<DocInfoAttribute>(out DocInfoAttribute attribute))
            {
                this.Summary = attribute.Summary;
            }           
        }

        public DocInfo(PropertyInfo property)
        {
            Type type = property.DeclaringType;
            this.DeclaringType = type;
            this.MemberType = ClassMemberType.Property;
            this.MemberName = "{0}.{1}.{2}"._Format(type.Namespace, type.Name, property.Name);
            this.From = DocInfoFrom.Reflection;

            if (property.HasCustomAttributeOfType(out DocInfoAttribute attribute))
            {
                this.Summary = attribute.Summary;                
            }
        }

        public DocInfo(MethodInfo method)
        {
            Type type = method.DeclaringType;
            this.DeclaringType = type;
            this.MemberType = ClassMemberType.Method;
            this.Returns = method.ReturnType.FullName;
            this.MemberName = "{0}.{1}.{2}"._Format(type.Namespace, type.Name, method.Name);
            this.From = DocInfoFrom.Reflection;

            if (method.HasCustomAttributeOfType(out DocInfoAttribute attribute))
            {
                this.Summary = attribute.Summary;
                SetMethodInfo(attribute, method);
            }
        }

        public DocInfoFrom From
        {
            get;
            set;
        }

        /// <summary>
        /// The Type that the member was declared on
        /// </summary>
        public Type DeclaringType { get; set; }

        string _declaringTypeName;
        /// <summary>
        /// The full name name of the DeclaringType
        /// </summary>
        public string DeclaringTypeName
        {
            get
            {
                if (DeclaringType != null)
                {
                    _declaringTypeName = DeclaringType.FullName;
                }
                return _declaringTypeName;
            }
            set
            {
                _declaringTypeName = value;
            }
        }
        public string AssemblyName { get; set; }
        public string Summary { get; set; }
        
        public string Remarks { get; set; }
        public string Example { get; set; }
        public string Value { get; set; }
        public string Exception { get; set; }

        public string MemberName { get; set; }
        public ClassMemberType MemberType { get; set; }
        public string Returns { get; set; }

        public bool HasExtraItems
        {
            get
            {
                return _items != null && _items.Length > 0;
            }
        }

        object[] _items = new object[] { };
        public object[] Items(object[] setTo = null)
        {
            if (setTo != null)
            {
                _items = setTo;
            }

            return _items;
        }

        public object Item(int index)
        {
            object result = null;

            if (_items != null && index < _items.Length)
            {
                result = _items[index];
            }

            return result;
        }

        public void AddItem(object value)
        {
            object[] newItems = new object[_items.Length + 1];
            _items.CopyTo(newItems, 0);
            _items = newItems;
        }

        public void AddItems(object[] values)
        {
            object[] newItems = new object[_items.Length + values.Length];
            _items.CopyTo(newItems, 0);
            values.CopyTo(newItems, _items.Length);
            _items = newItems;
        }

        ParamInfo[] _paramInfos = new ParamInfo[] { };
        public ParamInfo[] ParamInfos
        {
            get
            {
                return _paramInfos;
            }
            set
            {
                _paramInfos = value;
            }
        }

        public void AddParamInfo(ParamInfo paramInfo)
        {
            lock (_paramInfos)
            {
                int capacity = 1;
                if (_paramInfos != null)
                {
                    capacity = _paramInfos.Length + 1;
                }
                ParamInfo[] tmp = new ParamInfo[capacity];
                if (_paramInfos != null)
                {
                    _paramInfos.CopyTo(tmp, 0);
                }
                tmp[capacity - 1] = paramInfo;
                _paramInfos = tmp;
            }
        }

        TypeParamInfo[] _typeParamInfos = new TypeParamInfo[] { };
        public TypeParamInfo[] TypeParamInfos
        {
            get
            {
                return _typeParamInfos;
            }
            set
            {
                _paramInfos = value;
            }
        }

        public void AddTypeParamInfo(TypeParamInfo typeParamInfo)
        {
            lock (_typeParamInfos)
            {
                int capacity = 1;
                if (_typeParamInfos != null)
                {
                    capacity = _typeParamInfos.Length + 1;
                }
                TypeParamInfo[] tmp = new TypeParamInfo[capacity];
                if (_typeParamInfos != null)
                {
                    _typeParamInfos.CopyTo(tmp, 0);
                }
                tmp[capacity - 1] = typeParamInfo;
                _typeParamInfos = tmp;
            }
        }

        CodeInfo[] _codeInfos = new CodeInfo[] { };
        public CodeInfo[] CodeInfos
        {
            get
            {
                return _codeInfos;
            }
            set
            {
                _codeInfos = value;
            }
        }

        public void AddCodeInfo(CodeInfo codeInfo)
        {
            lock (_codeInfos)
            {
                int capacity = 1;
                if (_codeInfos != null)
                {
                    capacity = _codeInfos.Length + 1;
                }
                CodeInfo[] tmp = new CodeInfo[capacity];
                if (_codeInfos != null)
                {
                    _codeInfos.CopyTo(tmp, 0);
                }
                tmp[capacity - 1] = codeInfo;
                _codeInfos = tmp;
            }
        }

        PermissionInfo[] _permissionInfos = new PermissionInfo[] { };
        public PermissionInfo[] PermissionInfos
        {
            get
            {
                return _permissionInfos;
            }
            set
            {
                _permissionInfos = value;
            }
        }

        public void AddPermissionInfo(PermissionInfo permissionInfo)
        {
            lock (_permissionInfos)
            {
                int capacity = 1;
                if (_permissionInfos != null)
                {
                    capacity = _permissionInfos.Length + 1;
                }
                PermissionInfo[] tmp = new PermissionInfo[capacity];
                if (_permissionInfos != null)
                {
                    _permissionInfos.CopyTo(tmp, 0);
                }
                tmp[capacity - 1] = permissionInfo;
                _permissionInfos = tmp;
            }
        }

        SeeAlsoInfo[] _seealsoInfos = new SeeAlsoInfo[] { };
        public SeeAlsoInfo[] SeeAlsoInfos
        {
            get
            {
                return _seealsoInfos;
            }
            set
            {
                _seealsoInfos = value;
            }
        }

        public void AddSeeAlsoInfo(SeeAlsoInfo seealsoInfo)
        {
            lock (_seealsoInfos)
            {
                int capacity = 1;
                if (_seealsoInfos != null)
                {
                    capacity = _seealsoInfos.Length + 1;
                }
                SeeAlsoInfo[] tmp = new SeeAlsoInfo[capacity];
                if (_seealsoInfos != null)
                {
                    _seealsoInfos.CopyTo(tmp, 0);
                }
                tmp[capacity - 1] = seealsoInfo;
                _seealsoInfos = tmp;
            }
        }

        internal static Dictionary<string, List<DocInfo>> FromXmlFilesIn(string directoryPath)
        {
            return FromXmlFilesIn(new DirectoryInfo(directoryPath));
        }

        internal static Dictionary<string, List<DocInfo>> FromXmlFilesIn(DirectoryInfo dir)
        {
            Args.ThrowIfNull(dir, "dir");

            Dictionary<string, List<DocInfo>> allResults = new Dictionary<string, List<DocInfo>>();

            if (dir.Exists)
            {
                FileInfo[] files = dir.GetFiles("*.xml");
                files.Each(f =>
                {
                    try
                    {
                        Dictionary<string, List<DocInfo>> tmp = FromXmlFile(f);
                        tmp.Keys.Each(s =>
                        {
                            if (!allResults.ContainsKey(s))
                            {
                                allResults.Add(s, new List<DocInfo>());
                            }

                            allResults[s].AddRange(tmp[s]);
                        });
                    }
                    catch (Exception ex)
                    {
                        Log.AddEntry("A non fatal exception occurred trying to read documentation xml file: {0}\r\n{1}",
                            LogEventType.Warning, f.FullName, ex.Message);
                    }
                });
            }
            else
            {
                Log.AddEntry("Requested DocInfo Dictionary for non-existent directory: {0}", LogEventType.Warning, dir.FullName);
            }

            return allResults;
        }

        internal static bool TryFromXmlFile(string filePath, out  Dictionary<string, List<DocInfo>> docInfos)
        {
            return TryFromXmlFile(new FileInfo(filePath), out docInfos);
        }

        internal static bool TryFromXmlFile(FileInfo file, out Dictionary<string, List<DocInfo>> docInfos)
        {
            docInfos = null;
            bool result = true;
            try
            {
                docInfos = FromXmlFile(file);
            }
            catch (Exception ex)
            {
                Log.AddEntry("An error occurred getting DocInfo for file {0}: {1}", ex, file.FullName, ex.Message);
                result = false;
            }

            return result;
        }

        internal static Dictionary<string, List<DocInfo>> FromXmlFile(string filePath)
        {
            return FromXmlFile(new FileInfo(filePath));
        }

        internal static Dictionary<string, List<DocInfo>> FromXmlFile(FileInfo file)
        {
            Args.ThrowIfNull(file, "file");

            Dictionary<string, List<DocInfo>> tempResults = new Dictionary<string, List<DocInfo>>();

            if (file.Exists)
            {
                doc doc = file.FromXmlFile<doc>();
                string assemblyName = doc.assembly.name;
                if (doc.members != null && doc.members.Items != null)
                {
                    doc.members.Items.Each(mem =>
                    {
                        DocInfo info = new DocInfo();
                        info.AssemblyName = assemblyName;
                        string memberName;
                        info.MemberType = GetMemberType(mem.name, out memberName);
                        info.MemberName = memberName;
                        info.From = DocInfoFrom.Xml;
                        
                        #region ugly
                       
                        if (mem.Items != null)
                        {
                            mem.Items.Each(memberItem =>
                            {
                                if (!info.TrySetSummary(memberItem))
                                {
                                    if (!info.TrySetRemarks(memberItem))
                                    {
                                        if (!info.TrySetExample(memberItem))
                                        {
                                            if (!info.TrySetValue(memberItem))
                                            {
                                                if (!info.TrySetReturns(memberItem))
                                                {
                                                    if (!info.TrySetParam(memberItem))
                                                    {
                                                        if (!info.TrySetTypeParam(memberItem))
                                                        {
                                                            if (!info.TrySetCode(memberItem))
                                                            {
                                                                if (!info.TrySetException(memberItem))
                                                                {
                                                                    if (!info.TrySetPermission(memberItem))
                                                                    {
                                                                        info.TrySetSeeAlso(memberItem);
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            });
                        }
                        #endregion

                        if (!tempResults.ContainsKey(memberName))
                        {
                            tempResults.Add(memberName, new List<DocInfo>());
                        }

                        tempResults[memberName].Add(info);
                    });
                }
            }
            else
            {
                Log.AddEntry("Requested DocInfo Dictionary for non-existent file: {0}", file.FullName);
            }

            return tempResults;
        }

        internal bool TrySetSummary(object item)
        {
            bool result = false;
            summary summary = item as summary;
            if (summary != null)
            {
                result = true;
                StringBuilder txt = new StringBuilder();
                summary.Text.Each(s =>
                {
                    txt.Append(s);
                });
                this.Summary = txt.ToString();

                if (summary.Items != null)
                {
                    AddItems(summary.Items);
                }

                if (summary.Items1 != null)
                {
                    AddItems(summary.Items1);
                }
            }

            return result;
        }

        internal bool TrySetRemarks(object item)
        {
            bool result = false;
            remarks remarks = item as remarks;
            if (remarks != null)
            {
                result = true;
                StringBuilder txt = new StringBuilder();
                remarks.Text.Each(s =>
                {
                    txt.Append(s);
                });
                this.Remarks = txt.ToString();

                if (remarks.Items != null)
                {
                    AddItems(remarks.Items);
                }
                if (remarks.Items1 != null)
                {
                    AddItems(remarks.Items1);
                }
            }

            return result;
        }

        internal bool TrySetExample(object item)
        {
            bool result = false;
            example example = item as example;
            if (example != null)
            {
                result = true;
                StringBuilder txt = new StringBuilder();
                example.Text.Each(s =>
                {
                    txt.Append(s);
                });
                this.Example = txt.ToString();

                if (example.Items != null)
                {
                    AddItems(example.Items);
                }                
            }

            return result;
        }

        internal bool TrySetValue(object item)
        {
            bool result = false;
            value value = item as value;
            if (value != null)
            {
                result = true;
                StringBuilder txt = new StringBuilder();
                value.Text.Each(s =>
                {
                    txt.Append(s);
                });
                this.Value = txt.ToString();

                if (value.Items != null)
                {
                    AddItems(value.Items);
                }
            }

            return result;
        }

        internal bool TrySetReturns(object item)
        {
            bool result = false;
            returns returns = item as returns;
            if (returns != null)
            {
                result = true;
                StringBuilder txt = new StringBuilder();
                returns.Text.Each(s =>
                {
                    txt.Append(s);
                });
                this.Returns = txt.ToString();

                if (returns.Items != null)
                {
                    AddItems(returns.Items);
                }
            }

            return result;
        }

        internal bool TrySetParam(object item)
        {
            bool result = false;
            param param = item as param;
            if (param != null)
            {
                result = true;
                
                StringBuilder txt = new StringBuilder();
                param.Text.Each(s =>
                {
                    txt.Append(s);
                });

                ParamInfo info = new ParamInfo();
                info.Description = txt.ToString();
                info.Name = param.name;
                AddParamInfo(info);
            }

            return result;
        }

        internal bool TrySetTypeParam(object item)
        {
            bool result = false;
            typeparam param = item as typeparam;
            if (param != null)
            {
                result = true;

                StringBuilder txt = new StringBuilder();
                param.Text.Each(s =>
                {
                    txt.Append(s);
                });
                
                TypeParamInfo info = new TypeParamInfo();
                info.Description = txt.ToString();
                info.Name = param.name;
                AddTypeParamInfo(info);
            }

            return result;
        }

        internal bool TrySetCode(object item)
        {
            bool result = false;
            code code = item as code;
            if (code != null)
            {
                result = true;
                StringBuilder txt = new StringBuilder();
                code.Text.Each(s =>
                {
                    txt.Append(s);
                });

                CodeInfo info = new CodeInfo();
                info.Code = txt.ToString();
                info.Language = code.language;
            }

            return result;
        }

        internal bool TrySetException(object item)
        {
            bool result = false;
            exception exception = item as exception;
            if (exception != null)
            {
                result = true;
                StringBuilder txt = new StringBuilder();
                exception.Text.Each(s =>
                {
                    txt.Append(s);
                });
                this.Exception = txt.ToString();

                if (exception.Items != null)
                {
                    AddItems(exception.Items);
                }
            }

            return result;
        }

        internal bool TrySetPermission(object item)
        {
            bool result = false;
            permission permission = item as permission;
            if (permission != null)
            {
                result = true;
                StringBuilder txt = new StringBuilder();
                permission.Text.Each(s =>
                {
                    txt.Append(s);
                });

                PermissionInfo info = new PermissionInfo();
                info.Text = txt.ToString();
                info.Cref = permission.cref;
                AddPermissionInfo(info);
            }

            return result;
        }

        internal bool TrySetSeeAlso(object item)
        {
            bool result = false;
            seealso seealso = item as seealso;
            if (seealso != null)
            {
                result = true;
                StringBuilder txt = new StringBuilder();
                seealso.Text.Each(s =>
                {
                    txt.Append(s);
                });

                SeeAlsoInfo info = new SeeAlsoInfo();
                info.Text = txt.ToString();
                info.Cref = seealso.cref;
                AddSeeAlsoInfo(info);
            }

            return result;
        }

        internal ClassMemberType GetMemberType(member m, out string memberName)
        {
            string tmp = m.name;
            return GetMemberType(tmp, out memberName);
        }

        internal static ClassMemberType GetMemberType(string tmp, out string memberName)
        {
            string type = tmp.Substring(0, 1);
            memberName = tmp.Substring(2, tmp.Length - 2);
            DocInfoMemberType memberType;
            Enum.TryParse<DocInfoMemberType>(type, out memberType);

            return (ClassMemberType)((int)memberType);
        }

        internal static Dictionary<string, List<DocInfo>> FromDocAttributes(Type type)
        {
            Dictionary<string, List<DocInfo>> documentation = new Dictionary<string, List<DocInfo>>();
            List<DocInfo> docInfos = new List<DocInfo>();
            docInfos.Add(new DocInfo(type));
            PropertyInfo[] properties = type.GetProperties();
            properties.Each(p =>
            {
                docInfos.Add(new DocInfo(p));
            });
            MethodInfo[] methods = type.GetMethods();
            methods.Each(m =>
            {
                if (m.WillProxy())
                {
                    docInfos.Add(new DocInfo(m));
                }
            });

            documentation.Add(type.AssemblyQualifiedName, docInfos);
            return documentation;
        }

        private void SetMethodInfo(DocInfoAttribute attr, MethodInfo method)
        {
            ParameterInfo[] actualInfos = method.GetParameters();
            if (actualInfos.Length != attr.ParameterDescriptions.Length)
            {
                LogMissingParameterDoc(method, actualInfos);
            }

            ParamInfo[] providedInfos = new ParamInfo[attr.ParameterDescriptions.Length];
            attr.ParameterDescriptions.Each((desc, i) =>
            {
                string name = string.Empty;
                string type = string.Empty;
                if (i < actualInfos.Length)
                {
                    ParameterInfo info = actualInfos[i];
                    name = info.Name;
                    type = info.ParameterType.FullName;
                }

                providedInfos[i] = new ParamInfo { Description = desc, Name = name };
            });

            this.MemberName = "{0}.{1}.{2}"._Format(DeclaringType.Namespace, DeclaringType.Name, method.Name);            
            this.ParamInfos = providedInfos;
        }

        private static void LogMissingParameterDoc(MethodInfo method, ParameterInfo[] actualInfos)
        {
            Type declaringType = method.DeclaringType;
            StringBuilder paramInfoString = new StringBuilder();
            actualInfos.Each((pi, i) =>
            {
                paramInfoString.AppendFormat("{0} {1}", pi.ParameterType.Name, pi.Name);
                if (i > 0)
                {
                    paramInfoString.Append(", ");
                }
            });
            Log.AddEntry("Not all parameters are documented on {0}.{1}.{2}({3})",
                LogEventType.Warning,
                declaringType.Namespace,
                declaringType.Name,
                method.Name,
                paramInfoString.ToString());
        }

        /// <summary>
        /// Infers documentation by reading any Doc attributes found in the 
        /// specified assembly as well as reading the xml documentation file 
        /// if it exists
        /// </summary>
        /// <param name="assembly"></param>
        /// <param name="precedence"></param>
        /// <returns></returns>
        public static Dictionary<string, List<DocInfo>> Infer(Assembly assembly, DocumentationPrecedence precedence = DocumentationPrecedence.Xml)
        {
            Args.ThrowIfNull(assembly, "assembly");

            Dictionary<string, List<DocInfo>> results = new Dictionary<string, List<DocInfo>>();

            Infer(results, assembly, precedence);

            return results;
        }

        public static void Infer(Dictionary<string, List<DocInfo>> results, Assembly assembly, DocumentationPrecedence precedence = DocumentationPrecedence.Xml)
        {
            FileInfo assemblyFileInfo = new FileInfo(assembly.Location);
            assembly.GetTypes().Each(type =>
            {
                AddDocInfos(results, type);
            });

            string name = Path.GetFileNameWithoutExtension(assemblyFileInfo.FullName);
            string xmlFilePath = Path.Combine(assemblyFileInfo.Directory.FullName, $"{name}.xml");
            FileInfo xmlFile = new FileInfo(xmlFilePath);

            if (xmlFile.Exists)
            {
                AddDocInfos(results, xmlFile, precedence);
            }
        }

        public static void AddDocInfos(Dictionary<string, List<DocInfo>> results, FileInfo xmlFile, DocumentationPrecedence precedence = DocumentationPrecedence.Xml)
        {
            if (TryFromXmlFile(xmlFile, out Dictionary<string, List<DocInfo>> xmlResults))
            {
                xmlResults.Keys.Each(typeName =>
                {
                    if ((results.ContainsKey(typeName) && precedence == DocumentationPrecedence.Xml) ||
                        !results.ContainsKey(typeName))
                    {
                        if (!results.ContainsKey(typeName))
                        {
                            results.Add(typeName, new List<DocInfo>());
                        }

                        results[typeName].AddRange(xmlResults[typeName]);
                    }
                });
            }
        }

        public static void AddDocInfos(Dictionary<string, List<DocInfo>> results, Type type)
        {
            Dictionary<string, List<DocInfo>> tempResults = FromDocAttributes(type);
            tempResults.Keys.Each(typeName =>
            {
                if (!results.ContainsKey(typeName))
                {
                    results.Add(typeName, new List<DocInfo>());
                }

                results[typeName].AddRange(tempResults[typeName]);
            });
        }
    }
}
