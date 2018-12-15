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
    // object types, collection types
    /// <summary>
    /// Base class for complex types: objects and collections
    /// </summary>
    public abstract class ComplexExpressionBase : Expression {
        private IList<Expression> _constructorArguments;

        protected ComplexExpressionBase()
        {
            _constructorArguments = new List<Expression>();
        }

        /// <summary>
        /// Arguments to the constructor if any
        /// </summary>
        public IList<Expression> ConstructorArguments
        {
            get { return this._constructorArguments; }
            set { 
                this._constructorArguments = value;
                if (value != null)
                {
                    foreach (Expression exp in value)
                    {
                        exp.Parent = this;
                    }
                }
            }
        }
    }
}
