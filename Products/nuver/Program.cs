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
using System.IO;
using Bam.Net.CommandLine;
using Bam.Net;
using Bam.Net.Testing;
using Bam.Net.Encryption;
using nuver.Nuget;

namespace nuver
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
            #endregion
            AddValidArgument("major", true, "Set or increment the major version", "<value> or /major to increment");
            AddValidArgument("minor", true, "Set or increment the minor version", "<value> or /minor to increment");
            AddValidArgument("patch", true, "Set or increment the patch version", "<value> or /patch to to increment");
            AddValidArgument("p", false, "The path to the .nuspec file");
            AddValidArgument("path", false, "The path to the .nuspec file");
            AddValidArgument("a", false, "If specified, will set the authors value", "<authors>");
            AddValidArgument("authors", false, "If specified, will set the author value", "<authors>");
            AddValidArgument("o", false, "If specified, will set the owners value", "<owners>");
            AddValidArgument("owners", false, "If specified, will set the owners value", "<owners>");
            AddValidArgument("c", false, "If specified, will set the copyright value", "<copyright>");
            AddValidArgument("copyright", false, "If specified, will set the copyright value", "<copyright>");
			AddValidArgument("show", true, "Show information in the specified nuspec file");
            DefaultMethod = typeof(Program).GetMethod("Start");
        }

        public static void Start()
        {
            if (!Arguments.Contains("p") && !Arguments.Contains("path"))
            {
                Out("/p:<path> must be specified", ConsoleColor.Red);
                Exit(1);
            }
            string path = Arguments["p"];
            if (string.IsNullOrEmpty(path))
            {
                path = Arguments["path"];
            }

            if (!File.Exists(path))
            {
                OutLineFormat("file not found: {0}", ConsoleColor.Red, path);
                Exit(1);
            }

            NuspecFile file = new NuspecFile(path);
            SetAuthors(file);
            SetOwners(file);
            SetCopyright(file);

			if (Arguments.Contains("show"))
			{
				string show = Arguments["show"].Or("").ToLowerInvariant();

				OutFormat("Id: {0}\r\n", ConsoleColor.Cyan, file.Id);

				if (show.Contains("version"))
				{
					OutFormat("Version: {0}\r\n", ConsoleColor.DarkBlue, file.Version.Value);
					show = show.Replace("version", "");
				}

				if (show.Contains("id"))
				{
					show = show.Replace("id", "");
				}

				string infoToShow = show.Or("authors, owners, description, releaseNotes, copyright");
				string[] infos = infoToShow.DelimitSplit(",");
				infos.Each(info =>
				{
					string propName = info.PascalCase();
					string value = file.Property<string>(propName);
					OutFormat("{0}: {1}\r\n", ConsoleColor.Cyan, propName, value);
				});

				OutLine();
			}
			else
			{
				if (Arguments.Contains("major"))
				{
					IncrementMajor(file);
				}

				if (Arguments.Contains("minor"))
				{
					IncrementMinor(file);
				}

				if (Arguments.Contains("patch"))
				{
					IncrementPatch(file);
				}

				file.Save();
			}
        }

		private static void IncrementPatch(NuspecFile file)
		{
			string patch = Arguments["patch"];
			if (!string.IsNullOrEmpty(patch))
			{
				file.Version.Patch = patch;
			}
			else
			{
				file.Version.IncrementPatch();
			}
		}

		private static void IncrementMinor(NuspecFile file)
		{
			string minor = Arguments["minor"];
			if (!string.IsNullOrEmpty(minor))
			{
				file.Version.Minor = minor;
			}
			else
			{
				file.Version.IncrementMinor();
			}
		}

		private static void IncrementMajor(NuspecFile file)
		{
			string major = Arguments["major"];
			if (!string.IsNullOrEmpty(major))
			{
				file.Version.Major = major;
			}
			else
			{
				file.Version.IncrementMajor();
			}
		}

        private static void SetCopyright(NuspecFile file)
        {
            string format = "Copyright © {0} {1}";
            int year = DateTime.UtcNow.Year;
            string copyRight = file.Copyright;
            if (Arguments.Contains("c"))
            {
                copyRight = format._Format(Arguments["c"], year);
            }
            else if (Arguments.Contains("copyright"))
            {
                copyRight = format._Format(Arguments["copyright"], year);
            }
            file.Copyright = copyRight;
        }

        private static void SetAuthors(NuspecFile file)
        {
            string authors = file.Authors;
            if (Arguments.Contains("a"))
            {
                authors = Arguments["a"];
            }
            else if (Arguments.Contains("authors"))
            {
                authors = Arguments["authors"];
            }

            file.Authors = authors;
        }

        private static void SetOwners(NuspecFile file)
        {
            string owners = file.Owners;
            if (Arguments.Contains("o"))
            {
                owners = Arguments["o"];
            }
            else if (Arguments.Contains("owners"))
            {
                owners = Arguments["owners"];
            }

            file.Owners = owners;
        }
    }
}
