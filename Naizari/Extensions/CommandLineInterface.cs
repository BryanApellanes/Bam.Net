/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.IO;
using Naizari.Helpers;

namespace Naizari.Extensions
{
    [Serializable]
    public abstract class CommandLineInterface
    {
        /// <summary>
        /// Represents arguments after parsing with a call to ParseArgs.  Arguments should be 
        /// passed in on the command line in the format /&lt;name&gt;:&lt;value&gt;.
        /// </summary>
        static ParsedArguments arguments;
        protected static ParsedArguments Arguments { get { return arguments; } set { arguments = value; } }
        static List<ArgumentInfo> validArgumentInfo = new List<ArgumentInfo>();
        protected static List<ArgumentInfo> ValidArgumentInfo
        {
            get { return validArgumentInfo; }
            set { validArgumentInfo = value; }
        }

        static bool stackTrace;
        protected static bool StackTrace
        {
            get { return stackTrace; }
            set { stackTrace = value; }
        }
        //protected static ConsoleMenu[] menus;

        protected static List<ConsoleMenu> menus;
        protected static List<ConsoleMenu> Menus
        {
            get
            {
                return menus;
            }
            set
            {
                menus = value;
            }
        }
        /// <summary>
        /// Event fired after command line arguments are parsed by a call to ParseArgs.
        /// </summary>
        protected static ConsoleArgsParsedDelegate ArgsParsed;

        /// <summary>
        /// Event fired after command line arguments are parsed by a call to ParseArgs
        /// and there was an error.
        /// </summary>
        protected static ConsoleArgsParsedDelegate ArgsParsedError;

        static CommandLineInterface()
        {
            ValidArgumentInfo = new List<ArgumentInfo>();
            AddValidArgument("stacktrace", true);
        }

        /// <summary>
        /// Prompts the user for [y]es or [n]o returning true for [y] and false for [n].
        /// </summary>
        /// <returns>boolean</returns>
        public static bool Confirm()
        {
            return Confirm("Continue? [y][N]");
        }

        /// <summary>
        /// Prompts the user for [y]es or [n]o returning true for [y] and false for [n].
        /// </summary>
        /// <param name="message">Optional message for the user.</param>
        /// <returns></returns>
        public static bool Confirm(string message)
        {
            return Confirm(message, true);
        }

        public static bool Confirm(string message, bool allowQuit)
        {
            return Confirm(message, ConsoleTextColor.White, allowQuit);
        }

        /// <summary>
        /// Prompts the user for [y]es or [n]o returning true for [y] and false for [n].
        /// </summary>
        /// <param name="message">Optional message for the user.</param>
        /// <param name="allowQuit">If true provides an additional [q]uit option which if selected
        /// will call Environment.Exit(0).</param>
        /// <returns>boolean</returns>
        public static bool Confirm(string message, ConsoleTextColor color, bool allowQuit)
        {
            Out(message, color);
            if(allowQuit)
                Console.WriteLine(" [q]");
            else
                Console.WriteLine();

            string answer = Console.ReadLine().Trim().ToLower();
            if (answer.Equals("y"))
                return true;

            if ((answer.Equals("n")))
                return false;

            if(allowQuit && answer.Equals("q"))
                Environment.Exit(0);

            return false;
        }

        public static string Prompt(string message)
        {
            return Prompt(message, ConsoleTextColor.Cyan);
        }

        /// <summary>
        /// Prompts the user for input.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns>string</returns>
        public static string Prompt(string message, ConsoleTextColor textColor)
        {
            return Prompt(message, textColor, false);
        }

        public static string Prompt(string message, ConsoleTextColor textColor, bool allowQuit)
        {
            Out(message, textColor);
            Console.Write(" >> ");
            string answer = Console.ReadLine();
            //answer = answer.Substring(4, answer.Length - 4);
            if (allowQuit && answer.ToLowerInvariant().Equals("q"))
                Environment.Exit(0);

            return answer;
        }

        public static void Exit()
        {
            Exit(0);
        }

        public static void Exit(int code)
        {
            ConsoleExtensions.SetTextColor();
            Environment.Exit(code);
        }

        public static void Usage(Assembly assemblyToAnalyze)
        {
            Assembly thisAssembly = Assembly.GetExecutingAssembly();
            FileInfo info = new FileInfo(thisAssembly.Location);
            Console.WriteLine("{0} /[a | action]:<action name>", info.Name);

            Console.WriteLine();
        }

        protected static void AddMenu(Assembly assemblyToAnalyze, string name, char option, ConsoleMenuDelegate menuDelegate)
        {
            AddMenu(assemblyToAnalyze, name, option, menuDelegate, name);
        }

        protected static void AddMenu(Assembly assemblyToAnalyze, string name, char option, ConsoleMenuDelegate menuDelegate, string header)
        {
            if (Menus == null)
                Menus = new List<ConsoleMenu>();

            ConsoleMenu menu = new ConsoleMenu();
            menu.HeaderText = header;
            menu.MenuKey = option;
            menu.Menu = menuDelegate;
            menu.Name = name;
            menu.AssemblyToAnalyze = assemblyToAnalyze;
            AddMenu(menu);
        }

        protected static void StartMainMenu()
        {
            AddMenu(Assembly.GetCallingAssembly(), "Main Menu", 'm', new ConsoleMenuDelegate(MainMenu), "Select an option below:");
            MainMenu(Assembly.GetCallingAssembly(), Menus.ToArray(), "Select an option below:");
        }

        protected static void AddMenu(ConsoleMenu menu)
        {
            Menus.Add(menu);
        }
        
        protected static void MainMenu(Assembly assemblyToAnalyze, ConsoleMenu[] otherMenus, string headerText)
        {
            List<ConsoleInvokeableMethod> actions = GetActions(assemblyToAnalyze);

            Console.WriteLine(headerText);
            Console.WriteLine();

            ShowMenu(actions);
            Console.WriteLine();
            Console.Write("| Q -> quit | ");

            string answer = ShowSelectedMenuOrReturnAnswer(otherMenus);

            try
            {
                InvokeSelection(actions, answer);
            }
            catch (Exception ex)
            {
                Out("An error occurred: " + ex.Message, ConsoleTextColor.Red);
                if (StackTrace)
                {
                    if (ex.InnerException != null)
                    {
                        ex = ex.InnerException;
                    }

                    Out(ex.StackTrace, ConsoleTextColor.Red);
                }
            }
            if (Confirm("Return to the main menu? [y][N]"))
                MainMenu(assemblyToAnalyze, otherMenus, headerText);
        }

        protected static string ShowSelectedMenuOrReturnAnswer(ConsoleMenu[] otherMenus)
        {
            //Console.WriteLine();
            WriteOtherMenuOptions(otherMenus);
            string answer = Console.ReadLine();
            Console.WriteLine();
            if (answer.Trim().ToLower().Equals("q"))
                Environment.Exit(0);

            ShowSelectedMenu(otherMenus, answer);
            return answer;
        }

        private static void WriteOtherMenuOptions(ConsoleMenu[] otherMenus)
        {
            if (otherMenus != null)
            {
                foreach (ConsoleMenu menu in otherMenus)
                {
                    Console.Write(menu.MenuKey + " -> " + menu.Name + " | ");
                }
            }
        }

        private static void ShowSelectedMenu(ConsoleMenu[] otherMenus, string answer)
        {
            if (otherMenus != null)
            {
                foreach (ConsoleMenu menu in otherMenus)
                {
                    if (menu.MenuKey.ToString().ToLower().Equals(answer.Trim().ToLower()))
                        menu.Menu(menu.AssemblyToAnalyze, otherMenus, menu.HeaderText);//menu.Menu.Method.Invoke(menu.AssemblyToAnalyze, otherMenus, menu.HeaderText);
                }
            }
        }

        public static void Out()
        {
            Console.WriteLine();
        }

        public static void OutFormat(string message, params object[] formatArgs)
        {
            Out(string.Format(message, formatArgs));
        }

        public static void OutFormat(string message, ConsoleTextColor color, params object[] formatArgs)
        {
            Out(string.Format(message, formatArgs), color);
        }

        public static void Out(string message)
        {
            Out(message, ConsoleTextColor.Grey);
        }

        public static void Out(string message, ConsoleTextColor color)
        {
            ConsoleExtensions.SetTextColor(color);
            Console.WriteLine(message);
            ConsoleExtensions.SetTextColor();
        }

        public static void InvokeSelection(List<ConsoleInvokeableMethod> actions, string answer)
        {
            InvokeSelection(actions, answer, "", "");
        }

        protected static void InvokeSelection(List<ConsoleInvokeableMethod> actions, string answer, string header, string footer)
        {
            int ignore;
            InvokeSelection(actions, answer, header, footer, out ignore);
        }

        static MethodInfo invoking;
        static object invokeOn;
        static object[] parameters;
        private static void InvokeMethod()
        {
            if (invoking == null)
            {
                invoking = (MethodInfo)AppDomain.CurrentDomain.GetData("Method");
            }

            if (invoking != null)
            {
                object inst = invokeOn == null ? AppDomain.CurrentDomain.GetData("Instance") : invokeOn;
                object[] parms = parameters == null ? (object[])AppDomain.CurrentDomain.GetData("Parameters") : parameters;
                invoking.Invoke(inst, parms);
            }
        }

        protected static void InvokeInSeparateAppDomain(MethodInfo method, object instance, object[] ps = null)
        {
            AppDomain testDomain = AppDomain.CreateDomain("TestAppDomain");
            invoking = method;
            invokeOn = instance;
            parameters = ps;

            testDomain.SetData("Method", method);
            testDomain.SetData("Instance", instance);
            testDomain.SetData("Parameters", parameters);
            testDomain.DoCallBack(InvokeMethod);
        }

        protected static void InvokeSelection(List<ConsoleInvokeableMethod> actions, string answer, string header, string footer, out int selectedIndex)
        {
            selectedIndex = -1;
            if (int.TryParse(answer.ToString(), out selectedIndex) && (selectedIndex - 1) > -1 && (selectedIndex - 1) < actions.Count)
            {
                selectedIndex -= 1;
                MethodInfo method = actions[selectedIndex].Method;
                object[] parameters = GetParameterInput(method);
                if (!string.IsNullOrEmpty(header))
                    Out(header, ConsoleTextColor.White);
                try
                {
                    if (method.IsStatic)
                    {
                        InvokeInSeparateAppDomain(method, null, parameters);
                        //method.Invoke(null, parameters);
                    }
                    else
                    {
                        ConstructorInfo ctor = method.DeclaringType.GetConstructor(Type.EmptyTypes);
                        if (ctor == null)
                            ExceptionHelper.Throw<InvalidOperationException>("Specified non-static method is declared on a type that has no parameterless constructor. {0}.{1}", method.DeclaringType.Name, method.Name);

                        InvokeInSeparateAppDomain(method, ctor.Invoke(null), parameters);
                        //method.Invoke(ctor.Invoke(null), parameters);
                    }
                }
                catch (Exception ex)
                {
                    if (ex.InnerException != null)
                        throw ex.InnerException;
                    else
                        throw;
                }
                if (!string.IsNullOrEmpty(footer))
                    Out(footer, ConsoleTextColor.White);
            }
            else
            {
                Console.WriteLine("Invalid selection");
                Environment.Exit(1);
            }
        }

        protected static void ShowMenu(List<ConsoleInvokeableMethod> actions)
        {
            for (int i = 1; i <= actions.Count; i++)
            {
                ConsoleInvokeableMethod consoleMethod = actions[i - 1];
                string menuOption = consoleMethod.Information;
                Console.WriteLine("{0}. {1}", i, menuOption);
            }
           
        }

        protected static List<ConsoleInvokeableMethod> GetActions(Assembly assemblyToAnalyze)
        {
            return GetActions<ConsoleAction>(assemblyToAnalyze);
        }

        protected static List<ConsoleInvokeableMethod> GetActions<TAttribute>(Assembly assemblyToAnalyze) where TAttribute : Attribute, new()
        {
            return GetActions(assemblyToAnalyze, typeof(TAttribute));
        }

        protected static List<ConsoleInvokeableMethod> GetActions(Assembly assemblyToAnalyze, Type attrType)
        {
            List<ConsoleInvokeableMethod> actions = new List<ConsoleInvokeableMethod>();
            Type[] types = assemblyToAnalyze.GetTypes();
            foreach (Type type in types)
            {
                MethodInfo[] methods = type.GetMethods();
                foreach (MethodInfo method in methods)
                {
                    object action = null;
                    if (method.HasCustomAttributeOfType(attrType, out action)) //HasCustomAttributeOfType(method, out action))
                    {
                        actions.Add(new ConsoleInvokeableMethod(method, (Attribute)action));
                    }
                }
            }
            return actions;
        }

        protected static char Pause()
        {
            return Pause(string.Empty);
        }

        protected static char Pause(string message)
        {
            if (!string.IsNullOrEmpty(message))
                Console.WriteLine(message);

            ConsoleKeyInfo keyInfo = Console.ReadKey();
            if (keyInfo.Key == ConsoleKey.Q)
                Exit(0);

            return keyInfo.KeyChar;
        }

        protected static object[] GetParameterInput(MethodInfo method)
        {
            return GetParameterInput(method, false);
        }

        protected static object[] GetParameterInput(MethodInfo method, bool generate)
        {
            ParameterInfo[] parameterInfos = method.GetParameters();
            List<object> parameterValues = new List<object>(parameterInfos.Length);
            foreach (ParameterInfo parameterInfo in parameterInfos)
            {
                if (parameterInfo.ParameterType != typeof(string))
                {
                    Out(string.Format("The method {0} can't be invoked because it takes parameters that are not of type string.", method.Name)
                        , ConsoleTextColor.Red);                    
                }

                if (generate)
                    parameterValues.Add(StringExtensions.RandomString(5));
                else
                    parameterValues.Add(GetInput(parameterInfo));
            }
            return parameterValues.ToArray();
        }

        private static string GetInput(ParameterInfo parameter)
        {
            Console.WriteLine("{0}: ", parameter.Name);
            return Console.ReadLine();
        }

        protected static bool HasCustomAttributeOfType<T>(MethodInfo method, out T attribute) where T: Attribute, new()
        {
            return CustomAttributeExtension.HasCustomAttributeOfType<T>(method, out attribute);
        }

        /// <summary>
        /// Makes the specified name a valid command line argument.  Command line
        /// arguments are assumed to be in the format /&lt;name&gt;:&lt;value&gt;.
        /// </summary>
        /// <param name="name"></param>
        public static void AddValidArgument(string name)
        {
            AddValidArgument(name, false);
        }

        /// <summary>
        /// Makes the specified name a valid command line argument.  Command line
        /// arguments are assumed to be in the format /&lt;name&gt;:&lt;value&gt;.
        /// </summary>
        /// <param name="name">The name of the command line argument.</param>
        /// <param name="allowNull">If true no value for the specified name is necessary.</param>
        public static void AddValidArgument(string name, bool allowNull)
        {
            ValidArgumentInfo.Add(new ArgumentInfo(name, allowNull));
        }

        protected static void ParseArgs(string[] args)
        {
            Arguments = new ParsedArguments(args, ValidArgumentInfo.ToArray());
            StackTrace = Arguments.Contains("stacktrace");
            if (Arguments.Status == ArgumentParseStatus.Error || Arguments.Status == ArgumentParseStatus.Invalid)
            {
                if (ArgsParsedError != null)
                    ArgsParsedError(Arguments);
            }
            else if (Arguments.Status == ArgumentParseStatus.Success)
            {
                if (ArgsParsed != null)
                    ArgsParsed(Arguments);
            }
        }

    }
}
