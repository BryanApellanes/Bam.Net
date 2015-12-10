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
using System.Collections;

namespace Naizari.Javascript.JsonExSerialization.Framework.Expressions
{
    /// <summary>
    /// Represents a javascript object
    /// </summary>
    public sealed class ObjectExpression : ComplexExpressionBase {

        private IList<KeyValueExpression> _properties;

        public ObjectExpression()
        {
            _properties = new List<KeyValueExpression>();
        }

        public override Type DefaultType
        {
            get { return typeof(Hashtable); }
        }

        /// <summary>
        /// The object's properties
        /// </summary>
        public IList<KeyValueExpression> Properties
        {
            get { return this._properties; }
            set { this._properties = value; }
        }

        public Expression this[string key]
        {
            get {
                int keyIndex = IndexOf(key);
                return keyIndex != -1 ? Properties[keyIndex].ValueExpression : null;
            }
            set {
                int keyIndex = IndexOf(key);
                if (keyIndex != -1)
                    Properties[keyIndex].ValueExpression = value;
                else
                    Add(key, value);
            }
        }

        /// <summary>
        /// Finds the index of the given key in the property list
        /// </summary>
        /// <param name="key">the key to find</param>
        /// <returns>0-based index of the key/value property in the list or -1 if not found</returns>
        public int IndexOf(string key)
        {
            for (int i = 0; i < Properties.Count; i++)
            {
                KeyValueExpression keyValue = Properties[i];
                if (keyValue.KeyExpression is ValueExpression && keyValue.Key == key)
                    return i;
            }
            return -1;
        }

        /// <summary>
        /// Add a property to this object
        /// </summary>
        /// <param name="key">the key for the property</param>
        /// <param name="value">the value for the property</param>
        /// <returns>KeyValueExpression that was added</returns>
        public KeyValueExpression Add(Expression key, Expression value)
        {
            return Add(new KeyValueExpression(key, value));
        }

        /// <summary>
        /// Add a property to this object
        /// </summary>
        /// <param name="expression">the key value expression to add</param>
        /// <returns>KeyValueExpression that was added</returns>
        public KeyValueExpression Add(KeyValueExpression expression)
        {
            expression.Parent = this;
            expression.ValueExpression.Parent = this;
            Properties.Add(expression);
            return expression;
        }

        /// <summary>
        /// Add a property to this object
        /// </summary>
        /// <param name="key">the key for the property</param>
        /// <param name="value">the value for the property</param>
        /// <returns>KeyValueExpression that was added</returns>
        public KeyValueExpression Add(string key, Expression value)
        {
            return Add(new ValueExpression(key), value);
        }
    } 
}
