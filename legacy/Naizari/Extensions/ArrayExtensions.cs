/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Naizari.Helpers;

namespace Naizari.Extensions
{
    public static class ArrayExtensions
    {
        public delegate void ArrayExtensionDelegate<T>(T[] arr);

        public static T RandomElement<T>(T[] array)
        {
            Random r = new Random();
            return array[r.Next(array.Length - 1)];
        }

        public static T[] ThrowIfEmpty<T>(this T[] array)
        {
            if (array.Length == 0)
                ExceptionHelper.Throw<ArgumentException>("The array was empty");
            return array;
        }

        public static T[] HasOrDie<T>(this T[] array, int count)
        {
            if(array.Length != count)
                ExceptionHelper.Throw<ArgumentException>("Expected {0} but has {1}", count, array.Length);
            return array;
        }

        public static T[] HasLessThanOrDie<T>(this T[] array, int count)
        {
            if (array.Length >= count)
                ExceptionHelper.Throw<ArgumentException>("Expected less than {0} but has {1}", count, array.Length);
            return array;
        }

        public static T[] HasMoreThanOrDie<T>(this T[] array, int count)
        {
            if (array.Length <= count)
                ExceptionHelper.Throw<ArgumentException>("Expected more than {0} but has {1}", count, array.Length);
            return array;
        }
    }
}
