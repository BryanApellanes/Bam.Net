/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.Configuration;
using System.Reflection;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Data;

namespace Bam.Net.Configuration
{
    /// <summary>
    /// A simple wrapper around the ConfigurationManager that simplifies
    /// configuration information access and enables programmatic
    /// manipulation of configuration values
    /// </summary>
    public static class DefaultConfiguration
    {
        public const string DefaultApplicationName = "UNKNOWN-APPLICATION";
        public const string DefaultProcessName = "UNKNOWN-PROCESS";

        static NameValueCollection _appSettings = ConfigurationManager.AppSettings;
        static ConnectionStringSettingsCollection _connectionStrings = ConfigurationManager.ConnectionStrings;

        /// <summary>
        /// Gets the application settings.
        /// </summary>
        /// <returns></returns>
        public static NameValueCollection GetAppSettings()
        {
            return _appSettings;
        }

        /// <summary>
        /// Sets the NameValueCollection that configuration settings are retrieved from when 
        /// using the DefaultConfiguration static class.  The default is ConfigurationManager.AppSettings;
        /// </summary>
        /// <param name="appSettings"></param>
        public static void SetAppSettings(Dictionary<string, string> appSettings)
        {
            NameValueCollection actual = new NameValueCollection();
            foreach (string key in appSettings.Keys)
            {
                actual.Add(key, appSettings[key]);
            }
            SetAppSettings(actual);
        }

        /// <summary>
        /// Sets the NameValueCollection that configuration settings are retrieved from when 
        /// using the DefaultConfiguration static class.  The default is ConfigurationManager.AppSettings;
        /// </summary>
        /// <param name="appSettings"></param>
        public static void SetAppSettings(NameValueCollection appSettings)
        {
            if (_appSettings != appSettings)
            {
                lock (_appSettings)
                {
                    _appSettings = appSettings;
                }
            }
        }
       
        /// <summary>
        /// Reset the inner appSettings to those provided by ConfigurationManager.AppSettings
        /// </summary>
        public static void SetAppSettings()
        {
            _appSettings = ConfigurationManager.AppSettings;
        }

        public static void SetConnectionStrings(params ConnectionStringSettings[] connectionStrings)
        {
            ConnectionStringSettingsCollection actual = new ConnectionStringSettingsCollection();
            foreach (ConnectionStringSettings setting in connectionStrings)
            {
                actual.Add(setting);
            }

            SetConnectionStrings(actual);
        }
        /// <summary>
        /// Sets the ConnectionStringSettingsCollection that connection strings are retrieved from when 
        /// using the DefaultConfiguration static class. The default is ConfigurationManager.ConnectionStrings.
        /// </summary>
        /// <param name="connectionStrings"></param>
        public static void SetConnectionStrings(ConnectionStringSettingsCollection connectionStrings)
        {
            if (_connectionStrings != connectionStrings)
            {
                lock (_connectionStrings)
                {
                    _connectionStrings = connectionStrings;
                }
            }
        }

        /// <summary>
        /// Sets the connection strings back to those specified in the default configuration file.
        /// </summary>
        public static void SetConnectionStrings()
        {
            _connectionStrings = ConfigurationManager.ConnectionStrings;
        }

        /// <summary>
        /// Gets the connection strings.
        /// </summary>
        /// <returns></returns>
        public static ConnectionStringSettingsCollection GetConnectionStrings()
        {
            return _connectionStrings;
        }

        /// <summary>
        /// Get the property value for the specified type. If the value is not found 
        /// an empty string will be returned.
        /// </summary>
        /// <param name="type">The Type to retrieve the property for</param>
        /// <param name="property">The property to retrieve a value for</param>
        public static string GetAppSetting(Type type, string property)
        {
            return GetAppSetting(string.Format("{0}.{1}", type.Name, property));
        }

        /// <summary>
        /// Gets the connection string.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="property">The property.</param>
        /// <returns></returns>
        public static string GetConnectionString(Type type, string property)
        {
            return GetConnectionString(string.Format("{0}.{1}", type.Name, property));
        }

        /// <summary>
        /// Get the value of the specified key. If the value is not found an empty string will be returned.
        /// </summary>
        /// <param name="key">The key name to return the value for.</param>
        public static string GetAppSetting(string key)
        {
            return GetAppSetting(key, string.Empty);
        }

        /// <summary>
        /// Get the value of the specified key. If the value is not found 
        /// the specified default value will be returned.
        /// </summary>
        /// <param name="key">The key name to return the value for.</param>
        /// <param name="defaultValue">The value that will be returned if the 
        /// specified key is not found.</param>
        public static string GetAppSetting(string key, string defaultValue)
        {
            string returnValue = _appSettings[key];
            if (string.IsNullOrEmpty(returnValue))
            {
                returnValue = defaultValue;
            }
            if (string.IsNullOrEmpty(returnValue))
            {
                returnValue = ConfigurationResolver.Current[key];
            }
            return returnValue;
        }

        /// <summary>
        /// Get the value of the specified key.  If the value is not found
        /// and throwIfNullOrEmpty is true the specified messageFormat will 
        /// be used as the message of an InvalidOperationException with the 
        /// key as format args
        /// </summary>
        /// <param name="key"></param>
        /// <param name="throwIfNullOrEmpty"></param>
        /// <param name="messageFormat"></param>
        /// <returns></returns>
        public static string GetAppSetting(string key, bool throwIfNullOrEmpty, string messageFormat = "Value for key ({0}) was not found in confugration appSettings")
        {
            string returnValue = GetAppSetting(key, string.Empty);
            Args.ThrowIf<InvalidOperationException>(string.IsNullOrEmpty(returnValue) && throwIfNullOrEmpty, messageFormat, key);
            return returnValue;
        }

        /// <summary>
        /// Return the specified connection string from the default configuration file.
        /// If the value is not found a blank entry with the specified name will 
        /// be added to the configuration file and an empty string will be returned.
        /// </summary>
        /// <param name="name">The name of the connection string to retrieve</param>
        /// <returns>string</returns>
        public static string GetConnectionString(string name)
        {
            return GetConnectionString(name, string.Empty);
        }

        /// <summary>
        /// Return the specified connection string from the default configuration file.
        /// If the value is not found the specified default value will be added to the
        /// configuration file and that value will be returned.
        /// </summary>
        /// <param name="name">The name of the connection string to retrieve</param>
        /// <param name="defaultValue">The value to return if the connection string
        /// is not found in the configuration file.  This value will also be added to 
        /// the configuration file.</param>
        /// <returns>string</returns>
        public static string GetConnectionString(string name, string defaultValue)
        {
            return GetConnectionString(name, defaultValue, false);
        }

        /// <summary>
        /// Return the specified connection string from the default confiruation file.
        /// If the value is not found the specified default value will be added to the
        /// configuration file and that value will be returned.
        /// </summary>
        /// <param name="name">The name of the connection string to retrieve</param>
        /// <param name="defaultValue">The value to return if the connection string
        /// is not found in the configuration file.  This value will also be added to 
        /// the configuration file.</param>
        /// <param name="add">If true and the specified name is not found the specified default
        /// value will be added to the configuration file.</param>
        /// <returns>string</returns>
        public static string GetConnectionString(string name, string defaultValue, bool add)
        {
            string returnValue = string.Empty;
            ConnectionStringSettings settings = _connectionStrings[name];//ConfigurationManager.ConnectionStrings[name];
            if (settings != null)
            {
                returnValue = settings.ConnectionString;
            }

            if (string.IsNullOrEmpty(returnValue))
            {
                returnValue = defaultValue;
            }

            if (add)
                AddConnectionString(name, returnValue);

            return returnValue;
        }

        public static void ThrowIfHasNullProperties(object target)
        {
            PropertyInfo[] properties = target.GetType().GetProperties();
            foreach (PropertyInfo property in properties)
            {
                if (property.GetValue(target, null) == null)
                {
                    throw new InvalidOperationException("Object has null properties");
                }
            }
        }
        /// <summary>
        /// Set properties from the default config file.  The config file should contain keys in the form <i>TypeName.PropertyName</i>, 
        /// where <i>TypeName</i> is the name of the class and <i>PropertyName</i> is the name of the property to set
        /// </summary>
        /// <param name="target"></param>
        public static void SetProperties(object target)
        {
            SetProperties(target, false);
        }

        /// <summary>
        /// Set properties from the default config file.  The config file should contain keys in the form <i>TypeName.PropertyName</i>, 
        /// where <i>TypeName</i> is the name of the class and <i>PropertyName</i> is the name of the property to set
        /// </summary>
        /// <param name="target"></param>
        /// <param name="throwIfMissingRequiredProperties">If required property isn't found in the default config
        /// throw an exception.</param>
        public static void SetProperties(object target, bool throwIfMissingRequiredProperties)
        {
            Type targetType = target.GetType();
            PropertyInfo[] properties = targetType.GetProperties();
            foreach (PropertyInfo property in properties)
            {
                SetProperty(target, targetType, property);
            }

            if (throwIfMissingRequiredProperties)
            {
                if (targetType.GetInterface("IHasRequiredProperties") != null)
                {
                    CheckRequiredProperties((IHasRequiredProperties)target);
                }
            }
        }

        /// <summary>
        /// Copies the currently executing assembly's config file to the specified
        /// path.
        /// </summary>
        /// <param name="filePath"></param>
        public static void CopyConfig(string filePath)
        {
            System.Configuration.Configuration config = GetConfig();
            config.SaveAs(filePath);
        }

        /// <summary>
        /// Adds the connection string.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="connectionString">The connection string.</param>
        public static void AddConnectionString(string name, string connectionString)
        {
            System.Configuration.Configuration config = GetConfig();
            AddConnectionString(name, connectionString, config);
        }

        /// <summary>
        /// Adds the connection string.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="connectionString">The connection string.</param>
        /// <param name="config">The configuration.</param>
        public static void AddConnectionString(string name, string connectionString, System.Configuration.Configuration config)
        {
            ConnectionStringSettings settings = new ConnectionStringSettings();
            settings.ConnectionString = connectionString;
            settings.Name = name;
            settings.ProviderName = "SqlClient";
            config.ConnectionStrings.ConnectionStrings.Add(settings);
            config.Save();
        }

        /// <summary>
        /// Removes the application setting.
        /// </summary>
        /// <param name="key">The key.</param>
        public static void RemoveAppSetting(string key)
        {
            RemoveAppSetting(Assembly.GetExecutingAssembly(), key);
        }

        /// <summary>
        /// Removes the application setting.
        /// </summary>
        /// <param name="assembly">The assembly.</param>
        /// <param name="key">The key.</param>
        public static void RemoveAppSetting(this Assembly assembly, string key)
        {
            GetConfig(assembly).AppSettings.Settings.Remove(key);
        }

        /// <summary>
        /// Adds the application setting.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        public static void AddAppSetting(string key, string value)
        {
            AddAppSetting(typeof(DefaultConfiguration), key, value);
        }

        /// <summary>
        /// Adds the application setting.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        public static void AddAppSetting(this Type type, string key, string value)
        {
            AddAppSetting(type.Assembly, key, value);
        }

        /// <summary>
        /// Adds the application setting.
        /// </summary>
        /// <param name="assembly">The assembly.</param>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        public static void AddAppSetting(this Assembly assembly, string key, string value)
        {
            assembly.AddAppSetting(key, value, false);
        }

        /// <summary>
        /// Adds the application setting.
        /// </summary>
        /// <param name="assembly">The assembly.</param>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <param name="throwIfExisting">if set to <c>true</c> [throw if existing].</param>
        /// <exception cref="InvalidOperationException">Attempted to set an appsetting that is already set</exception>
        public static void AddAppSetting(this Assembly assembly, string key, string value, bool throwIfExisting)
        {
            System.Configuration.Configuration config = GetConfig(assembly);
            if (config.AppSettings.Settings[key] == null)
            {
                config.AppSettings.Settings.Add(key, value);
                config.Save();
            }
            else if (throwIfExisting)
            {
                throw new InvalidOperationException("Attempted to set an appsetting that is already set");
            }
        }

        /// <summary>
        /// Gets the configuration.
        /// </summary>
        /// <returns></returns>
        public static System.Configuration.Configuration GetConfig()
        {
            return GetConfig(Assembly.GetExecutingAssembly());
        }

        /// <summary>
        /// Gets the configuration.
        /// </summary>
        /// <param name="assembly">The assembly.</param>
        /// <returns></returns>
        public static System.Configuration.Configuration GetConfig(Assembly assembly)
        {
            return GetConfig(assembly.Location);
        }

        /// <summary>
        /// Sets the application setting.
        /// </summary>
        /// <param name="configPath">The configuration path.</param>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        public static void SetAppSetting(string configPath, string key, string value)
        {
            SetAppSettings(configPath, new Dictionary<string, string> { { key, value } });
        }

        public static void SetAppSettings(FileInfo exePath, Dictionary<string, string> values)
        {
            SetAppSettings($"{exePath.FullName}.config", values);
        }

        public static void SetAppSettings(string configPath, Dictionary<string, string> values)
        {
            System.Configuration.Configuration config = GetConfig(configPath);
            foreach(string key in values.Keys)
            {
                if(config.AppSettings.Settings[key] != null)
                {
                    config.AppSettings.Settings.Remove(key);
                }
                config.AppSettings.Settings.Add(key, values[key]);
            }            

            config.Save();
        }
        /// <summary>
        /// Gets the configuration.
        /// </summary>
        /// <param name="configPath">The configuration path.</param>
        /// <returns></returns>
        public static System.Configuration.Configuration GetConfig(string configPath)
        {
            ExeConfigurationFileMap fileMap = new ExeConfigurationFileMap { ExeConfigFilename = configPath };
            System.Configuration.Configuration config = ConfigurationManager.OpenMappedExeConfiguration(fileMap, ConfigurationUserLevel.None);
            return config;
        }

        /// <summary>
        /// Sets the property.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <param name="propertyName">Name of the property.</param>
        public static void SetProperty(object target, string propertyName)
        {
            Type type = target.GetType();
            PropertyInfo property = type.GetProperty(propertyName);
            if (property != null)
            {
                SetProperty(target, type, property);
            }
            else
            {
                string propValue = _appSettings[type.Name + "." + property.Name];
                if (string.IsNullOrEmpty(propValue))
                    propValue = _appSettings[property.Name];
            }
        }

        private static void SetProperty(object target, Type targetType, PropertyInfo property)
        {

            if (property.PropertyType == typeof(string) || property.PropertyType.IsEnum)
            {
                string propFromConfig = GetProperty(targetType.Name, property.Name);

                if (!string.IsNullOrEmpty(propFromConfig))
                {
                    if (property.PropertyType == typeof(string))
                    {
                        property.SetValue(target, propFromConfig, null);
                    }
                    else if (property.PropertyType.IsEnum)
                    {
                        property.SetValue(target, Enum.Parse(property.PropertyType, propFromConfig), null);
                    }
                }

            }
        }

        /// <summary>
        /// Gets the property.
        /// </summary>
        /// <param name="targetTypeName">Name of the target type.</param>
        /// <param name="property">The property.</param>
        /// <returns></returns>
        public static string GetProperty(string targetTypeName, string property)
        {
            Dictionary<string, string> appSettings = new Dictionary<string, string>();
            foreach (string key in _appSettings.Keys)
            {
                appSettings.Add(key, _appSettings[key]);
            }

            return GetProperty(targetTypeName, property, appSettings);
        }

        /// <summary>
        /// Gets the property.
        /// </summary>
        /// <param name="targetTypeName">Name of the target type.</param>
        /// <param name="property">The property.</param>
        /// <param name="appSettings">The application settings.</param>
        /// <returns></returns>
        public static string GetProperty(string targetTypeName, string property, Dictionary<string, string> appSettings)
        {
            if (appSettings.ContainsKey(targetTypeName + "." + property))
                return appSettings[targetTypeName + "." + property];

            if (appSettings.ContainsKey(property))
                return appSettings[property];

            string propFromConfig = string.Empty;
            ConnectionStringSettings conset = ConfigurationManager.ConnectionStrings[targetTypeName + "." + property];

            if (conset == null)
                conset = ConfigurationManager.ConnectionStrings[property];

            if (conset != null)
                propFromConfig = conset.ConnectionString;

            return propFromConfig;
        }

        /// <summary>
        /// Sets the properties of the target instance that match the property names of the specified proxy
        /// from the specified proxy instance.  
        /// </summary>
        /// <param name="target">The instance to set the properties for.</param>
        /// <param name="proxy">An instance of the proxy class to use.</param>
        public static void SetPropertiesByProxyInstance(object target, object proxy)
        {
            PropertyInfo[] targetProperties = target.GetType().GetProperties();
            PropertyInfo[] proxyProperties = target.GetType().GetProperties();

            foreach (PropertyInfo targetProperty in targetProperties)
            {
                foreach (PropertyInfo proxyProperty in proxyProperties)
                {
                    if (proxyProperty.Name.Equals(targetProperty.Name))
                    {
                        targetProperty.SetValue(target, proxyProperty.GetValue(proxy, null), null);
                    }
                }
            }
        }
        /// <summary>
        /// Sets the properties of the specified target from the default config file, where the property names of the specified proxy
        /// match the property names of the target.  The config file should contain keys in the form <i>TypeName.PropertyName</i>, 
        /// where <i>TypeName</i> is the name of the proxy class and <i>PropertyName</i> is the name of the property to set
        /// on the target.
        /// </summary>
        /// <param name="target">The instance to set the properties for.</param>
        /// <param name="proxy">An instance of the proxy class to use.</param>
        public static void SetPropertiesByProxy(object target, object proxy)
        {
            SetPropertiesByProxy(target, proxy, false);
        }

        /// <summary>
        /// Sets the properties of the current instance that match the property names of the specified proxy
        /// from the default config file.  The config file should contain keys in the form <i>TypeName.PropertyName</i>, 
        /// where <i>TypeName</i> is the name of the proxy class and <i>PropertyName</i> is the name of the property to set
        /// on the current instance.
        /// </summary>
        /// <param name="target">The instance to set the properties for.</param>
        /// <param name="proxy">An instance of the proxy class to use.</param>
        /// <param name="throwIfMissingRequiredProperties">Throws an exception if target implements IHasRequiredProperties
        /// and not all required properties have been set.  The overrided default is false.</param>
        public static void SetPropertiesByProxy(object target, object proxy, bool throwIfMissingRequiredProperties)
        {
            Type targetType = target.GetType();
            Type proxyType = proxy.GetType();
            //for each property in proxy type if there is a matching property in targetType set the property
            SetPropertiesByProxy(target, proxyType, throwIfMissingRequiredProperties);
        }

        /// <summary>
        /// Sets the properties of the specified target that match the property names of the specified proxy
        /// from the default config file.  The config file should contain keys in the form <i>TypeName.PropertyName</i>, 
        /// where <i>TypeName</i> is the name of the proxy class and <i>PropertyName</i> is the name of the property to set
        /// on the target.
        /// </summary>
        /// <param name="target">The instance to set the properties for.</param>
        /// <param name="proxyType">The type of the proxy to use.</param>
        public static void SetPropertiesByProxy(object target, Type proxyType)
        {
            SetPropertiesByProxy(target, proxyType, false);
        }

        /// <summary>
        /// Sets the properties of the specified target that match the property names of the specified proxy
        /// from the default config file.  The config file should contain keys in the form <i>TypeName.PropertyName</i>, 
        /// where <i>TypeName</i> is the name of the proxy class and <i>PropertyName</i> is the name of the property to set
        /// on the target.
        /// </summary>
        /// <param name="target">The instance to set the properties for.</param>
        /// <param name="proxyType">The type of the proxy to use.</param>
        /// <param name="throwIfMissingRequiredProperties">Throws an exception if target implements IHasRequiredProperties
        /// and not all required properties have been set.  The overrided default is false.</param>
        public static void SetPropertiesByProxy(object target, Type proxyType, bool throwIfMissingRequiredProperties)
        {
            Type targetType = target.GetType();
            PropertyInfo[] proxyProperties = proxyType.GetProperties();
            foreach (PropertyInfo proxyProp in proxyProperties)
            {
                PropertyInfo targetProp = targetType.GetProperty(proxyProp.Name);
                if (targetProp != null)
                    SetProperty(target, proxyType, targetProp);
            }

            if (throwIfMissingRequiredProperties)
            {
                if (targetType.GetInterface("IHasRequiredProperties") != null)
                {
                    CheckRequiredProperties((IHasRequiredProperties)target, proxyType);
                }
            }
        }

        /// <summary>
        /// Copies properties that match in name and type from source
        /// to destination.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        public static void CopyProperties(object source, object destination)
        {
            Type sourceType = source.GetType();
            Type destinationType = destination.GetType();
            foreach (PropertyInfo destinationProperty in destinationType.GetProperties())
            {
                PropertyInfo sourceProperty = sourceType.GetProperty(destinationProperty.Name);
                if (sourceProperty != null &&
                    sourceProperty.PropertyType.Equals(destinationProperty.PropertyType) &&
                    destinationProperty.CanWrite)
                {
                    object value = sourceProperty.GetValue(source, null);
                    destinationProperty.SetValue(destination, value, null);
                }
            }
        }

        /// <summary>
        /// Checks the properties named in the RequiredProperties property of the specified
        /// IHasRequiredProperties object ensuring that each has been set. This method will 
        /// throw an error if any required property is null or an empty string.
        /// </summary>
        /// <param name="target">The IHasRequiredProperties implementation to check.</param>
        public static void CheckRequiredProperties(this IHasRequiredProperties target)
        {
            CheckRequiredProperties(target, target.GetType());
        }

        /// <summary>
        /// Checks the properties named in the RequiredProperties property of the specified
        /// IHasRequiredProperties object ensuring that each has been set. This method will 
        /// throw an error if required properties have not been set.
        /// </summary>
        /// <param name="target">The IHasRequiredProperties implementation to check.</param>
        private static void CheckRequiredProperties(IHasRequiredProperties target, Type configType)
        {
            foreach (string property in target.RequiredProperties)
            {
                Type targetType = target.GetType();
                PropertyInfo prop = targetType.GetProperty(property);
                if (prop == null)
                    throw new InvalidIHasRequiredPropertiesImplementationException(targetType, property);

                string propVal = (string)prop.GetValue(target, null);

                if (string.IsNullOrEmpty(propVal))
                    throw new RequiredPropertyNotSetException(configType, prop);
            }
        }

        /// <summary>
        /// Invokes methods declared in the appSettings section of the default configuration file.  
        /// </summary>
        /// <param name="target">The object instance to invoke methods on</param>
        /// <remarks>
        /// The keys 
        /// should be in the format &lt;Type.Name&gt;.InvokeMethod.&lt;MethodName&gt;.&lt;Order as int&gt;.  For
        /// example: PatternsDatabaseSetupPermissionsInfo.InvokeMethod.AddStandardDbOwnerGroup.0.  The trailing number
        /// determines the order of invocation relative to all other method invocations from configuration.
        /// The values should be a comma separated list of string parameters to be passed to the method upon invocation.
        /// Any additional spaces in the comma separated list of values will not be trimmed so the invoked methods
        /// should handle that possibility accordingly.
        /// </remarks>
        public static void InvokeConfigMethods(object target)
        {
            Dictionary<string, string> appSettingDictionary = new Dictionary<string, string>();
            foreach (string appSettingKey in _appSettings.Keys)
            {
                appSettingDictionary.Add(appSettingKey, _appSettings[appSettingKey]);
            }
            InvokeConfigMethods(target, appSettingDictionary);
        }

        /// <summary>
        /// Invokes methods declared in the specified appSettingDictionary.  
        /// </summary>
        /// <param name="target">The object instance to invoke methods on</param>
        /// <param name="appSettingDictionary">A dictionary of methods to invoke along with the parameter values.</param>
        /// <remarks>
        /// The dictionary keys 
        /// should be in the format &lt;Type.Name&gt;.InvokeMethod.&lt;MethodName&gt;.&lt;Order as int&gt;.  For
        /// example: PatternsDatabaseSetupPermissionsInfo.InvokeMethod.AddStandardDbOwnerGroup.0.  The trailing number
        /// determines the order of invocation relative to all other method invocations from configuration.
        /// The values should be a comma separated list of string parameters to be passed to the method upon invocation.
        /// Any additional spaces in the comma separated list of values will not be trimmed so the invoked methods
        /// should handle that possibility accordingly.
        /// </remarks>
        public static void InvokeConfigMethods(object target, Dictionary<string, string> appSettingDictionary)
        {
            Type targetType = target.GetType();
            MethodInfo[] methods = targetType.GetMethods();
            List<int> methodOrder = new List<int>();
            Dictionary<int, MethodInfo> methodsToInvoke = new Dictionary<int, MethodInfo>();
            Dictionary<int, object[]> methodParams = new Dictionary<int, object[]>();
            foreach (MethodInfo method in methods)
            {
                foreach (string appSettingKey in appSettingDictionary.Keys)
                {
                    string[] splitKey = appSettingKey.Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries);
                    if (splitKey.Length == 4 &&
                        splitKey[0].Equals(targetType.Name) &&
                        splitKey[1].Equals("InvokeMethod") &&
                        splitKey[2].Equals(method.Name))
                    {
                        if (int.TryParse(splitKey[3], out int methNum))
                        {
                            methodOrder.Add(methNum);
                            methodsToInvoke.Add(methNum, method);
                            string[] parameters = appSettingDictionary[appSettingKey].Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                            methodParams.Add(methNum, parameters);
                        }
                    }
                }
            }

            methodOrder.Sort();
            foreach (int i in methodOrder)
            {
                MethodInfo method = methodsToInvoke[i];
                object[] parameters = methodParams[i];
                method.Invoke(target, parameters);
            }
        }

        private static void ThrowInvalidOperationException(Type t)
        {
            throw new InvalidOperationException("The target object specified is of type " + t.Name + " which is not serializable.  If this is a user defined type add the [Serializable] attribute to the class definition");
        }


    }
}

