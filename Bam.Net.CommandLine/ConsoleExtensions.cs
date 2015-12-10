/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Runtime.InteropServices;
using System.Reflection;
using System.Text;

namespace Bam.Net.CommandLine
{
    /// <summary>
    /// This class exists because of an initial limitation
    /// requiring all code to be compatible to .net 1.1.
    /// All code has since been converted to take advantage
    /// of the ConsoleColor enumeration.  This class was
    /// left in place to catch any calls that are still
    /// being made to it.
    /// </summary>
    public static class ConsoleExtensions
    {
        /// <summary>
        /// Calls Console.ResetColor()
        /// </summary>
        [Obsolete("This method is obsolete.  Use Console.ResetColor() instead.")]
        public static void SetTextColor()
        {
            Console.ResetColor();
        }

        /// <summary>
        /// This method exists because this toolkit was written 
        /// prior to the introduction of the ConsoleColor class
        /// and this was originally using PInvoke style console
        /// color setting.  This method has been updated to use
        /// ConsoleColor and left in place so all existing 
        /// calls to this method don't break.
        /// </summary>
        /// <param name="textColor"></param>
        [Obsolete("This method is obsolete.  Use Console.ForegroundColor property instead.")]
        public static void SetTextColor(ConsoleColor textColor)
        {
            Console.ForegroundColor = textColor;
        }

        public static void SetConsoleColors(ConsoleColorCombo combo)
        {
            Console.ForegroundColor = combo.ForegroundColor;
            Console.BackgroundColor = combo.BackgroundColor;
        }

        public static void PropertiesToConsole(object target)
        {
            Console.WriteLine(target.GetType().PropertiesToString("\r\n"));
        }

        
    }
}
