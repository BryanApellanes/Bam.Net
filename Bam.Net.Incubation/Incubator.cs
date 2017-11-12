/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using Bam.Net;

namespace Bam.Net.Incubation
{
    /// <summary>
    /// A simple dependency injection container.
    /// </summary>
    public class Incubator: ISetupContext
    {
        object _accessLock = new object();
        Dictionary<Type, object> _typeInstanceDictionary;
        Dictionary<string, Type> _classNameTypeDictionary;
        Dictionary<Type, Dictionary<string, object>> _ctorParams;

        static Incubator()
        {
            Default = new Incubator();
        }

        public Incubator()
        {
            _typeInstanceDictionary = new Dictionary<Type, object>();
            _classNameTypeDictionary = new Dictionary<string, Type>();
            _ctorParams = new Dictionary<Type, Dictionary<string, object>>();
        }

        public static Incubator Default
        {
            get;
            set;
        }

        public Incubator Clone()
        {
            lock (_accessLock)
            {
                Incubator val = new Incubator();
                foreach (Type t in _typeInstanceDictionary.Keys)
                {
                    val._typeInstanceDictionary.Add(t, _typeInstanceDictionary[t]);
                }
                foreach (string s in _classNameTypeDictionary.Keys)
                {
                    val._classNameTypeDictionary.Add(s, _classNameTypeDictionary[s]);
                }
                foreach (Type type in _ctorParams.Keys)
                {
                    val._ctorParams.Add(type, _ctorParams[type]);
                }

                return val;
            }
        }

        /// <summary>
        /// Copy the values from the specified incubator to the current; the same as CopyFrom
        /// </summary>
        /// <param name="incubator">The incubator to copy from</param>
        /// <param name="overwrite">If true, values in the current incubator
        /// will be over written by values of the same types from the specified
        /// incubator otherwise the current value will be kept</param>
        public void CombineWith(Incubator incubator, bool overwrite = true)
        {
            CopyFrom(incubator, overwrite);
        }
        
        /// <summary>
        /// Copy the values from the specified incubator to the current; the same as CombineWith
        /// </summary>
        /// <param name="incubator">The incubator to copy from</param>
        /// <param name="overwrite">If true, values in the current incubator
        /// will be over written by values of the same types from the specified
        /// incubator otherwise the current value will be kept</param>
        public void CopyFrom(Incubator incubator, bool overwrite = true)
        {
            lock (_accessLock)
            {
                foreach (Type t in incubator._typeInstanceDictionary.Keys)
                {
                    if (!this._typeInstanceDictionary.ContainsKey(t) || overwrite)
                    {
                        this._typeInstanceDictionary[t] = incubator._typeInstanceDictionary[t];
                    }
                }
                foreach (string s in incubator._classNameTypeDictionary.Keys)
                {
                    if (!this._classNameTypeDictionary.ContainsKey(s) || overwrite)
                    {
                        this._classNameTypeDictionary[s] = incubator._classNameTypeDictionary[s];
                    }
                }
            }
        }

        /// <summary>
        /// Constructs an instance of type T by finding a constructor
        /// that can take objects of types that have already been 
        /// constructed or set.  If the constructor parameters have not
        /// been instantiated an InvalidOperationException will be 
        /// thrown.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T Construct<T>()
        {
            return (T)Construct(typeof(T));
        }

        /// <summary>
        /// Construct an instance of type T without
        /// setting the new instance as the new internal gettable instance
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetWithoutSet<T>()
        {
            return (T)GetWithoutSet(typeof(T));
        }

        /// <summary>
        /// Construct an instance of type T without
        /// setting the new instance as the new internal gettable instance
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public object GetWithoutSet(Type type)
        {
            ConstructorInfo ctor;
            List<object> ctorParams;
            GetCtorAndParams(type, out ctor, out ctorParams);
            return ctor.Invoke(ctorParams.ToArray());
        }

        /// <summary>
        /// Construct an instance of the specified type
        /// injecting constructor params from the current 
        /// incubator
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public object Construct(Type type)
        {
            ConstructorInfo ctor;
            List<object> ctorParams;
            GetCtorAndParams(type, out ctor, out ctorParams);
            this[type] = ctor.Invoke(ctorParams.ToArray());
            return this[type];
        }
        
        /// <summary>
        /// Set writable properties of the specified instance to 
        /// instances in the current Incubator.
        /// </summary>
        /// <param name="instance"></param>
        public void SetProperties(object instance)
        {
            Type type = instance.GetType();
            
            PropertyInfo[] properties = type.GetProperties();
            foreach (PropertyInfo prop in properties)
            {
                object value = this[prop.PropertyType];
                Delegate getter = value as Delegate;
                value = getter != null ? getter.DynamicInvoke() : value;
                // TODO: if value is null add a check for a 
                // custom attribute (not yet defined) to determine if we should do
                // Get(prop.PropertyType)
                if (prop.CanWrite && value != null)
                {
                    prop.SetValue(instance, value, null);
                }
            }
        }

        /// <summary>
        /// Constructs an object of type T passing the specified ctorParams to the 
        /// contructor.
        /// </summary>
        /// <typeparam name="T">The type of the object to instantiate.</typeparam>
        /// <param name="ctorParams">The object values to pass to the constructor of type T.</param>
        /// <exception cref="InvalidOperationException">If the constructor with a signature matching
        /// the types of the specified ctorParams is not found.</exception>
        public T Construct<T>(params object[] ctorParams)
        {
            Type type = typeof(T);

            Construct(type, ctorParams);
            return (T)this[type];
        }

        /// <summary>
        /// Constructs an object of the specified type passing the specified
        /// ctorParams to the constructor.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="ctorParams"></param>
        /// <returns></returns>
        public object Construct(Type type, params object[] ctorParams)
        {
            Type[] ctorTypes = new Type[ctorParams.Length];
            for (int i = 0; i < ctorTypes.Length; i++)
            {
                ctorTypes[i] = ctorParams[i].GetType();
            }

            ConstructorInfo ctor = type.GetConstructor(ctorTypes);
            if (ctor == null)
            {
                Throw(type, ctorTypes);
            }

            this[type] = ctor.Invoke(ctorParams);
            return this[type];
        }

        private static void Throw(Type type, Type[] ctorTypes)
        {
            throw new ConstructFailedException(type, ctorTypes);
        }

        /// <summary>
        /// Constructs an object of type T using existing instances
        /// of the specified ctorParamTypes.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ctorParamTypes"></param>
        /// <returns></returns>
        public T Construct<T>(params Type[] ctorParamTypes)
        {
            object[] ctorParams = GetInstancesFromTypes(ctorParamTypes);

            return Construct<T>(ctorParams);
        }

        public object Construct(Type type, Type[] ctorParamTypes)
        {
            object[] ctorParams = GetInstancesFromTypes(ctorParamTypes);

            return Construct(type, ctorParams);
        }

        private object[] GetInstancesFromTypes(Type[] ctorParamTypes)
        {
            if (ctorParamTypes == null)
            {
                return new object[] { };
            }
            object[] ctorParams = new object[ctorParamTypes.Length];
            for (int i = 0; i < ctorParamTypes.Length; i++)
            {
                Type type = ctorParamTypes[i];
                object instance = this[type];
                if (instance == null)
                {
                    throw new InvalidOperationException(string.Format("An object of type {0} has not been instantiated in the current container context.", type.Name));
                }

                ctorParams[i] = instance;
            }
            return ctorParams;
        }

        object _getLock = new object();
        private T GetInternal<T>()
        {
            Func<T> f = this[typeof(T)] as Func<T>;
            Func<Type, T> fp = this[typeof(T)] as Func<Type, T>;
            if (f != null)
            {
                return f();
            }
            else if (fp != null)
            {
                return fp(typeof(T));
            }
            else
            {
                return (T)this[typeof(T)];
            }
        }
        
        public T Get<T>(string className)
        {
            return (T)Get(className);
        }

        public object Get(string className)
        {
            return Get(className, out Type t);
        }

        public object Get(Type type)
        {
            return Get(type, GetCtorParams(type).ToArray());
        }

        public object Get(string className, out Type type)
        {
            type = this[className];
            if (type != null)
            {
                object result = this[type];
                if (result is Func<object> fn)
                {
                    return fn() ?? Get(type, GetCtorParams(type));
                }
                else
                {
                    result = Get(type, GetCtorParams(type));
                }
                return result;
            }

            return null;
        }

        public object Get(Type type, params Type[] ctorParamTypes)
        {
            if (this[type] == null)
            {
                Construct(type, ctorParamTypes);
            }

            return this[type];
        }

        /// <summary>
        /// Gets an object of type T if it has been instantiated otherwise
        /// calls Construct and returns the result.
        /// </summary>
        /// <typeparam name="T">The type of the object get.</typeparam>
        /// <param name="ctorParamTypes">Array of types used to retrieve the parameters passed to the contructor of
        /// type T</param>
        /// <returns>T</returns>
        public T Get<T>(params Type[] ctorParamTypes)
        {
            if (this[typeof(T)] == null)
            {
                return Construct<T>(ctorParamTypes);
            }
            else
            {
                return GetInternal<T>();
            }
        }

		public bool TryGet<T>(out T value)
		{
			Exception ignore;
			return TryGet<T>(out value, out ignore);
		}

		public bool TryGet<T>(out T value, out Exception ex)
		{
			ex = null;
			value = default(T);
			bool result = false;
			try
			{
				value = Get<T>();
				result = true;
			}
			catch (Exception e)
			{
				ex = e;
			}
			return result;
		}

        /// <summary>
        /// Gets an object of type T if it has been instantiated otherwise
        /// calls Construct and returns the result.
        /// </summary>
        /// <typeparam name="T">The type of the object to get.</typeparam>
        /// <returns>T</returns>
        public T Get<T>()
        {
            if (this[typeof(T)] == null)
            {
                T getInternal = GetInternal<T>();
                if(getInternal == null)
                {
                    this[typeof(T)] = Construct<T>();
                }
            }

            return GetInternal<T>();
        }

        /// <summary>
        /// Gets an object of type T if it has been instantiated otherwise
        /// sets the inner instance to the specified setToIfNull and returns
        /// it.  This results in the specified setToIfNull being returned
        /// for subsequent calls to this method.
        /// </summary>
        /// <typeparam name="T">The type of the object to get</typeparam>
        /// <param name="setToIfNull">The instance to set the inner instance to if
        /// it has not been previously set</param>
        /// <returns>T</returns>
        public T Get<T>(T setToIfNull)
        {
            if (this[typeof(T)] == null)
            {
                this[typeof(T)] = setToIfNull;
            }

            return GetInternal<T>();
        }
        /// <summary>
        /// Gets an object of type T if it has been instantiated otherwise
        /// calls Construct and returns the result.
        /// </summary>
        /// <typeparam name="T">The type of the object to get.</typeparam>
        /// <param name="ctorParams">Array of objects to pass to the constructor of type T</param>
        /// <returns>T</returns>
        public T Get<T>(params object[] ctorParams)
        {
            if (this[typeof(T)] == null)
            {
                return Construct<T>(ctorParams);
            }
            else
            {
                return (T)this[typeof(T)];
            }
        }

        public object Get(Type type, params object[] ctorParams)
        {
            if (this[type] == null)
            {
                return Construct(type, ctorParams);
            }
            else
            {
                return this[type];
            }
        }
        
        public void Set<T>(T instance)
        {
            Set<T>(instance, false);
        }

        /// <summary>
        /// Sets the inner instance of type T to the specified
        /// instance.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="instance"></param>
        public void Set<T>(T instance, bool throwIfSet)
        {
            Check<T>(throwIfSet);

            this[typeof(T)] = instance;
        }

        public void Set<T>(Func<T> instanciator, bool throwIfSet = false)
        {
            Check<T>(throwIfSet);

            this[typeof(T)] = instanciator;
        }

        public void Set<T>(Func<Type, T> instanciator, bool throwIfSet = false)
        {
            Check<T>(throwIfSet);

            this[typeof(T)] = instanciator;
        }

        public void Set(Type type, Func<object> instanciator, bool throwIfSet = false)
        {
            Check(type, throwIfSet);

            this[type] = instanciator;
        }

        public void Set(Type forType, Type useType, bool throwIfSet = false)
        {
            Set(forType, Construct(useType), throwIfSet);
        }

        public void Set(Type type, object instance, bool throwIfSet = false)
        {
            Check(type, throwIfSet);

            this[type] = instance;
        }
        
        private void Check(Type t, bool throwIfSet)
        {
            if (throwIfSet && Contains(t))
            {
                throw new InvalidOperationException(string.Format("Type of ({0}) already set in this incubator", t.Name));
            }
        }

        private void Check<T>(bool throwIfSet)
        {
            if (throwIfSet && Contains<T>())
            {
                throw new InvalidOperationException(string.Format("Type of <{0}> already set in this incubator", typeof(T).Name));
            }
        }

        public string[] ClassNames
        {
            get
            {
                return _classNameTypeDictionary.Keys.ToArray();
            }
        }

        /// <summary>
        /// Types as they would be resolved when using 
        /// the values in ClassNames
        /// </summary>
        public Type[] ClassNameTypes
        {
            get
            {
                HashSet<Type> types = new HashSet<Type>();
                foreach (string cn in ClassNames)
                {
                    Type type = this[cn];
                    if (type != null)
                    {
                        types.Add(type);
                    }
                }
                return types.ToArray();
            }
        }

        public Type this[string className]
        {
            get
            {
                if (_classNameTypeDictionary.ContainsKey(className))
                {
                    return _classNameTypeDictionary[className];
                }
                else
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// All the Types that are mapped to instances
        /// or instanciators
        /// </summary>
        public Type[] MappedTypes
        {
            get
            {
                return _typeInstanceDictionary.Keys.ToArray();
            }
        }

        public void Remove<T>()
        {
            Remove(typeof(T));
        }

        public void Remove(string className)
        {
            Type ignore;
            Remove(className, out ignore);
        }

        public void Remove(string className, out Type type)
        {
            type = this[className];
            if (type != null)
            {
                Remove(type);
            }
        }

        public void Remove(Type type)
        {            
            string fullyQualifiedTypeName = string.Format("{0}.{1}", type.Namespace, type.Name);

            lock (_accessLock)
            {
                if (_typeInstanceDictionary.ContainsKey(type))
                {
                    _typeInstanceDictionary.Remove(type);
                }

                if (_classNameTypeDictionary.ContainsKey(type.Name))
                {
                    _classNameTypeDictionary.Remove(type.Name);
                }

                if (_classNameTypeDictionary.ContainsKey(fullyQualifiedTypeName))
                {
                    _classNameTypeDictionary.Remove(fullyQualifiedTypeName);
                }
            }
        }

        public bool HasClass(string className)
        {
            return this[className] != null;
        }

        public bool Contains<T>()
        {
            return Contains(typeof(T));
        }

        public bool Contains(Type type)
        {
            return this[type] != null;
        }

        /// <summary>
        /// Gets the inner instance of the type specified or
        /// null if it has not been set through a call to Set(), Get() or 
        /// Construct().
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public object this[Type type]
        {
            get
            {
                if (_typeInstanceDictionary.ContainsKey(type))
                {
                    return _typeInstanceDictionary[type];
                }
                else
                {
                    return null;
                }
            }
            set
            {
                if (_typeInstanceDictionary.ContainsKey(type))
                {
                    _typeInstanceDictionary[type] = value;
                }
                else
                {
                    lock (_accessLock)
                    {
                        _typeInstanceDictionary.Add(type, value);
                        string fullyQualifiedTypeName = string.Format("{0}.{1}", type.Namespace, type.Name);
                        if (!_classNameTypeDictionary.ContainsKey(type.Name))
                        {
                            _classNameTypeDictionary.Add(type.Name, type);
                        }
                        else if (!_classNameTypeDictionary.ContainsKey(fullyQualifiedTypeName))
                        {
                            _classNameTypeDictionary.Add(fullyQualifiedTypeName, type);
                        }
                        else
                        {
                            throw new InvalidOperationException(string.Format("The specified type {0} conflicts with an existing type registration.", type.Name));
                        }
                    }
                }
            }
        }
        
        /// <summary>
        /// Set the value to pass into the constructor when 
        /// constructing the specified type
        /// </summary>
        /// <param name="forType"></param>
        /// <param name="parameterName"></param>
        /// <param name="value"></param>
        public void SetCtorParam(Type forType, string parameterName, object value)
        {
            lock (_accessLock)
            {
                if (!_ctorParams.ContainsKey(forType))
                {
                    _ctorParams.Add(forType, new Dictionary<string, object>());
                }

                if (!_ctorParams[forType].ContainsKey(parameterName))
                {
                    _ctorParams[forType].Add(parameterName, value);
                }
            }
        }

        public object GetCtorParameterValue(Type forType, string parameterName)
        {
            if(_ctorParams.ContainsKey(forType) && _ctorParams[forType].ContainsKey(parameterName))
            {
                return _ctorParams[forType][parameterName];
            }
            return null;
        }

        private void GetCtorAndParams(Type type, out ConstructorInfo ctor, out List<object> ctorParams)
        {
            ctorParams = GetCtorParams(type, out ctor);
            if (ctor == null)
            {
                Throw(type, ctorParams.Select(p => p.GetType()).ToArray());
            }
        }

        private List<object> GetCtorParams(Type type)
        {
            ConstructorInfo ctor;
            return GetCtorParams(type, out ctor);
        }
        private List<object> GetCtorParams(Type type, out ConstructorInfo ctorInfo)
        {
            ctorInfo = null;
            ConstructorInfo[] ctors = type.GetConstructors();
            List<object> ctorParams = new List<object>();
            foreach (ConstructorInfo ctor in ctors)
            {
                ParameterInfo[] parameters = ctor.GetParameters();
                if (parameters.Length > 0)
                {
                    foreach (ParameterInfo paramInfo in parameters)
                    {
                        object ctorParam = GetCtorParameterValue(type, paramInfo.Name);
                        if(ctorParam != null)
                        {
                            ctorParams.Add(ctorParam);
                        }
                        else
                        {
                            object existing = this[paramInfo.ParameterType] ?? Get(paramInfo.ParameterType, GetCtorParams(paramInfo.ParameterType).ToArray());
                            if (existing != null)
                            {
                                ctorParams.Add(existing);
                            }
                            else
                            {
                                ctorParams.Clear();
                                break;
                            }
                        }
                    }
                }

                if (ctorParams.Count == parameters.Length)
                {
                    ctorInfo = ctor;
                    break;
                }
            }
            return ctorParams;
        }
    }
}
