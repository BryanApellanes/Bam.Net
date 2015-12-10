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

namespace Naizari.Javascript.JsonExSerialization.Framework.Parsing
{
    /// <summary>
    /// The type for a given token
    /// </summary>
    enum TokenType
    {
        Number,
        Identifier,
        DoubleQuotedString,
        SingleQuotedString,
        Symbol
    }

    /// <summary>
    /// Structure to represent a token from the input stream
    /// </summary>
    struct Token
    {
        public static Token Empty = new Token();

        public TokenType type;
        public string value;
        public Token(TokenType type, string value)
        {
            this.type = type;
            this.value = value;
        }

        public override bool Equals(object other)
        {
            return other is Token && Equals((Token)other);
        }

        public static bool operator ==(Token lhs, Token rhs)
        {
            return lhs.Equals(rhs);
        }

        public static bool operator !=(Token lhs, Token rhs)
        {
            return !lhs.Equals(rhs);
        }

        private bool Equals(Token other)
        {
            return type == other.type && value == other.value;
        }

        public override string ToString()
        {
            return string.Format("{0}: {1} {2}", GetType().Name, type, value);
        }

        public override int GetHashCode()
        {
            return ((string)(type.ToString() + ":" + value.ToString())).GetHashCode();
        }
    }
}
