/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Runtime.InteropServices;
using Naizari.Helpers;

namespace Naizari.Extensions
{
    [Flags]
    public enum ConsoleTextColor
    {
        Black = 0x0000,
        Blue = 0x0001,
        Green = 0x0002,
        Cyan = 0x0003,
        Red = 0x0004,
        Magenta = 0x0005,
        Yellow = 0x0006,
        Grey = 0x0007,
        White = 0x0008
    }

    public static class ConsoleExtensions
    {
        const int STD_INPUT_HANDLE = -10;
        const int STD_OUTPUT_HANDLE = -11;
        const int STD_ERROR_HANDLE = -12;

        [DllImportAttribute("Kernel32.dll")]
        private static extern IntPtr GetStdHandle
        (
            int nStdHandle // input, output, or error device

        );

        [DllImportAttribute("Kernel32.dll")]
        private static extern bool SetConsoleTextAttribute
        (
            IntPtr hConsoleOutput, // handle to screen buffer

            int wAttributes    // text and background colors

        );

        public static bool SetTextColor()
        {
            return SetTextColor(ConsoleTextColor.Grey);
        }

        public static bool SetTextColor(ConsoleTextColor textColor)
        {
            return SetTextColor(textColor, true);
        }

        public static bool SetTextColor(ConsoleTextColor textColor, bool bright)
        {
            IntPtr nConsole = GetStdHandle(STD_OUTPUT_HANDLE);
            int colorMap;

            if (bright)
            {
                colorMap = (int)textColor | (int)ConsoleTextColor.White;
            }
            else
            {
                colorMap = (int)textColor;
            }

            return SetConsoleTextAttribute(nConsole, colorMap);
        }

        public static void PropertiesToConsole(object target)
        {
            Console.WriteLine(Debug.PropertiesToString(target));
        }
    }
}
