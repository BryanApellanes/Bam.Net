/*
	Copyright © Bryan Apellanes 2015  
*/
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

using System.IO;
using Bam.Net;
using Bam.Net.Data;
using Bam.Net.Incubation;
using Bam.Net.Logging;
using Yahoo.Yui.Compressor;
using System.Threading;

namespace Bam.Net.Data
{
    public partial class DaoProxyRegistration
    {
        string[] metaProperties = new string[] { "Uuid", "Cuid" };

        public DaoProxyRegistration(Type daoType)
        {
            Args.ThrowIfNull(daoType, "daoType");
            Args.ThrowIf<InvalidOperationException>(
                !daoType.IsSubclassOf(typeof(Dao)), "The specified type ({0}) must be a Dao implementation.",
                daoType.Name);

            this.ServiceProvider = new Incubator();
            this.Assembly = daoType.Assembly;
            this.ContextName = Dao.ConnectionName(daoType);
            Dao.RegisterDaoTypes(daoType, this.ServiceProvider);
        }

        public DaoProxyRegistration(Assembly assembly)
        {
            this.ServiceProvider = new Incubator();
            this.Assembly = assembly;
            Type daoType = (from type in assembly.GetTypes()
                            where type.IsSubclassOf(typeof(Dao))
                            select type).FirstOrDefault();

            Args.ThrowIfNull(daoType, "daoType");

            this.ContextName = Dao.ConnectionName(daoType);
            Dao.RegisterDaoTypes(daoType, this.ServiceProvider);
        }

        public Database Database { get; set; }

        static IDictionary<string, DaoProxyRegistration> _registrations;
        protected internal static IDictionary<string, DaoProxyRegistration> Registrations
        {
            get
            {
                if (_registrations == null)
                {
                    _registrations = new Dictionary<string, DaoProxyRegistration>();
                }

                return _registrations;
            }
        }

        /// <summary>
        /// Register all the Dao types in the current domain that belong to the 
        /// specified connectionName/schema
        /// </summary>
        /// <param name="connectionName"></param>
        /// <param name="retryCount"></param>
        public static void RegisterConnection(string connectionName, int retryCount = 5)
        {
            try
            {
                AppDomain.CurrentDomain.GetAssemblies().Each(a =>
                {
                    a.GetTypes()
                        .Where(t => t.IsSubclassOf(typeof(Dao)) && Dao.ConnectionName(t).Equals(connectionName))
                        .Each(t => Register(t));
                });
            }
            catch (Exception ex)
            {
                Log.AddEntry("Exception occurred registering connection ({0}): retryCount=({1}): {2}", LogEventType.Warning, connectionName, retryCount.ToString(), ex.Message);
                if (retryCount > 0)
                {
                    Thread.Sleep(300);
                    RegisterConnection(connectionName, --retryCount);
                }
            }
        }

        /// <summary>
        /// Register siblings of the specified Dao type T along with
        /// T itself
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static DaoProxyRegistration Register<T>() where T : Dao
        {
            return Register(typeof(T));
        }

        static object _registerLock = new object();
        public static DaoProxyRegistration Register(Type daoType)
        {
            string connectionName = Dao.ConnectionName(daoType);
            if (!Registrations.ContainsKey(connectionName))
            {
                lock (_registerLock)
                {
                    if (!Registrations.ContainsKey(connectionName))
                    {
                        DaoProxyRegistration registration = new DaoProxyRegistration(daoType);
                        Registrations.Add(connectionName, registration);
                    }
                }
            }

            return Registrations[connectionName];
        }

        public static DaoProxyRegistration[] Register(DirectoryInfo dir, string searchPattern = "*.dll")
        {
            DaoProxyRegistration[] results = new DaoProxyRegistration[] { };

            if (dir.Exists)
            {
                FileInfo[] dlls = dir.GetFiles(searchPattern.DelimitSplit(",", "|"));
                List<DaoProxyRegistration> tmp = new List<DaoProxyRegistration>();
                dlls.Each(dll =>
                {
                    Assembly current = Assembly.LoadFrom(dll.FullName);
                    tmp.Add(Register(current));
                });
                results = tmp.ToArray();
            }

            return results;//.ToArray();
        }

        public static DaoProxyRegistration Register(FileInfo daoDll)
        {
            Assembly daoAssembly = Assembly.LoadFrom(daoDll.FullName);
            return Register(daoAssembly);
        }

        public static DaoProxyRegistration Register(Assembly assembly)
        {
            Type daoType = (from type in assembly.GetTypes()
                            where type.IsSubclassOf(typeof(Dao))
                            select type).FirstOrDefault();

            string connectionName = Dao.ConnectionName(daoType);
            if (!Registrations.ContainsKey(connectionName))
            {
                lock (_registerLock)
                {
                    if (!Registrations.ContainsKey(connectionName))
                    {
                        DaoProxyRegistration registration = new DaoProxyRegistration(assembly);
                        Registrations.Add(connectionName, registration);
                    }
                }
            }

            return Registrations[connectionName];
        }

        public static StringBuilder GetScript(bool min = false)
        {
            StringBuilder result = new StringBuilder();
            foreach (string contextName in Registrations.Keys)
            {
                result.AppendLine(GetScript(contextName, min).ToString());
            }
            return result;
        }

        public static StringBuilder GetScript(string contextName, bool min = false)
        {
            Args.ThrowIf<InvalidOperationException>(
                !Registrations.ContainsKey(contextName),
                "The specified contextName ({0}) was not registered for proxying", contextName);

            StringBuilder result;
            if (min)
            {
                result = Registrations[contextName].MinProxies;
            }
            else
            {
                result = Registrations[contextName].Proxies;
            }

            return result;
        }

        public override int GetHashCode()
        {
            return this.GetHashCode(ContextName, Assembly, ServiceProvider);
        }

        public string ContextName { get; set; }
        public Assembly Assembly { get; set; }
        public Incubator ServiceProvider { get; set; }

        StringBuilder _proxiesScript;
        object _proxiesScriptLock = new object();
        public StringBuilder Proxies
        {
            get
            {
                return _proxiesScriptLock.DoubleCheckLock<StringBuilder>(ref _proxiesScript, () => BuildProxyScript());
            }
        }

        StringBuilder _minProxiesScript;
        object _minProxiesScriptLock = new object();
        public StringBuilder MinProxies
        {
            get
            {
                return _minProxiesScriptLock.DoubleCheckLock<StringBuilder>(
                    ref _minProxiesScript,
                    () =>
                    {
                        StringBuilder temp = new StringBuilder();
                        JavaScriptCompressor jsc = new JavaScriptCompressor();
                        string script = Proxies.ToString();
                        string minScript = jsc.Compress(script);
                        temp.Append(minScript);
                        return temp;
                    }
                );
            }
        }
        StringBuilder _ctorsScript;
        object _ctorsScriptLock = new object();
        public StringBuilder Ctors
        {
            get
            {
                return _ctorsScriptLock.DoubleCheckLock<StringBuilder>(ref _ctorsScript, () => BuildCtorScript());
            }
        }

        StringBuilder _minCtorsScript;
        object _minCtorsScriptLock = new object();
        public StringBuilder MinCtors
        {
            get
            {
                return _minCtorsScriptLock.DoubleCheckLock<StringBuilder>(
                    ref _minCtorsScript,
                    () =>
                    {
                        StringBuilder temp = new StringBuilder();
                        JavaScriptCompressor jsc = new JavaScriptCompressor();
                        string script = Ctors.ToString();
                        string minScript = jsc.Compress(script);
                        temp.Append(minScript);
                        return temp;
                    }
                );
            }
        }

        private StringBuilder BuildCtorScript()
        {
            return GetDaoJsCtorScript(ServiceProvider, ServiceProvider.ClassNames);
        }

        internal static StringBuilder GetDaoJsCtorScript(Incubator incubator, string[] classes)
        {
            StringBuilder ctorScript = new StringBuilder();
            StringBuilder fkProto = new StringBuilder();

            foreach (string className in classes)
            {
                Type modelType = incubator[className];
                MethodInfo modelTypeMethod = modelType.GetMethod("GetDaoType");
                if (modelTypeMethod != null)
                {
                    modelType = (Type)modelTypeMethod.Invoke(null, null);
                }

                if (modelType.HasCustomAttributeOfType<TableAttribute>())
                {
                    GetJsCtorParamsAndBody(modelType, fkProto, out StringBuilder parameters, out StringBuilder body);
                    ctorScript.AppendFormat("b.ctor.{0} = function {0}(", className);
                    // -- params 
                    ctorScript.Append(parameters.ToString());
                    // -- end params
                    ctorScript.AppendLine("){");
                    // -- body
                    ctorScript.Append(body.ToString());
                    // -- end body
                    ctorScript.AppendLine("}");
                }
            }

            ctorScript.AppendLine(fkProto.ToString());
            return ctorScript;
        }

        internal static void GetJsCtorParamsAndBody(Type type, StringBuilder fkProto, out StringBuilder paramList, out StringBuilder body)
        {
            paramList = new StringBuilder();
            body = new StringBuilder();
            PropertyInfo[] properties = (from prop in type.GetPropertiesWithAttributeOfType<ColumnAttribute>()
                                         where !prop.HasCustomAttributeOfType<KeyColumnAttribute>()
                                         select prop).ToArray();

            for (int i = 0; i < properties.Length; i++)
            {
                PropertyInfo property = properties[i];

                string propertyName = property.Name.CamelCase();
                paramList.Append(propertyName);
                if (i != properties.Length - 1)
                {
                    paramList.Append(", ");
                }

                ForeignKeyAttribute fk;
                if (property.HasCustomAttributeOfType<ForeignKeyAttribute>(out fk))
                {
                    string refProperty = string.Format("{0}Of{1}", fk.ReferencedTable, fk.Name).CamelCase();
                    body.AppendFormat("\tthis.{0} = new dao.entity('{1}', {2});\r\n", refProperty, fk.ReferencedTable, fk.Name);

                    fkProto.AppendFormat("b.ctor.{0}.prototype.{1}Collection = function(){{\r\n", fk.ReferencedTable, fk.Table.CamelCase());
                    fkProto.AppendFormat("\treturn new dao.collection(this, '{0}', '{1}', '{2}', '{3}');\r\n", fk.ReferencedTable, fk.ReferencedKey, fk.Table, fk.Name);
                    fkProto.Append("};\r\n");
                }
                else
                {
                    body.AppendFormat("\tthis.{0} = {0};\r\n", propertyName);
                }
            }

            string varName = GetVarName(type);
            body.AppendFormat("\tthis.update = function(opts){{ bam.{0}.update(this, opts); }};\r\n", varName);
            body.AppendFormat("\tthis.save = this.update;\r\n");
            body.AppendFormat("\tthis.delete = function(opts){{ bam.{0}.delete(this, opts); }};\r\n", varName);
            body.AppendFormat("\tthis.fks = function(){{ return dao.getFks('{0}');}};\r\n", Dao.TableName(type));
            body.AppendFormat("\tthis.pk = function(){{ return '{0}'; }};\r\n", Dao.GetKeyColumnName(type).ToLowerInvariant());
        }

        private static string GetVarName(Type type)
        {
            string varName = type.Name;
            if (type.HasCustomAttributeOfType(true, out ProxyAttribute proxyAttr))
            {
                varName = proxyAttr.VarName;
            }

            return varName;
        }

        private StringBuilder BuildProxyScript()
        {
            return BuildProxyScript(ServiceProvider, ContextName);
        }

        #region assemble script
        private StringBuilder BuildProxyScript(Incubator incubator, string connectionName = "")
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("$(document).ready(function(){");
            if (!string.IsNullOrEmpty(connectionName))
            {
                stringBuilder.AppendFormat("\tdao.{0} = {{}};\r\n", connectionName);
            }
            stringBuilder.AppendLine("\t(function(d, $, win){");
            stringBuilder.AppendLine("\t\t\"use strict\";");
            stringBuilder.AppendLine("\t\td.ctors = d.ctors || {};");
            stringBuilder.AppendLine("\t\td.fks = d.fks || [];");
            stringBuilder.AppendLine(GetBodyAndMeta(incubator).ToString());

            stringBuilder.AppendFormat("\t}})(dao.{0} || dao, jQuery, window || {{}});\r\n", connectionName);
            stringBuilder.AppendLine("});");
            return stringBuilder;
        }

        private StringBuilder GetBodyAndMeta(Incubator incubator)
        {
            StringBuilder script = new StringBuilder();
            StringBuilder meta = new StringBuilder();

            foreach (string className in incubator.ClassNames)
            {
                Type modelType = incubator[className];
                if (modelType.IsSubclassOf(typeof(Dao)))
                {
                    StringBuilder parameters;
                    StringBuilder body;
                    string connectionName = Dao.ConnectionName(modelType);

                    meta.AppendLine("\t\td.tables = d.tables || {};");
                    meta.AppendFormat("\t\td.tables.{0} = {{}};\r\n", className);
                    meta.AppendFormat("\t\td.tables.{0}.keyColumn = '{1}';\r\n", className, Dao.GetKeyColumnName(modelType));
                    meta.AppendFormat("\t\td.tables.{0}.cols = [];\r\n", className);
                    meta.AppendFormat("\t\td.tables.{0}.ctx = '{1}';\r\n\r\n", className, connectionName);

                    GetCtorParamsAndBody(modelType, meta, connectionName, out parameters, out body);
                    script.AppendFormat("\t\td.ctors.{0} = function {0}(", className);
                    // -- params 
                    script.Append(parameters.ToString());
                    // -- end params
                    script.AppendLine("){");

                    // writing meta data
                    PropertyInfo[] modelProps = modelType.GetProperties();
                    foreach (PropertyInfo prop in modelProps)
                    {
                        ColumnAttribute col;
                        if (prop.HasCustomAttributeOfType<ColumnAttribute>(out col))
                        {
                            string typeName = prop.PropertyType.Name;
                            if (prop.PropertyType.IsGenericType &&
                                prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                            {
                                typeName = prop.PropertyType.GetGenericArguments()[0].Name;
                            }

                            meta.AppendFormat("\t\td.tables.{0}.cols.push({{name: '{1}', type: '{2}', nullable: {3} }});\r\n", className, col.Name, typeName, col.AllowNull ? "true" : "false");
                        }

                        ForeignKeyAttribute fk;
                        if (prop.HasCustomAttributeOfType<ForeignKeyAttribute>(out fk))
                        {
                            meta.AppendFormat("\t\td.fks.push({{ pk: '{0}', pt: '{1}', fk: '{2}', ft: '{3}', nullable: {4} }});\r\n", fk.ReferencedKey, fk.ReferencedTable, fk.Name, fk.Table, fk.AllowNull ? "true" : "false");
                        }
                    }

                    // -- body
                    script.Append(body.ToString());
                    // -- end body
                    script.AppendLine("\t\t}");



                    // -- end writing meta data
                }
            }

            script.AppendLine(meta.ToString());
            return script;
        }

        private void GetCtorParamsAndBody(Type type, StringBuilder meta, string connectionName, out StringBuilder paramList, out StringBuilder body)
        {
            string ctorName = type.Name;//GetVarName(type);

            paramList = new StringBuilder();
            body = new StringBuilder();
            body.AppendFormat("\t\t\tthis.tableName = '{0}';\r\n", ctorName);
            body.AppendFormat("\t\t\tthis.ctx = this.schemaName = this.cxName = this.connectionName = '{0}';\r\n", connectionName);
            body.AppendLine("\t\t\tthis.Dao = {};");
            body.AppendLine("\t\t\tthis.collections = {};");
            body.AppendFormat("\t\t\tthis.Dao.{0} = undefined;\r\n", Dao.GetKeyColumnName(type));

            PropertyInfo[] properties = (from prop in type.GetPropertiesWithAttributeOfType<ColumnAttribute>()
                                         where !prop.HasCustomAttributeOfType<KeyColumnAttribute>() && !metaProperties.Contains(prop.Name)
                                         select prop).ToArray();

            for (int i = 0; i < properties.Length; i++)
            {
                PropertyInfo property = properties[i];

                string propertyName = property.Name;
                paramList.Append(propertyName);
                if (i != properties.Length - 1)
                {
                    paramList.Append(", ");
                }

                ForeignKeyAttribute fk;
                if (property.HasCustomAttributeOfType<ForeignKeyAttribute>(out fk))
                {
                    string refProperty = string.Format("{0}Of{1}", fk.ReferencedTable, fk.Name);
                    body.AppendFormat("\t\t\tthis.{0} = new dao.wrapper('{1}', {2});\r\n", refProperty, fk.ReferencedTable, fk.Name);
                    
                    meta.AppendFormat("\t\td.ctors.{0}.prototype.{1}Collection = function(){{\r\n", fk.ReferencedTable, fk.Table);

                    meta.AppendFormat("\t\t\tif(_.isUndefined(this.collections.{0})){{\r\n", fk.Table);                    
                    meta.AppendFormat("\t\t\t\tthis.collections.{2} =  new dao.collection(this, '{0}', '{1}', '{2}', '{3}');\r\n", fk.ReferencedTable, fk.ReferencedKey, fk.Table, fk.Name);
                    meta.Append("\t\t\t}\r\n");
                    meta.AppendFormat("\t\t\treturn this.collections.{0};\r\n", fk.Table);
                    meta.Append("\t\t};\r\n");
                }
                else
                {
                    body.AppendFormat("\t\t\tthis.Dao.{0} = {0};\r\n", propertyName);
                }
            }

            meta.AppendFormat("\t\tfor(var f in dao.wrapper.prototype){{ d.ctors.{0}.prototype[f] = dao.wrapper.prototype[f];}}\r\n", ctorName);

            body.AppendFormat("\t\t\tthis.fks = function(){{ return dao.getFks('{0}');}};\r\n", Dao.TableName(type));
        }
        #endregion
    }
}
