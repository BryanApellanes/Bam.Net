/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.IO;
using Bam.Net;
using System.Diagnostics;
using Bam.Net.Logging;
using Bam.Net.Configuration;
using System.Threading;

namespace Bam.Net.CommandLine
{
    [Serializable]
    public abstract class CommandLineInterface : MarshalByRefObject
    {
        static event ExitDelegate Exiting;
        static event ExitDelegate Exited;

        static ParsedArguments arguments;

        static CommandLineInterface()
        {
            IsolateMethodCalls = true;
            ValidArgumentInfo = new List<ArgumentInfo>();
        }

        /// <summary>
        /// Get the value specified for the argument with the 
        /// specified name either from the command line or
        /// from the default configuration file or prompt for
        /// it if the value was not found
        /// </summary>
        /// <param name="name"></param>
        /// <param name="promptMessage"></param>
        /// <returns></returns>
        public static string GetArgument(string name, string promptMessage = null)
        {
            string acronym = name.CaseAcronym().ToLowerInvariant();
            string fromConfig = DefaultConfiguration.GetAppSetting(name, "").Or(DefaultConfiguration.GetAppSetting(acronym, ""));
            return Arguments.Contains(name) ? Arguments[name] :
                Arguments.Contains(acronym) ? Arguments[acronym] :
                !string.IsNullOrEmpty(fromConfig) ? fromConfig :
                Prompt(promptMessage ?? $"Please enter a value for {name}");
        }

        /// <summary>
        /// Represents arguments after parsing with a call to ParseArgs.  Arguments should be 
        /// passed in on the command line in the format /&lt;name&gt;:&lt;value&gt;.
        /// </summary>
        public static ParsedArguments Arguments { get { return arguments; } set { arguments = value; } }

        static List<ArgumentInfo> validArgumentInfo = new List<ArgumentInfo>();

        protected static List<ArgumentInfo> ValidArgumentInfo
        {
            get { return validArgumentInfo; }
            set { validArgumentInfo = value; }
        }

        protected static List<ConsoleMenu> otherMenus;
        protected static List<ConsoleMenu> OtherMenus
        {
            get
            {
                return otherMenus;
            }
            set
            {
                otherMenus = value;
            }
        }

        /// <summary>
        /// If false a prompt to confirm to the last menu will be presented
        /// after every selection, if true the last menu will be presented
        /// automatically
        /// </summary>
        protected static bool AutoReturn
        {
            get;
            set;
        }

        /// <summary>
        /// Event fired after command line arguments are parsed by a call to ParseArgs.
        /// </summary>
        protected static event ConsoleArgsParsedDelegate ArgsParsed;

        /// <summary>
        /// Event fired after command line arguments are parsed by a call to ParseArgs
        /// and there was an error.
        /// </summary>
        protected static event ConsoleArgsParsedDelegate ArgsParsedError;


        /// <summary>
        /// Checks if the owner of the current process has admin rights,
        /// if not the original command line is rebuilt and run with 
        /// the runas verb set on the startinfo.  The current
        /// process will exit.
        /// </summary>
        public static void EnsureAdminRights()
        {
            if (!WeHaveAdminRights())
            {
                Elevate();
            }
        }

        /// <summary>
        /// Determines if the current process is being run by a user with administrative 
        /// rights
        /// </summary>
        /// <returns></returns>
        public static bool WeHaveAdminRights()
        {
            return UserUtil.CurrentWindowsUserHasAdminRights();
        }

        /// <summary>
        /// Runs the current process again, prompting for admin rights
        /// </summary>
        public static void Elevate()
        {
            Process current = Process.GetCurrentProcess();
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.Verb = "runas";
            startInfo.FileName = current.MainModule.FileName;
            StringBuilder arguments = new StringBuilder();
            Environment.GetCommandLineArgs().Rest(1, arg =>
            {
                arguments.Append(arg);
                arguments.Append(" ");
            });
            startInfo.Arguments = arguments.ToString();
            Process.Start(startInfo);
            Environment.Exit(0);
        }

        public static bool ConfirmFormat(string format, params object[] args)
        {
            return Confirm(string.Format(format, args));
        }

        public static bool ConfirmFormat(string format, ConsoleColor color, params object[] args)
        {
            return Confirm(string.Format(format, args), color);
        }

        public static bool ConfirmFormat(string format, ConsoleColor color, bool allowQuit, params object[] args)
        {
            return Confirm(string.Format(format, args), color, allowQuit);
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

        public static bool Confirm(string message, ConsoleColor color)
        {
            return Confirm(message, color, true);
        }
        public static bool Confirm(string message, bool allowQuit)
        {
            return Confirm(message, ConsoleColor.White, allowQuit);
        }

        /// <summary>
        /// Prompts the user for [y]es or [n]o returning true for [y] and false for [n].
        /// </summary>
        /// <param name="message">Optional message for the user.</param>
        /// <param name="allowQuit">If true provides an additional [q]uit option which if selected
        /// will call Environment.Exit(0).</param>
        /// <returns>boolean</returns>
        public static bool Confirm(string message, ConsoleColor color, bool allowQuit)
        {
            Out(message, color);
            if (allowQuit)
            {
                Console.WriteLine(" [q]");
            }
            else
            {
                Console.WriteLine();
            }

            string answer = Console.ReadLine().Trim().ToLower();
            if (answer.Equals("y"))
            {
                return true;
            }

            if (answer.Equals("n"))
            {
                return false;
            }

            if (allowQuit && answer.Equals("q"))
            {
                Environment.Exit(0);
            }

            return false;
        }

        public static int NumberPrompt(string message, ConsoleColor color = ConsoleColor.Cyan)
        {
            return IntPrompt(message, color);
        }

        public static long LongPrompt(string message, ConsoleColor color = ConsoleColor.Cyan)
        {
            string value = Prompt(message, color);
            long result = -1;
            long.TryParse(value, out result);
            return result;
        }

        public static int IntPrompt(string message, ConsoleColor color = ConsoleColor.Cyan)
        {
            string value = Prompt(message, color);
            int result = -1;
            int.TryParse(value, out result);
            return result;
        }

        public static string[] ArrayPrompt(string message, params string[] quitters)
        {
            return ArrayPrompt(message, (IEnumerable<string>)quitters);
        }

        public static string[] ArrayPrompt(string message, IEnumerable<string> quitters)
        {
            List<string> results = new List<string>();
            string entry = string.Empty;
            do
            {
                entry = Prompt(message);
                if (!quitters.Contains(entry) && !results.Contains(entry) && !string.IsNullOrEmpty(entry))
                {
                    results.Add(entry);
                }
            } while (!quitters.Contains(entry));

            return results.ToArray();
        }

        public static string Prompt(string message)
        {
            return Prompt(message, ConsoleColor.Cyan);
        }

        /// <summary>
        /// Prompts the user for input.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns>string</returns>
        public static string Prompt(string message, ConsoleColor textColor)
        {
            return Prompt(message, textColor, false);
        }

        public static string Prompt(string message, ConsoleColor textColor, bool allowQuit)
        {
            return Prompt(message, " >> ", textColor, allowQuit);
        }

        public static string Prompt(string message, string promptTxt, ConsoleColor textColor)
        {
            return Prompt(message, promptTxt, textColor, false);
        }

        public static string Prompt(string message, string promptTxt, ConsoleColor textColor, bool allowQuit)
        {
            return Prompt(message, promptTxt, new ConsoleColorCombo(textColor), allowQuit);
        }

        public static string Prompt(string message, string promptTxt, ConsoleColor textColor, ConsoleColor backgroundColor)
        {
            return Prompt(message, promptTxt, new ConsoleColorCombo(textColor, backgroundColor), false);
        }

        public static string Prompt(string message, string promptTxt, ConsoleColor textColor, ConsoleColor backgroundColor, bool allowQuit)
        {
            return Prompt(message, promptTxt, new ConsoleColorCombo(textColor, backgroundColor), allowQuit);
        }

        public static string Prompt(string message, string promptTxt, ConsoleColorCombo colors, bool allowQuit)
        {
            return PromptProvider(message, promptTxt, colors, allowQuit);
        }

        static Func<string, string, ConsoleColorCombo, bool, string> _promptProvider;
        public static Func<string, string, ConsoleColorCombo, bool, string> PromptProvider
        {
            get
            {
                if (_promptProvider == null)
                {
                    _promptProvider = (message, promptTxt, colors, allowQuit) =>
                    {
                        Out(message, colors);
                        Console.Write(promptTxt);
                        string answer = Console.ReadLine();
                        //answer = answer.TruncateFront(message.Length + promptTxt.Length);

                        if (allowQuit && answer.ToLowerInvariant().Equals("q"))
                        {
                            Environment.Exit(0);
                        }

                        return answer;
                    };
                }

                return _promptProvider;
            }
            set
            {
                _promptProvider = value;
            }
        }

        public static void Clear()
        {
            Console.Clear();
        }

        public static void Exit()
        {
            Exit(0);
        }

        public static void Exit(int code)
        {
            ConsoleExtensions.SetTextColor();
            OnExiting(code);
            Environment.Exit(code);
            OnExited(code);
        }

        private static void OnExiting(int code)
        {
            if (Exiting != null)
            {
                Exiting(code);
            }
        }

        private static void OnExited(int code)
        {
            if (Exited != null)
            {
                Exited(code);
            }
        }

        public static void Usage(Assembly assembly)
        {
            string assemblyVersion = assembly.GetName().Version.ToString();
            string fileVersion = FileVersionInfo.GetVersionInfo(assembly.Location).FileVersion;
            string usageFormat = @"Assembly Version: {0}
File Version: {1}

{2} [arguments]";
            FileInfo info = new FileInfo(assembly.Location);
            OutLineFormat(usageFormat, assemblyVersion, fileVersion, info.Name);
            Thread.Sleep(3);
            foreach (ArgumentInfo argInfo in ValidArgumentInfo)
            {
                string valueExample = string.IsNullOrEmpty(argInfo.ValueExample) ? string.Empty : string.Format(":{0}\r\n", argInfo.ValueExample);
                OutLineFormat("/{0}{1}\r\n    {2}", argInfo.Name, valueExample, argInfo.Description);
            }
            Thread.Sleep(30);
        }

        protected static void AddMenu(Assembly assemblyToAnalyze, string name, char option, ConsoleMenuDelegate menuDelegate)
        {
            AddMenu(assemblyToAnalyze, name, option, menuDelegate, name);
        }

        protected static void AddMenu(Assembly assemblyToAnalyze, string name, char option, ConsoleMenuDelegate menuDelegate, string header)
        {
            if (OtherMenus == null)
            {
                OtherMenus = new List<ConsoleMenu>();
            }

            ConsoleMenu menu = new ConsoleMenu();
            menu.HeaderText = header;
            menu.MenuKey = option;
            menu.MenuWriter = menuDelegate;
            menu.Name = name;
            menu.AssemblyToAnalyze = assemblyToAnalyze;
            AddMenu(menu);
        }

        protected static void StartMainMenu()
        {
            AddMenu(Assembly.GetCallingAssembly(), "Main Menu", 'm', new ConsoleMenuDelegate(ShowMenu), "Select an option below:");
            ShowMenu(Assembly.GetCallingAssembly(), OtherMenus.ToArray(), "Select an option below:");
        }

        protected static void AddMenu(ConsoleMenu menu)
        {
            OtherMenus.Add(menu);
        }
        
        protected static void ShowMenu(Assembly assemblyToAnalyze, ConsoleMenu[] otherMenus, string headerText)
        {
            List<ConsoleInvokeableMethod> actions = GetConsoleInvokeableMethods<ConsoleAction>(assemblyToAnalyze);
            ShowMenu(otherMenus, headerText, actions);
        }

        protected static void ShowMenu<TAttribute, TType>(string headerText) where TAttribute : Attribute, new()
        {
            List<ConsoleInvokeableMethod> actions = GetConsoleInvokeableMethods<TAttribute, TType>();
            ShowMenu(OtherMenus.ToArray(), headerText, actions);
        }

        protected static void ShowMenu<TAttribute, TType>(ConsoleMenu[] otherMenus, string headerText)
            where TAttribute : Attribute, new()
        {
            List<ConsoleInvokeableMethod> actions = GetConsoleInvokeableMethods<TAttribute, TType>();
            ShowMenu(otherMenus, headerText, actions);
        }

        /// <summary>
        /// Reads all keys in the appSettings section of the default configuration
        /// file and adds them all as valid arguments so that they may be 
        /// specified on the command line.
        /// </summary>
        protected static void AddConfigurationSwitches()
        {
            DefaultConfiguration.GetAppSettings().AllKeys.Each(key =>
            {
                AddValidArgument(key, $"Override value from config: {DefaultConfiguration.GetAppSetting(key)}");
            });
        }

        private static void ShowMenu(ConsoleMenu[] otherMenus, string headerText, List<ConsoleInvokeableMethod> actions)
        {
            Console.WriteLine(headerText);
            Console.WriteLine();

            ShowActions(actions);

            Console.Write("| Q -> quit ");

            string answer = ShowSelectedMenuOrReturnAnswer(otherMenus);

            Console.WriteLine();

            try
            {
                InvokeSelection(actions, answer);
            }
            catch (Exception ex)
            {
                OutLine("An error occurred: " + ex.Message, ConsoleColor.Red);
                if (Arguments.Contains("stacktrace"))
                {
                    if (ex.InnerException != null)
                    {
                        ex = ex.InnerException;
                    }

                    Out(ex.StackTrace, ConsoleColor.Red);
                }
            }

            if (AutoReturn)
            {
                ShowMenu(otherMenus, headerText, actions);
            }
            else if (ConfirmFormat("Return to {0}? [y][N] ", headerText))
            {
                ShowMenu(otherMenus, headerText, actions);
            }
        }

        protected static string ShowSelectedMenuOrReturnAnswer(ConsoleMenu[] otherMenus)
        {
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
                    Console.Write(" | " + menu.MenuKey + " -> " + menu.Name);
                }
                Console.WriteLine();
            }
        }

        private static void ShowSelectedMenu(ConsoleMenu[] otherMenus, string answer)
        {
            if (otherMenus != null)
            {
                foreach (ConsoleMenu menu in otherMenus)
                {
                    if (menu.MenuKey.ToString().ToLower().Equals(answer.Trim().ToLower()))
                    {
                        menu.MenuWriter(menu.AssemblyToAnalyze, otherMenus, menu.HeaderText);//menu.Menu.Method.Invoke(menu.AssemblyToAnalyze, otherMenus, menu.HeaderText);
                    }
                }
                Console.WriteLine();
            }
        }

        public static void OutLine()
        {
            Out();
        }

        static Action _outProvider;
        public static Action OutProvider
        {
            get
            {
                if (_outProvider == null)
                {
                    _outProvider = () =>
                    {
                        Console.WriteLine();
                    };
                }

                return _outProvider;
            }
            set
            {
                _outProvider = value;
            }
        }

        /// <summary>
        /// Writes a newline character to the console using Console.WriteLine()
        /// </summary>
        public static void Out()
        {
            OutProvider();
        }

        public static void OutLineFormat(string message, params object[] formatArgs)
        {
            OutLine(string.Format(message, formatArgs));
        }

        /// <summary>
        /// Print the specified message in the specified
        /// color to the console using the specified string.format
        /// args to format the message.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="color"></param>
        /// <param name="formatArgs"></param>
        public static void OutLineFormat(string message, ConsoleColor color, params object[] formatArgs)
        {
            OutLine(string.Format(message, formatArgs), color);
        }

        /// <summary>
        /// Print the specified message in the specified
        /// colors to the console using the specified string.format
        /// args to format the message.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="foreground"></param>
        /// <param name="background"></param>
        /// <param name="formatArgs"></param>
        public static void OutLineFormat(string message, ConsoleColor foreground, ConsoleColor background, params object[] formatArgs)
        {
            OutLine(string.Format(message, formatArgs), new ConsoleColorCombo(foreground, background));
        }

        public static void OutLineFormat(string message, ConsoleColorCombo colors, params object[] formatArgs)
        {
            OutLine(string.Format(message, formatArgs), colors);
        }
        //
        public static void OutFormat(string message, params object[] formatArgs)
        {
            Out(string.Format(message, formatArgs));
        }

        public static void OutFormat(string message, ConsoleColor color, params object[] formatArgs)
        {
            Out(string.Format(message, formatArgs), color);
        }

        public static void OutFormat(string message, ConsoleColor foreground, ConsoleColor background, params object[] formatArgs)
        {
            Out(string.Format(message, formatArgs), new ConsoleColorCombo(foreground, background));
        }

        public static void OutFormat(string message, ConsoleColorCombo colors, params object[] formatArgs)
        {
            Out(string.Format(message, formatArgs), colors);
        }

        public static void Out(string message)
        {
            Out(message, ConsoleColor.Gray);
        }

        static Action<string, ConsoleColor> _coloredMessageProvider;
        static object _coloredMessageProviderLock = new object();
        static BackgroundThreadQueue<ConsoleMessage> _messageQueue;
        public static Action<string, ConsoleColor> ColoredMessageProvider
        {
            get
            {
                return _coloredMessageProviderLock.DoubleCheckLock(ref _coloredMessageProvider, () =>
                {
                    EnsureQueue();
                    return _coloredMessageProvider = (s, c) =>
                    {
                        _messageQueue.Enqueue(new ConsoleMessage(s, c));
                    };
                });
            }
            set
            {
                _coloredMessageProvider = value;
            }
        }

        static object _queueLock = new object();
        static object _colorLock = new object();
        private static void EnsureQueue()
        {
            _messageQueue = _queueLock.DoubleCheckLock(ref _messageQueue, () =>
            {
                return new BackgroundThreadQueue<ConsoleMessage>((msg) =>
                {
                    lock (_colorLock)
                    {
                        Console.ForegroundColor = msg.Colors.ForegroundColor;
                        Console.BackgroundColor = msg.Colors.BackgroundColor;
                        Console.Write(msg.Text);
                        Console.ResetColor();
                    }
                });
            });
        }

        public static void Out(string message, ConsoleColor color)
        {
            ColoredMessageProvider(message, color);
        }

        static Action<string, ConsoleColorCombo> _colorBackgroundMessageProvider;
        public static Action<string, ConsoleColorCombo> ColoredBackgroundMessageProvider
        {
            get
            {
                if (_colorBackgroundMessageProvider == null)
                {
                    EnsureQueue();
                    _colorBackgroundMessageProvider = (s, c) =>
                    {
                        _messageQueue.Enqueue(new ConsoleMessage(s, c));
                    };
                }
                return _colorBackgroundMessageProvider;
            }
            set
            {
                _colorBackgroundMessageProvider = value;
            }
        }

        public static void Out(string message, ConsoleColorCombo colors)
        {
            ColoredBackgroundMessageProvider(message, colors);
        }

        public static void OutLine(string message)
        {
            OutLine(message, ConsoleColor.Gray);
        }
        
        public static void OutLine(string message, ConsoleColor color)
        {
            Out($"{message}\r\n", color);
        }

        public static void OutLine(string message, ConsoleColor foreground, ConsoleColor background)
        {
            Out($"{message}\r\n", new ConsoleColorCombo(foreground, background));
        }
        public static void OutLine(string message, ConsoleColorCombo colors)
        {
            Out($"{message}\r\n", colors);
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

        static MethodInfo _methodToInvoke;
        static object invokeOn;
        static object[] parameters;
        private static void InvokeMethod()
        {
            if (_methodToInvoke == null)
            {
                _methodToInvoke = (MethodInfo)AppDomain.CurrentDomain.GetData("Method");
            }

            if (_methodToInvoke != null)
            {
                object inst = invokeOn == null ? AppDomain.CurrentDomain.GetData("Instance") : invokeOn;
                object[] parms = parameters == null ? (object[])AppDomain.CurrentDomain.GetData("Parameters") : parameters;
                _methodToInvoke.Invoke(inst, parms);
            }
        }

        protected internal static void InvokeInCurrentAppDomain(MethodInfo method, object instance, object[] ps = null)
        {
            // added this method for consistency with InvokeInSeparateAppDomain method
            _methodToInvoke = method;
            invokeOn = instance;
            parameters = ps;

            InvokeMethod();
        }

        protected internal static void InvokeInSeparateAppDomain(MethodInfo method, object instance, object[] ps = null)
        {
            InvokeInSeparateAppDomain(method, instance, null, ps);
        }
        protected internal static void InvokeInSeparateAppDomain(MethodInfo method, object instance, object state, object[] ps = null)
        {
            AppDomain isolationDomain = AppDomain.CreateDomain("TestAppDomain");
            _methodToInvoke = method;
            invokeOn = instance;
            parameters = ps;

            isolationDomain.SetData("Method", method);
            isolationDomain.SetData("Instance", instance);
            isolationDomain.SetData("Parameters", parameters);
            isolationDomain.SetData("State", state);
            isolationDomain.DoCallBack(InvokeMethod);
            AppDomain.Unload(isolationDomain);
        }

        protected internal static T PopState<T>()
        {
            return (T)AppDomain.CurrentDomain.GetData("State");
        }

        protected internal static void InvokeSelection(List<ConsoleInvokeableMethod> actions, string answer, string header, string footer, out int selectedNumber)
        {
            selectedNumber = -1;
            if (int.TryParse(answer.ToString(), out selectedNumber) && (selectedNumber - 1) > -1 && (selectedNumber - 1) < actions.Count)
            {
                selectedNumber = InvokeSelection(actions, header, footer, selectedNumber);
            }
            else
            {
                Console.WriteLine("Invalid entry");
                Environment.Exit(1);
            }
        }

        /// <summary>
        /// If true will cause all calls to InvokeSelection to be 
        /// run in a separate AppDomain.  This is primarily for 
        /// UnitTest isolation.
        /// </summary>
        protected internal static bool IsolateMethodCalls
        {
            get;
            set;
        }

        protected internal static int InvokeSelection(List<ConsoleInvokeableMethod> actions, string header, string footer, int selectedNumber)
        {
            selectedNumber -= 1;
            ConsoleInvokeableMethod action = actions[selectedNumber];
            MethodInfo method = action.Method;
            MethodInfo invoke = typeof(ConsoleInvokeableMethod).GetMethod("Invoke");
            object[] parameters = GetParameterInput(method);
            action.Parameters = parameters;

            if (!string.IsNullOrEmpty(header))
            {
                Out(header, ConsoleColor.White);
            }

            try
            {
                if (!method.IsStatic)
                {
                    ConstructorInfo ctor = method.DeclaringType.GetConstructor(Type.EmptyTypes);
                    if (ctor == null)
                        ExceptionHelper.Throw<InvalidOperationException>("Specified non-static method is declared on a type that has no parameterless constructor. {0}.{1}", method.DeclaringType.Name, method.Name);

                    action.Provider = ctor.Invoke(null);
                }

                if (IsolateMethodCalls)
                {
                    InvokeInSeparateAppDomain(invoke, action, parameters);
                }
                else
                {
                    InvokeInCurrentAppDomain(invoke, action, parameters);
                }
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    throw ex.InnerException;
                }
                else
                {
                    throw;
                }
            }
            if (!string.IsNullOrEmpty(footer))
            {
                Out(footer, ConsoleColor.White);
            }

            return selectedNumber;
        }

        protected static void ShowActions(List<ConsoleInvokeableMethod> actions)
        {
            for (int i = 1; i <= actions.Count; i++)
            {
                ConsoleInvokeableMethod consoleMethod = actions[i - 1];
                string menuOption = consoleMethod.Information;
                Console.WriteLine("{0}. {1}", i, menuOption);
            }
        }

        protected static List<ConsoleInvokeableMethod> GetConsoleInvokeableMethods(Assembly assemblyToAnalyze)
        {
            return GetConsoleInvokeableMethods<ConsoleAction>(assemblyToAnalyze);
        }

        protected static List<ConsoleInvokeableMethod> GetConsoleInvokeableMethods<TAttribute, TType>() where TAttribute : Attribute, new()
        {
            return GetConsoleInvokeableMethods(typeof(TType), typeof(TAttribute));
        }

        protected static List<ConsoleInvokeableMethod> GetConsoleInvokeableMethods<TAttribute>(Type typeWhoseAssemblyWillBeAnalyzed) where TAttribute : Attribute, new()
        {
            return GetConsoleInvokeableMethods<TAttribute>(typeWhoseAssemblyWillBeAnalyzed.Assembly);
        }

        protected static List<ConsoleInvokeableMethod> GetConsoleInvokeableMethods<TAttribute>(Assembly assemblyToAnalyze) where TAttribute : Attribute, new()
        {
            return GetConsoleInvokeableMethods(assemblyToAnalyze, typeof(TAttribute));
        }

        protected static List<ConsoleInvokeableMethod> GetConsoleInvokeableMethods(Assembly assemblyToAnalyze, Type attrType)
        {
            List<ConsoleInvokeableMethod> actions = new List<ConsoleInvokeableMethod>();
            Type[] types = assemblyToAnalyze.GetTypes();
            foreach (Type type in types)
            {
                actions.AddRange(GetConsoleInvokeableMethods(type, attrType));
            }
            return actions;
        }

        protected static List<ConsoleInvokeableMethod> GetConsoleInvokeableMethods(Type typeToAnalyze, Type attributeAddorningMethod)
        {
            List<ConsoleInvokeableMethod> actions = new List<ConsoleInvokeableMethod>();
            MethodInfo[] methods = typeToAnalyze.GetMethods();
            foreach (MethodInfo method in methods)
            {
                object action = null;
                if (method.HasCustomAttributeOfType(attributeAddorningMethod, false, out action)) //HasCustomAttributeOfType(method, out action))
                {
                    actions.Add(new ConsoleInvokeableMethod(method, (Attribute)action));
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
                    OutLine(string.Format("The method {0} can't be invoked because it takes parameters that are not of type string.", method.Name)
                        , ConsoleColor.Red);
                }

                if (generate)
                    parameterValues.Add("".RandomString(5));
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

        protected static bool HasCustomAttributeOfType<T>(MethodInfo method, out T attribute) where T : Attribute, new()
        {
            return CustomAttributeExtension.HasCustomAttributeOfType<T>(method, out attribute);
        }

        /// <summary>
        /// Makes the specified name a valid command line argument.  Command line
        /// arguments are assumed to be in the format /&lt;name&gt;:&lt;value&gt;.
        /// </summary>
        /// <param name="name"></param>
        public static void AddValidArgument(string name, string description = null)
        {
            AddValidArgument(name, false, description: description);
        }

        /// <summary>
        /// Calls AddValidArgument for every ConsoleAction that has a 
        /// CommandLineSwitch defined
        /// </summary>
        /// <param name="type"></param>
        public static void AddSwitches(Type type)
        {
            MethodInfo[] methods = type.GetMethods();
            foreach (MethodInfo method in methods)
            {
                ConsoleAction action = null;
                if (method.HasCustomAttributeOfType<ConsoleAction>(out action))
                {
                    if (!string.IsNullOrEmpty(action.CommandLineSwitch))
                    {
                        AddValidArgument(action.CommandLineSwitch, true, addAcronym: true, description: action.Information, valueExample: action.ValueExample);
                    }
                }
            }
        }

        /// <summary>
        /// Determines if any command line switches were provided as arguments
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool ReceivedSwitchArguments(Type type)
        {
            MethodInfo[] methods = type.GetMethods();
            bool receivedSwitches = false;
            foreach (MethodInfo method in methods)
            {
                ConsoleAction action = null;
                if (method.HasCustomAttributeOfType<ConsoleAction>(out action))
                {
                    if (!string.IsNullOrEmpty(action.CommandLineSwitch) && !receivedSwitches)
                    {
                        receivedSwitches = Arguments.Contains(action.CommandLineSwitch);
                    }
                }
                if (receivedSwitches)
                {
                    break;
                }
            }
            return receivedSwitches;
        }

        /// <summary>
        /// Execute the methods on the specified instance that are addorned with ConsoleAction
        /// attributes that have CommandLineSwitch(es) defined that match keys in the
        /// specified ParsedArguments using the specified ILogger to report any switches not
        /// found.  An ExpectFailedException will be thrown if more than one method is found
        /// with a matching CommandLineSwitch defined in ConsoleAction attributes
        /// </summary>
        /// <param name="arguments"></param>
        /// <param name="instance"></param>
        /// <param name="logger"></param>
        public static bool ExecuteSwitches(ParsedArguments arguments, object instance, ILogger logger = null)
        {
            Expect.IsNotNull(instance, "instance can't be null, use a Type if executing static method");
            return ExecuteSwitches(arguments, instance.GetType(), instance, logger);
        }

        /// <summary>
        /// Execute the methods on the specified instance that are addorned with ConsoleAction
        /// attributes that have CommandLineSwitch(es) defined that match keys in the
        /// specified ParsedArguments using the specified ILogger to report any switches not
        /// found.  An ExpectFailedException will be thrown if more than one method is found
        /// with a matching CommandLineSwitch defined in ConsoleAction attributes
        /// </summary>
        /// <param name="arguments"></param>
        /// <param name="type"></param>
        /// <param name="isolateMethodCalls"></param>
        /// <param name="logger"></param>
        /// <returns></returns>
        public static bool ExecuteSwitches(ParsedArguments arguments, Type type, bool isolateMethodCalls, ILogger logger = null)
        {
            bool originalValue = IsolateMethodCalls;
            IsolateMethodCalls = isolateMethodCalls;
            bool result = ExecuteSwitches(arguments, type, false, null, logger);
            IsolateMethodCalls = originalValue;
            return result;
        }

        /// <summary>
        /// Execute the methods on the specified instance that are addorned with ConsoleAction
        /// attributes that have CommandLineSwitch(es) defined that match keys in the
        /// specified ParsedArguments using the specified ILogger to report any switches not
        /// found.  An ExpectFailedException will be thrown if more than one method is found
        /// with a matching CommandLineSwitch defined in ConsoleAction attributes
        /// </summary>
        /// <param name="arguments"></param>
        /// <param name="type"></param>
        /// <param name="instance"></param>
        /// <param name="logger"></param>
        /// <returns></returns>
        public static bool ExecuteSwitches(ParsedArguments arguments, Type type, object instance = null, ILogger logger = null)
        {
            return ExecuteSwitches(arguments, type, true, instance, logger);
        }

        /// <summary>
        /// Execute the methods on the specified instance that are addorned with ConsoleAction
        /// attributes that have CommandLineSwitch(es) defined that match keys in the
        /// specified ParsedArguments using the specified ILogger to report any switches not
        /// found.  An ExpectFailedException will be thrown if more than one method is found
        /// with a matching CommandLineSwitch defined in ConsoleAction attributes
        /// </summary>
        /// <param name="arguments"></param>
        /// <param name="type"></param>
        /// <param name="warnForNotFoundSwitches"></param>
        /// <param name="instance"></param>
        /// <param name="logger"></param>
        /// <returns>true if command line switches were executed otherwise false</returns>
        public static bool ExecuteSwitches(ParsedArguments arguments, Type type, bool warnForNotFoundSwitches = true, object instance = null, ILogger logger = null)
        {
            bool executed = false;
            foreach (string key in arguments.Keys)
            {
                ConsoleInvokeableMethod methodToInvoke = GetConsoleInvokeableMethod(arguments, type, key, instance);

                if (methodToInvoke != null)
                {
                    if (IsolateMethodCalls)
                    {
                        methodToInvoke.InvokeInSeparateAppDomain();
                    }
                    else
                    {
                        methodToInvoke.InvokeInCurrentAppDomain();
                    }
                    executed = true;
                    logger?.AddEntry("Executed {0}: {1}", key, methodToInvoke.Information);
                }
                else
                {
                    if (logger != null && warnForNotFoundSwitches)
                    {
                        logger.AddEntry("Specified command line switch was not found {0}", LogEventType.Warning, key);
                    }
                }
            }
            return executed;
        }

        /// <summary>
        /// Makes the specified name a valid command line argument.  Command line
        /// arguments are assumed to be in the format /&lt;name&gt;:&lt;value&gt;.
        /// </summary>
        /// <param name="name">The name of the command line argument.</param>
        /// <param name="allowNull">If true no value for the specified name is necessary.</param>
        /// <param name="addAcronym">Add another valid argument of the acronym of the specified name</param>
        public static void AddValidArgument(string name, bool allowNull, bool addAcronym = false, string description = null, string valueExample = null)
        {
            ValidArgumentInfo.Add(new ArgumentInfo(name, allowNull, description, valueExample));
            if (addAcronym)
            {
                ValidArgumentInfo.Add(new ArgumentInfo(name.CaseAcronym().ToLowerInvariant(), allowNull, $"{description}; same as {name}", valueExample));
            }
        }

        protected static void ParseArgs(string[] args)
        {
            Arguments = new ParsedArguments(args, ValidArgumentInfo.ToArray());
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

        private static ConsoleInvokeableMethod GetConsoleInvokeableMethod(ParsedArguments arguments, Type type, string key, object instance = null)
        {
            string commandLineSwitch = key;
            string switchValue = arguments[key];
            MethodInfo[] methods = type.GetMethods();
            List<ConsoleInvokeableMethod> toExecute = new List<ConsoleInvokeableMethod>();
            foreach (MethodInfo method in methods)
            {
                ConsoleAction consoleAction;
                if (method.HasCustomAttributeOfType<ConsoleAction>(out consoleAction))
                {
                    if (consoleAction.CommandLineSwitch.Or("").Equals(commandLineSwitch) ||
                        consoleAction.CommandLineSwitch.CaseAcronym().ToLowerInvariant().Or("").Equals(commandLineSwitch))
                    {
                        toExecute.Add(new ConsoleInvokeableMethod(method, consoleAction, instance, switchValue));
                    }
                }
            }

            Expect.IsFalse(toExecute.Count > 1, "Multiple ConsoleActions found with the specified command line switch: {0}"._Format(commandLineSwitch));

            if (toExecute.Count == 0)
            {
                return null;
            }

            return toExecute[0];
        }
    }
}
