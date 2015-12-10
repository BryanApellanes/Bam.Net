/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Naizari.Helpers
{
    public class ExceptionHelper
    {
        public static void ThrowIfNull(object objectToCheck, string paramName)
        {
            if (objectToCheck == null)
                throw new ArgumentNullException(paramName);
        }

        public static void ThrowIfEmptyOrNull(string stringToCheck, string paramName)
        {
            if (string.IsNullOrEmpty(stringToCheck))
                throw new ArgumentNullException(paramName);
        }

        public static void ThrowIfIsNotOfType<T>(object objectToCheck, string paramName)
        {
            ThrowIfNull(objectToCheck, paramName);

            if (!(objectToCheck is T))
                throw new InvalidOperationException(string.Format("The specified object is of concrete type {0} which is not of generic type {1}.", objectToCheck.GetType().Name, typeof(T).Name));
        }

        public static void Throw<TException>(string messageFormat, params object[] args) where TException : Exception
        {
            throw CreateException<TException>(messageFormat, args);
        }

        public static TException CreateException<TException>(string messageFormat, params object[] args) where TException : Exception
        {
            Type exceptionType = typeof(TException);
            ConstructorInfo ctor = exceptionType.GetConstructor(new Type[] { typeof(string)});//, typeof(object[]) });
            return (TException)ctor.Invoke(new object[] { string.Format(messageFormat, args) });
        }

        public static void ThrowInvalidOperation(string messageFormat, params object[] args)
        {
            throw new InvalidOperationException(string.Format(messageFormat, args));
        }
    }
}
