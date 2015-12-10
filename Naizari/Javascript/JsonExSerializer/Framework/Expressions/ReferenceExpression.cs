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

namespace Naizari.Javascript.JsonExSerialization.Framework.Expressions
{
    /// <summary>
    /// A reference to another object
    /// </summary>
    public sealed class ReferenceExpression : Expression
    {
        private Expression _reference;   // the expression that is referenced
        private JsonPath _path; // path to the referenced expression
        private object result;

        public ReferenceExpression(string ReferencePath)
        {
            this._path = new JsonPath(ReferencePath);
        }

        public ReferenceExpression(JsonPath ReferencePath)
        {
            this._path = ReferencePath;
        }

        public JsonPath Path
        {
            get { return _path; }
        }

        public override Type DefaultType
        {
            get { return typeof(object); }
        }
        public Expression ReferencedExpression
        {
            get { return _reference; }
            set
            {
                if (_reference == value)
                    return;
                if (_reference != null)
                    throw new InvalidOperationException("Attempt to change referenced expression after its already been set");
                _reference = value;
                _reference.ObjectConstructed += new EventHandler<ObjectConstructedEventArgs>(Reference_ObjectConstructed);
            }
        }

        void Reference_ObjectConstructed(object sender, ObjectConstructedEventArgs e)
        {
            result = e.Result;
        }

        public object ReferencedValue {
            get
            {
                if (result == null)
                    throw new InvalidOperationException("Attempt to reference " + Path + " before its constructed");
                return result;
            }
        }
    }
}
