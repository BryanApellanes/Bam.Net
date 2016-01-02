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
using System.IO;

namespace Naizari.Javascript.JsonExSerialization.Framework.Parsing
{
    /// <summary>
    /// Tokenizes input from the specified reader and returns tokens for the parser to parse.
    /// </summary>
    sealed class TokenStream
    {
        #region Member Variables

        private TextReader _reader;
        private Queue<Token> _tokens;
        private char[] _symbols;
        private StringBuilder _buffer;
        private int _capacity;
        #endregion

        /// <summary>
        /// Create an instance of the token stream to read from the given reader.
        /// </summary>
        /// <param name="reader"></param>
        public TokenStream(TextReader reader)
        {
            _reader = reader;
            _capacity = 16;
            _tokens = new Queue<Token>(_capacity);
            _symbols = "[]<>():,{}.$".ToCharArray();
            Array.Sort<char>(_symbols);
            _buffer = new StringBuilder();
            //Fill();
        }

        /// <summary>
        /// Peek at the next available token without consuming it.
        /// </summary>
        /// <returns>the next available token, or the empty token if all tokens have been read</returns>
        /// <see cref="Token.Empty"/>
        public Token PeekToken()
        {
            if (_tokens.Count == 0)
                Fill();

            if (_tokens.Count == 0)
                return Token.Empty;
            else
                return _tokens.Peek();
        }

        /// <summary>
        /// Reads the next available token and consumes it.
        /// </summary>
        /// <returns>the next available token, or the empty token if all tokens have been read</returns>
        /// <see cref="Token.Empty"/>
        public Token ReadToken()
        {
            if (_tokens.Count == 0)
                Fill();

            if (_tokens.Count > 0)
                return _tokens.Dequeue();
            else
                return Token.Empty;
        }

        /// <summary>
        /// Checks to see if there are any more tokens to be read
        /// </summary>
        /// <returns>true if no more tokens</returns>
        public bool IsEmpty()
        {
            return PeekToken() == Token.Empty;
        }

        private void Fill()
        {
            Token t;
            while ((t = ReadTokenFromReader()) != Token.Empty)
                _tokens.Enqueue(t);

            if (t == Token.Empty)
                _tokens.Enqueue(Token.Empty);
        }

        /// <summary>
        /// Reads a token from the text reader and returns it
        /// </summary>
        /// <returns></returns>
        private Token ReadTokenFromReader()
        {
            StringBuilder buffer = _buffer;
            buffer.Length = 0;

            int c;
            char ch;

            while ((c = _reader.Read()) != -1) {
                ch = (char) c;
                if (IsQuoteStart(ch)) {
                    return GetQuotedString(ch, buffer);
                } else if (IsNumberStart(ch)) {
                    return GetNumber(ch, buffer);
                } else if (char.IsWhiteSpace(ch)) {
                    // nothing
                } else if (IsIdentifierStart(ch)) {
                    return  GetIdentifier(ch, buffer);
                } else if (IsLineCommentStart(ch)) {
                    ReadLineComment(ch);
                } else if (IsMultilineCommentStart(ch)) {
                    ReadMultilineComment(ch);
                } else if (IsSymbolStart(ch)) {
                    return GetSymbol(ch);
                } else {
                    throw new ParseException("Invalid character");
                }
                buffer.Length = 0;
            }
            return Token.Empty;
        }

        #region Read Methods

        /// <summary>
        /// Reads a C# multiline comment
        /// <example>
        /// /*
        ///   This is a multiline comment
        /// */
        /// </example>
        /// </summary>
        /// <param name="ch">the starting character</param>
        private void ReadMultilineComment(char ch)
        {
            // read until we see */
            _reader.Read(); // eat the "*" char
            char prev = ' ';
            int c;
            while ((c = _reader.Read()) != -1)
            {
                ch = (char)c;
                if (ch == '/' && prev == '*')
                    return;
                prev = ch;
            }
            // If we get here we didn't reach the end of the comment
            throw new ParseException("Unterminated multiline comment");
        }

        /// <summary>
        /// Reads a single line comment // comment
        /// </summary>
        /// <param name="ch">the starting character</param>
        private void ReadLineComment(char ch)
        {
            _reader.Read(); // eat the 2nd "/" char
            int c;
            // read until the end of the line
            while ((c = _reader.Read()) != -1)
            {
                ch = (char)c;
                if (ch == '\r' && _reader.Peek() == '\n') {
                    _reader.Read();
                    return;
                } else if (ch == '\n') {
                    return;
                }
            }
        }

        /// <summary>
        /// Parses a symbol from the reader such as "," "." etc
        /// </summary>
        /// <param name="ch">the starting character</param>
        /// <param name="buffer">a buffer to store input</param>
        /// <returns>symbol token</returns>
        private static Token GetSymbol(char ch)
        {
            // we don't have any symbols at the moment that are more than one character
            // so we can just return any symbols
            return new Token(TokenType.Symbol, ch.ToString());
        }

        /// <summary>
        /// Gets an identifier from the reader such as a variable reference, null, true, or false.
        /// Follows C# rules, non-qouted string starting with a letter or "_" followed by letters digits or "_"
        /// </summary>
        /// <param name="start">the starting character</param>
        /// <param name="buffer">a buffer to hold input</param>
        /// <returns>identifier token</returns>
        private Token GetIdentifier(char start, StringBuilder buffer)
        {

            buffer.Append(start);
            int c;
            char ch;
            while ((c = _reader.Peek()) != -1)
            {
                ch = (char)c;
                if (char.IsLetterOrDigit(ch) || ch == '_')
                {
                    buffer.Append(ch);
                }
                else
                {
                    return new Token(TokenType.Identifier, buffer.ToString());
                }
                _reader.Read();
            }
            return new Token(TokenType.Identifier, buffer.ToString());
        }

        /// <summary>
        /// Gets a number from the reader, which can be integer, floating point or scientific notation
        /// Examples: 123343, -123232, 12.345, -45.3434, 3.45E+10
        /// </summary>
        /// <param name="start">the starting character</param>
        /// <param name="buffer">buffer to hold input</param>
        /// <returns>number token</returns>
        private Token GetNumber(char start, StringBuilder buffer)
        {
            char ch = start;
            buffer.Append(ch);
            int i = (start == '.') ? 1 : 0;

            while (i < 3)
            {
                switch (i)
                {
                    case 0: // first part of integer
                        GetIntegerPart(buffer);
                        ch = (char)_reader.Peek();
                        if (ch == '.')
                        {
                            i=1;  // try to read fractional now
                            buffer.Append((char)_reader.Read());
                        }
                        else if (ch == 'e' || ch == 'E')
                        {
                            i = 2; // try to read exponent now
                            buffer.Append((char)_reader.Read());
                        }
                        else
                        {
                            i = 4;  //break out
                            break;
                        }
                        break;
                    case 1: // fractional part
                        GetIntegerPart(buffer);
                        ch = (char)_reader.Peek();
                        if (ch == '.')
                        {
                            throw new ParseException("Invalid number exception");
                        }
                        else if (ch == 'e' || ch == 'E')
                        {
                            i = 2; // read exponent
                            buffer.Append((char)_reader.Read());
                        }
                        else
                        {
                            i = 3; // break out
                        }
                        break;
                    case 2: // scientific notation
                        ch = (char)_reader.Peek();
                        //check for an optional sign
                        if (ch == '+' || ch == '-')
                        {
                            buffer.Append((char)_reader.Read());
                        }
                        GetIntegerPart(buffer);
                        ch = (char)_reader.Peek();
                        if (ch == '.')
                        {
                            throw new ParseException("Invalid number exception");
                        }
                        else
                        {
                            i = 3; // break out
                        }
                        break;
                }
            }
            return new Token(TokenType.Number, buffer.ToString());
        }

        /// <summary>
        /// Gets an integer portion of a number, stopping at a "." or the start of an exponent "e" or "E"
        /// </summary>
        /// <param name="buffer">buffer to store input</param>
        private void GetIntegerPart(StringBuilder buffer)
        {
            int c;
            char ch;
            while ((c = _reader.Peek()) != -1)
            {
                ch = (char)c;
                if (char.IsNumber(ch))
                {
                    buffer.Append(ch);
                }
                else if (ch == '.' || ch == 'e' || ch == 'E' || IsSymbolStart(ch) || char.IsWhiteSpace(ch))
                {
                    break;
                }
                else
                {
                    throw new ParseException("Invalid number, unexpected character: " + ch);
                }
                _reader.Read();
            }
        }

        /// <summary>
        /// Gets a single or double qouted string from the reader, handling and escape characters
        /// </summary>
        /// <param name="start">the starting character</param>
        /// <param name="buffer">buffer for input</param>
        /// <returns>string token</returns>
        private Token GetQuotedString(char start, StringBuilder buffer)
        {
            char quoteChar = start;
            bool escape = false;
            char ch;
            int c;
            while ((c = _reader.Read()) != -1) {
                ch = (char) c;

                if (escape)
                {
                    switch (ch)
                    {
                        case 't': // horizantal tab
                            buffer.Append('\t');
                            break;
                        case 'n': // newline
                            buffer.Append('\n');
                            break;
                        case '\\': // reverse solidus
                            buffer.Append('\\');
                            break;
                        case '/':  // solidus
                            buffer.Append('/');
                            break;
                        case 'b':  // backspace
                            buffer.Append('\b');
                            break;
                        case 'f':  // formfeed
                            buffer.Append('\f');
                            break;
                        case 'r': // carriage return
                            buffer.Append('\r');
                            break;
                        case 'u': // unicode escape sequence \unnnn
                            {
                                char[] ucodeChar = new char[4];
                                int nRead = _reader.Read(ucodeChar, 0, 4);
                                if (nRead != 4)
                                    throw new ParseException("Invalid unicode escape sequence, expecting \"\\unnnn\", but got " + (new string(ucodeChar, 0, nRead)));
                                buffer.Append((char)uint.Parse(new string(ucodeChar), System.Globalization.NumberStyles.HexNumber));
                            }
                            break;
                        default:
                            buffer.Append(ch);
                            break;
                    }
                    escape = false;
                }
                else
                {
                    if (ch == '\\')
                    {
                        escape = true;
                    }
                    else if (ch == quoteChar)
                    {
                        return new Token(quoteChar == '"' ? TokenType.DoubleQuotedString : TokenType.SingleQuotedString, buffer.ToString());
                        buffer.Length = 0;
                    }
                    else
                    {
                        buffer.Append(ch);
                    }
                }
            }
            throw new ParseException("Unterminated string constant");
        }

        #endregion

        #region Token Predicates

        /// <summary>
        /// Is the character a starting quote character
        /// </summary>
        /// <param name="ch">character to test</param>
        /// <returns>true if quote start</returns>
        private static bool IsQuoteStart(char ch)
        {
            return ch == '\'' || ch == '"';
        }

        /// <summary>
        /// Is the character the start of a number
        /// </summary>
        /// <param name="ch">character to test</param>
        /// <returns>true if number start</returns>
        private bool IsNumberStart(char ch)
        {
            if (ch == '.' && char.IsDigit((char)_reader.Peek()))
                return true;
            else
                return ch == '+' || ch == '-' || char.IsDigit(ch);
        }

        /// <summary>
        /// Is the character the start of an identifier
        /// </summary>
        /// <param name="ch">character to test</param>
        /// <returns>true if identifier start</returns>
        private static bool IsIdentifierStart(char ch)
        {
            return char.IsLetter(ch) || ch == '_';
        }

        /// <summary>
        /// Is the character the start of a symbol
        /// </summary>
        /// <param name="ch">character to test</param>
        /// <returns>true if symbol start</returns>
        private bool IsSymbolStart(char ch)
        {
            return Array.BinarySearch<char>(_symbols, ch) != -1;
        }

        /// <summary>
        /// Is the character the start of a single line comment
        /// </summary>
        /// <param name="ch">character to start</param>
        /// <returns>true if single line comment start</returns>
        private bool IsLineCommentStart(char ch)
        {
            return (ch == '/' && _reader.Peek() == '/');
        }

        /// <summary>
        /// Is the character the start of a multiline comment
        /// </summary>
        /// <param name="ch">character to test</param>
        /// <returns>true if multiline start</returns>
        private bool IsMultilineCommentStart(char ch)
        {
            return (ch == '/' && _reader.Peek() == '*');
        }

        #endregion
    }
}
