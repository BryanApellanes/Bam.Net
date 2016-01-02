/*
	Copyright © Bryan Apellanes 2015  
*/
// <copyright file="ArrayBuilder.cs" company="JsonExSerializer Project Contributors">
// Copyright (c) 2007, JsonExSerializer Project Contributors
// Code licensed under the New BSD License:
// http://code.google.com/p/jsonexserializer/wiki/License
// </copyright>

using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace Naizari.Javascript.JsonExSerialization.Collections
{

    /// <summary>
    /// Collection builder class for arrays
    /// </summary>
    public class ArrayBuilder : ICollectionBuilder
    {
        /// <summary>
        /// The type for the array
        /// </summary>
        private Type _arrayType;

        /// <summary>
        /// The final array being built
        /// </summary>
        private Array result;

        /// <summary>
        /// The current index for insertion into the array
        /// </summary>
        private int index;

        /// <summary>
        /// Initializes an ArrayBuilder to build an array of Type <paramref name="arrayType"/> containing
        /// <paramref name="itemCount"/> elements.
        /// </summary>
        /// <param name="arrayType">The type of the array to build</param>
        /// <param name="itemCount">The number of elements in the array</param>
        public ArrayBuilder(Type arrayType, int itemCount)
        {
            _arrayType = arrayType;
            if (_arrayType.IsArray)
            {
                result = Array.CreateInstance(_arrayType.GetElementType(), itemCount);
            }
            else
            {
                throw new ArgumentException("arrayType parameter must be of type Array");
            }
        }
        #region ICollectionBuilder Members

        /// <summary>
        /// Adds an element to the array
        /// </summary>
        /// <param name="item">the item to add</param>
        public void Add(object item)
        {
            result.SetValue(item, index++);
        }

        /// <summary>
        /// Returns the array that was built
        /// </summary>
        /// <returns>an array</returns>
        public object GetResult()
        {
            return result;            
        }

        /// <summary>
        /// Returns a reference to the array being built
        /// </summary>
        /// <returns></returns>
        public object GetReference()
        {
            return result;
        }

        #endregion
    }
}
