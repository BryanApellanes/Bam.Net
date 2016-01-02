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

namespace Naizari.Javascript.JsonExSerialization.TypeConversion
{
    /// <summary>
    /// Converter for the System.Collections.BitArray type
    /// </summary>
    public class BitArrayConverter : IJsonTypeConverter
    {

        #region IJsonTypeConverter Members
        
        /// converts a bit array into a string of the format:  "63,FFEF10002EFA"
        /// length,HexEncodedBits
        public object ConvertFrom(object item, SerializationContext serializationContext)
        {
            BitArray ba = (BitArray) item;
            int len = ba.Count;
            // set up our bits as int array
            int[] bits = new int[((len - 1) >> 5) + 1];

            // copy the bool values to a temp array for iteration
            bool[] bitBools = new bool[len];
            ba.CopyTo(bitBools, 0);

            // convert to int array
            for (int i = 0; i < len; i++)
            {
                if (bitBools[i])
                {
                    bits[i >> 5] |= 1 << i;
                }
            }

            // write as Hex values
            StringBuilder sb = new StringBuilder(4 * bits.Length);
            for (int i = bits.Length - 1; i >= 0; i--)
            {
                string formatted = bits[i].ToString("X");
                if (bits[i] < 4096) // minimum value that is 4-digits
                {
                    sb.Append(formatted.PadLeft(4, '0'));
                }
                else
                {
                    sb.Append(formatted);
                }
            }

            Hashtable result = new Hashtable();
            result["Count"] = len;
            result["Bits"] = sb.ToString();
            return result;
        }

        public object ConvertTo(object item, Type sourceType, SerializationContext serializationContext)
        {
            string bits = (string)((Hashtable)item)["Bits"];
            int count = (int)((Hashtable)item)["Count"];
            // >> 5 == Fast integer division by 32
            int intLength = ((count - 1) >> 5) + 1;
            BitArray result = new BitArray(count);
            for (int i = 0, j = intLength - 1; i + 3 < bits.Length && j >= 0; i += 4, j--)
            {
                string temp = bits.Substring(i, 4);
                int value = int.Parse(temp, System.Globalization.NumberStyles.AllowHexSpecifier);
                int end = Math.Min(32, count - (j << 5));
                for (int k = 0; k < end; k++)
                {
                    result[j << 5 | k] = (value & (1 << k)) != 0;
                }
            }
            return result;
        }

        public object Context
        {
            set { return; }
        }

        #endregion

        #region IJsonTypeConverter Members

        public Type GetSerializedType(Type sourceType)
        {
            return typeof(Hashtable);
        }

        #endregion

        #region IJsonTypeConverter Members


        public SerializationContext SerializationContext
        {
            set { return; }
        }

        #endregion
    }
}
