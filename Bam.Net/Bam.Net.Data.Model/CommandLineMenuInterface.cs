/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using Bam.Net;
using Bam.Net.CommandLine;
using Bam.Net.Data.Model;

namespace Bam.Net.Data.Model
{
    public abstract class CommandLineMenuInterface : CommandLineInterface
    {
        static List<Type> _types = new List<Type>();
        static MenuContext _currentContext;
        static MenuContext _mainContext;
        static IModelActionMenuWriter _writer;

        protected static MenuContext CurrentContext
        {
            get
            {
                return _currentContext;
            }
        }

        protected static MenuContext MainContext
        {
            get
            {
                return _mainContext;
            }
        }

        protected static IModelActionMenuWriter Writer
        {
            get
            {
                return _writer;
            }
        }

        /// <summary>
        /// Gets or sets the Type of the MenuContext to use.
        /// The default is ConsoleMenuContext
        /// </summary>
        protected static Type MenuContextType
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the Type of the MenuWriter to use.
        /// The default is ConsoleActionMenuWriter
        /// </summary>
        protected static Type MenuWriterType
        {
            get;
            set;
        }

        protected static Assembly AssemblyToAnalyze
        {
            get;
            set;
        }

        protected static void Start(string[] args)
        {
            SetProviderTypes();

            ParseArgs(args);
            SetTypes();
            SelectClass();
        }

        private static void SetProviderTypes()
        {
            if (MenuWriterType == null)
            {
                MenuWriterType = typeof(ConsoleModelActionMenuWriter);
            }

            if (MenuContextType == null)
            {
                MenuContextType = typeof(ConsoleMenuContext);
            }

            if (AssemblyToAnalyze == null)
            {
                AssemblyToAnalyze = Assembly.GetEntryAssembly();
            }

            _writer = MenuWriterType.Construct<IModelActionMenuWriter>();
        }
        
        private static void SetTypes()
        {
            Type[] types = AssemblyToAnalyze.GetTypes();
            for (int i = 0; i < types.Length; i++)
            {
                Type type = types[i];
                string name = string.Format("{0}.{1}", type.Namespace, type.Name);
                _types.Add(type);
            }
        }

        private static void SelectClass()
        {
            for (int i = 0; i < _types.Count; i++)
            {
                Type type = _types[i];
                string display = string.Format("{0}.{1}", type.Namespace, type.Name);
                OutLineFormat("{0}. {1}", i + 1, display);
            }
            string selection = Prompt(" select class");
            int num = 0;
            if (int.TryParse(selection, out num))
            {
                if (num > _types.Count || num <=0)
                {
                    InvalidSelection(selection);
                }
                else
                {
                    Type selectedType = _types[num - 1];
                    MenuLoop(selectedType);
                }
            }
            else
            {
                InvalidSelection(selection);
            }            
        }

        protected static void MenuLoop(Type selectedType)
        {
            SetProviderTypes();
            _mainContext = MenuContextType.Construct<MenuContext>(selectedType, _writer);
            _currentContext = _mainContext;
            MenuLoop();
        }


        protected static void InvalidSelection(string selected = "")
        {
            OutFormat("Invalid selection: {0}", ConsoleColor.Red, selected);
            Exit(1);
        }

        private static void MenuLoop()
        {
            Clear();
            _currentContext.Show();
            string selection = Prompt(" select option");
            
            MightQuit(selection);

            if (selection.ToLowerInvariant().Equals("b"))
            {
                if (_currentContext == _mainContext)
                {
                    Clear();
                    SelectClass();
                }
                else
                {
                    _currentContext = _currentContext.LastMenu;
                }
            }
            else
            {
                object result = _currentContext.Menu.RunSelection(selection);
                if (result != null)
                {
                    MenuContext next = MenuContextType.Construct<MenuContext>(result, _writer);
                    next.LastMenu = _currentContext;
                    _currentContext = next;
                }
            }
            MenuLoop();
        }

        protected static void MightQuit(string selection)
        {
            if (selection.ToLowerInvariant().Equals("q"))
            {
                Exit(0);
            }
        }
    }
}
