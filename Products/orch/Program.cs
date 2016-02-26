/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net;
using Bam.Net.CommandLine;
using Bam.Net.Testing;
using Bam.Net.Automation;
using System.Reflection;
using System.IO;

namespace foreman
{
    [Serializable]
    class Program : CommandLineTestInterface
    {
        static void Main(string[] args)
        {
            PreInit();
            Initialize(args);
        }

        public static void PreInit()
        {
            #region expand for PreInit help
            // To accept custom command line arguments you may use            
            /*
             * AddValidArgument(string argumentName, bool allowNull)
            */

            // All arguments are assumed to be name value pairs in the format
            // /name:value unless allowNull is true then only the name is necessary.

            // to access arguments and values you may use the protected member
            // arguments. Example:

            /*
             * arguments.Contains(argName); // returns true if the specified argument name was passed in on the command line
             * arguments[argName]; // returns the specified value associated with the named argument
             */

            // the arguments protected member is not available in PreInit() (this method)

            AddValidArgument("run", true, description: "Run the copy and rename process");

            DefaultMethod = typeof(Program).GetMethod("Start");
            Expect.IsNotNull(DefaultMethod);
            #endregion
        }

        public static void Start()
        {
            MainMenu("Foreman");
        }
        
        [ConsoleAction("Create Job")]
        public void CreateJob()
        {
            string jobName = Prompt("Enter the name of the job:");
            Job job = new Job(jobName);

            Assembly automationAssembly = typeof(Job).Assembly;
            Type[] allTypes = automationAssembly.GetTypes();
            List<Type> workerTypes = new List<Type>();
            allTypes.Each((type) =>
            {
                if (type.ImplementsInterface<IWorker>())
                {
                    workerTypes.Add(type);
                }
            });

            workerTypes.Each((wt, i) => OutLineFormat("{0}. {1}", ConsoleColor.Cyan, i + 1, wt.Name));

            int workTypeIndex = NumberPrompt("Select work to add");
            if(workTypeIndex <= 0)
            {
                Out("Invalid selection", ConsoleColor.Red);
            }
            string workName = Prompt("Enter a name for the work");
            Type workToAdd = workerTypes[workTypeIndex - 1];
            object work = workToAdd.Construct(workName);
            job.AddWorker((IWorker)work);
        }
    }
}
