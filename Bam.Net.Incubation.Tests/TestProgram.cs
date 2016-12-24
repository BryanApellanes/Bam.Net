/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Data;
using System.Data.Common;
using System.Data.Sql;
using System.Data.SqlClient;
using Bam.Net.Testing;
using System.IO;
using Bam.Net.CommandLine;
using Bam.Net;
using Bam.Net.Data;
using Bam.Net.Incubation;
using Bam.Net.Configuration;

namespace CommandLineTests
{
    public class TestProgram : CommandLineTestInterface
    {
        // Add optional code here to be run before initialization/argument parsing.
        public static void PreInit()
        {
            #region expand for PreInit help
            // To accept custom command line arguments you may use            
            /*
             * AddValidArgument(string argumentName, bool allowNull)
            */

            // All arguments are assumed to be name value pairs in the format
            // /name:value unless allowNull is true.

            // to access arguments and values you may use the protected member
            // arguments. Example:

            /*
             * arguments.Contains(argName); // returns true if the specified argument name was passed in on the command line
             * arguments[argName]; // returns the specified value associated with the named argument
             */

            // the arguments protected member is not available in PreInit() (this method)
            #endregion
			AddValidArgument("t", true, description: "run all tests");
			DefaultMethod = typeof(TestProgram).GetMethod("Start");
		}

		public static void Start()
		{
			if (Arguments.Contains("t"))
			{
				RunAllUnitTests(typeof(TestProgram).Assembly);
			}
			else
			{
				Interactive();
			}
		}

        /*
          * Methods addorned with the ConsoleAction attribute can be run
          * interactively from the command line while methods addorned with
          * the TestMethod attribute will be run automatically when the
          * compiled executable is run.  To run ConsoleAction methods use
          * the command line argument /i.
          * 
          * All methods addorned with ConsoleAction and TestMethod attributes 
          * must be static for the purposes of extending CommandLineTestInterface
          * or an exception will be thrown.
          * 
          */

        // To run ConsoleAction methods use the command line argument /i.        
        [ConsoleAction("This is a main menu option")]
        public static void ExampleMainMenuOption(string parameter)
        {
            Out(parameter, ConsoleColor.Green);
        }

        public class Primate
        {
        }

        public class Monkey : Primate
        {
        }

        public class Gorilla: Monkey
        {
            public Gorilla(IFruit fruit)
            {
                this.Fruit = fruit;
            }

            public IFruit Fruit { get; private set; }
        }
        public interface IFruit
        { }
        public class Banana : IFruit
        { }
        public class Apple : IFruit
        { }

        interface IVegetable
        { }

        public class Tomato : IFruit
        { }

        public abstract class Eater
        {
            public Eater(IFruit food)
            {
                Food = food;
            }
            public IFruit Food { get; set; }
        }

        [UnitTest]
        public static void IncubatorShouldGiveMeWhatISet()
        {
            Incubator i = new Incubator();
            i.Set(typeof(Primate), new Monkey());
            Primate m = i.Get<Primate>();
            Expect.IsTrue(m.GetType() == typeof(Monkey));
        }

        [UnitTest]
        public static void IncubatorShouldGiveMeWhatISet2()
        {
            Incubator i = new Incubator();
            i.Set<Primate>(new Monkey());
            Primate m = i.Get<Primate>();
            Expect.IsTrue(m.GetType() == typeof(Monkey));
        }

        [UnitTest]
        public static void IncubatorShouldTakeAFuncAndReturnResult()
        {
            Incubator i = new Incubator();
            Func<Primate> f = () => { return new Monkey(); };
            i.Set(typeof(Primate), f);
            Primate m = i.Get<Primate>();
            Expect.IsTrue(m.GetType() == typeof(Monkey));
        }

        [UnitTest]
        public static void IncubatorShouldTakeAFuncAndReturnResult2()
        {
            Incubator i = new Incubator();            
            i.Set<Primate>(() => { return new Monkey(); });
            Primate m = i.Get<Primate>();
            Expect.IsTrue(m.GetType() == typeof(Monkey));
        }

        [UnitTest]
        public static void IncubatorShouldTakeAFuncAndReturnByClassName()
        {
            Incubator i = new Incubator();
            i.Set<Primate>(() => { return new Monkey(); });
            object m = i.Get("Primate");
            Expect.IsTrue(m.GetType() == typeof(Monkey));
        }

        [UnitTest]
        public static void IncubatorShouldTakeAFuncAndPopOutSpecifiedType()
        {
            Incubator i = new Incubator();
            Type type;
            i.Set<Primate>(() => { return new Monkey(); });
            object m = i.Get("Primate", out type);
            Expect.IsTrue(type == typeof(Primate));
        }

        [UnitTest]
        public static void FluentIncubatorTest()
        {
            Incubator i = Requesting.A<Primate>().Returns<Monkey>();
            Primate m = i.Get<Primate>();
            Expect.IsTrue(m.GetType() == typeof(Monkey));
        }

        [UnitTest]
        public static void FluentConstructorTests()
        {
            Incubator withBanana = Requesting.A<IFruit>().Returns<Banana>();
            withBanana.AskingFor<Primate>().Returns<Gorilla>();
            Primate p = withBanana.Get<Primate>();
            Expect.IsTrue(p is Gorilla);
            Gorilla g = (Gorilla)p;
            Expect.IsTrue(g.Fruit.GetType() == typeof(Banana));
        }
        [UnitTest]
        public static void FluentConstructorTests2()
        {
            Incubator withApple = Requesting.A<IFruit>().Returns<Apple>();
            withApple.AskingFor<Monkey>().Returns<Gorilla>();
            Primate p = withApple.Get<Monkey>();
            Expect.IsTrue(p is Gorilla);
            Gorilla g = (Gorilla)p;
            Expect.IsTrue(g.Fruit.GetType() == typeof(Apple));
        }
        [UnitTest]
        public static void FluentConstructorTests3()
        {
            Incubator withApple = Requesting.A<IFruit>().Returns<Apple>();
            withApple.Bind<Monkey>().To<Gorilla>();
            Primate p = withApple.Get<Monkey>();
            Expect.IsTrue(p is Gorilla);
            Gorilla g = (Gorilla)p;
            Expect.IsTrue(g.Fruit.GetType() == typeof(Apple));
        }

        #region do not modify
        static void Main(string[] args)
        {
            PreInit();
            Initialize(args);
        }


        #endregion
    }
}
