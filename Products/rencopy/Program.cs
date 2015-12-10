/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Bam.Net.CommandLine;
using Bam.Net.Configuration;
using Bam.Net;
using Bam.Net.Testing;
using Bam.Net.Encryption;

using System.IO;
using System.Threading;

namespace rencopy
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

            AddValidArgument("run", true, "Run the copy and rename process");
            AddValidArgument("conf", false, "Path to the config file to use", "<.\\rencopy.json>");

            DefaultMethod = typeof(Program).GetMethod("Start");
            Expect.IsNotNull(DefaultMethod);
            #endregion
        }

        public static void Start()
        {
            if (Arguments.Contains("run"))
            {
                if (Arguments.Contains("conf"))
                {
                    _configFile = Arguments["conf"];
                }

                CopyAndRename();
            }
            else
            {
                MainMenu("(Ren)ame & (Copy)");
            }
        }

        static string _configFile = ".\\rencopy.json";
        [ConsoleAction("Copy and rename")]
        public static void CopyAndRename()
        {
            FileInfo file = new FileInfo(_configFile);
            if (!file.Exists)
            {
                OutFormat("{0} file not found, beginning configuration.", ConsoleColor.Yellow, _configFile);
                ConfigureCopyAndRename();
            }
            else
            {
                CopyAndRename(file);
            }
        }

        /// <summary>
        /// Performs the copy and rename operation using the specified config file
        /// </summary>
        /// <param name="configFile"></param>
        public static void CopyAndRename(FileInfo configFile)
        {
            _config = configFile.FromJson<RenCopyConfig>();
            _sourceDir = new DirectoryInfo(_config.SourceFolder);
            _targetDir = new DirectoryInfo(_config.TargetFolder);
            if (!_targetDir.Exists)
            {
                _targetDir.Create();
            }
            _curDir = _sourceDir;
            CopyFiles();
        }

        static RenCopyConfig _config;
        static DirectoryInfo _sourceDir;
        static DirectoryInfo _targetDir;
        static DirectoryInfo _curDir;

        private static void CopyFiles()
        {
			List<string> fileExtensionsToDoTextReplacementsOn = _config.TextReplacementFileExtensions.Select(s => s.ToLowerInvariant()).ToList();
			List<string> fileExtensionsToCopyBare = _config.CopyExtensions.Select(s => s.ToLowerInvariant()).ToList();

            // get the files from the current dir
            FileInfo[] files = _curDir.GetFiles();
			List<string> fileNamesToIgnore = new List<string>();
			foreach (string ignore in _config.Ignore)
			{
				FileInfo[] filesToIgnore = _curDir.GetFiles(ignore);
				foreach (FileInfo file in filesToIgnore)
				{
					fileNamesToIgnore.Add(file.Name);
				}
			}

            for (int i = 0; i < files.Length; i++)
            {
                FileInfo file = files[i];
				if (fileNamesToIgnore.Contains(file.Name))
				{
					continue;
				}

                string content = string.Empty;
                bool writeContent = false;
                bool copyBare = fileExtensionsToCopyBare.Contains(file.Extension) || _config.CopyAllFiles;

                // establish the new path by replacing the sourceDir fullname with the targetDir fullname
                DirectoryInfo dirOfFile = file.Directory;
                string targetPrefix = dirOfFile.FullName.Replace(_sourceDir.FullName, "");
                string targetFileName = file.Name;

                DoTextReplacements(fileExtensionsToDoTextReplacementsOn, file, ref content, ref writeContent, ref targetPrefix, ref targetFileName);

                string fullTargetPath = string.Format("{0}{1}\\{2}", _targetDir.FullName, targetPrefix, targetFileName);
                FileInfo newFile = new FileInfo(fullTargetPath);
                if (newFile.Exists)
                {
                    if (newFile.IsReadOnly)
                    {
                        newFile.RemoveAttribute(FileAttributes.ReadOnly);
                    }
                    newFile.Delete();
                    newFile.Refresh();
                }

                if (!newFile.Directory.Exists)
                {
                    newFile.Directory.Create();
                }

                try
                {
                    if (writeContent)
                    {
                        content.SafeWriteToFile(fullTargetPath, true);
                    }
                    else if(copyBare)
                    {
                        file.CopyTo(fullTargetPath, true);
                    }
                }
                catch (Exception ex)
                {
                    OutFormat("An error occurred with file {0}:\r\n\t{1}", ConsoleColor.Red, file.FullName, ex.Message);
                    Thread.Sleep(2500);
                    file.CopyTo(fullTargetPath, true);
                }
            }

            // get the subdirectories of the current directory and repeat
            Traverse();
        }

        private static void DoTextReplacements(List<string> fileExtensions, FileInfo file, ref string content, ref bool writeContent, ref string targetPrefix, ref string targetFileName)
        {
            string originalContent = File.ReadAllText(file.FullName);
            string newContent = originalContent;
            foreach (TextReplacement replacement in _config.TextReplacements)
            {
                if (fileExtensions.Contains(file.Extension.ToLowerInvariant()) && file.IsText())
                {
                    writeContent = true;
                    string originalTargetFileName = targetFileName;
                    targetFileName = replacement.GetNewText(targetFileName);
                    string afterReplaceTargetFileName = targetFileName;
                    if (!originalTargetFileName.Equals(afterReplaceTargetFileName))
                    {
                        OutFormat("original file name: {0}\r\n", ConsoleColor.Cyan, originalTargetFileName.First(50));
                        OutFormat("new file name: {0}\r\n", ConsoleColor.Yellow, afterReplaceTargetFileName.First(50));
                    }

                    string currentContent = newContent;
                    newContent = replacement.GetNewText(currentContent);
                    string afterReplaceContent = newContent;
                    if (!originalContent.Equals(afterReplaceContent))
                    {
                        OutFormat("original content: {0}\r\n", ConsoleColor.Blue, currentContent.First(50));
                        OutFormat("new content: {0}\r\n", ConsoleColor.Magenta, afterReplaceContent.First(50));
                    }
                }

                // change the name of the file also
                string originalTargetPrefix = targetPrefix;
                targetPrefix = replacement.GetNewText(targetPrefix);
                string afterReplaceTargetPrefix = targetPrefix;
                if (!originalTargetPrefix.Equals(afterReplaceTargetPrefix))
                {
                    OutFormat("original target prefix: {0}\r\n", ConsoleColor.Cyan, originalTargetPrefix.First(50));
                    OutFormat("new target prefix: {0}\r\n", ConsoleColor.Yellow, afterReplaceTargetPrefix.First(50));
                }
            }

            content = newContent;
        }

        private static void Traverse()
        {
            DirectoryInfo[] subDirs = _curDir.GetDirectories();
			List<string> directoryNamesToIgnore = new List<string>();
			foreach (string ignore in _config.Ignore)
			{
				DirectoryInfo[] dirs = _curDir.GetDirectories(ignore);
				foreach(DirectoryInfo dir in dirs)
				{
					directoryNamesToIgnore.Add(dir.Name);
				}
			}

            for (int i = 0; i < subDirs.Length; i++)
            {
                _curDir = subDirs[i];
				if(directoryNamesToIgnore.Contains(_curDir.Name))
				{
					continue;
				}
                CopyFiles();
            }
        }

        [ConsoleAction("Configure copy and rename")]
        public static void ConfigureCopyAndRename()
        {
            List<string> textReplacementFileExtensions = new List<string>();
            List<string> copyExtensions = new List<string>();
            string answer = "";
            while (!answer.Equals("done"))
            {
                if (!string.IsNullOrEmpty(answer))
                {
                    textReplacementFileExtensions.Add(answer);
                }
                answer = GetTextReplacementExtension();
            }

            answer = GetCopyFileExtension();
            bool copyAll = false;
            if (answer.ToLowerInvariant().Equals("all"))
            {
                copyAll = true;
            }
            else
            {
                while (!answer.Equals("done"))
                {
                    if (!string.IsNullOrEmpty(answer))
                    {
                        copyExtensions.Add(answer);
                    }
                    answer = GetCopyFileExtension();
                }
            }            

            List<TextReplacement> textReplacements = new List<TextReplacement>();
            TextReplacement replacement = new TextReplacement("", "");
            while (replacement != null)
            {
                if(!string.IsNullOrEmpty(replacement.OldText))
                {
                    textReplacements.Add(replacement);
                }
                replacement = GetTextReplacement();
            }

            string sourceFolder = Prompt("Enter the source folder path");
            string targetFolder = Prompt("Enter the target folder path");

            RenCopyConfig config = new RenCopyConfig();
            config.SourceFolder = sourceFolder;
            config.TargetFolder = targetFolder;
            config.TextReplacementFileExtensions = textReplacementFileExtensions.ToArray();
            config.CopyExtensions = copyExtensions.ToArray();
            config.CopyAllFiles = copyAll;
            config.TextReplacements = textReplacements.ToArray();

            config.ToJsonFile(_configFile);

            if (Confirm("Copy files now?"))
            {
                CopyAndRename();
            }
        }

        private static TextReplacement GetTextReplacement()
        {
            TextReplacement result = null;
            string oldText = Prompt("Enter the old text to be replaced (type done to finish)");

            if (!oldText.Equals("done"))
            {
                string newText = Prompt("Enter the text to replace it with (type done to finish)");
                if (!newText.Equals("quit") && !newText.Equals("done"))
                {
                    result = new TextReplacement(oldText, newText);
                }
            }

            return result;
        }

        private static string GetTextReplacementExtension()
        {
            return Prompt("Enter a file extension that will be analyzed for rename and content replacement\r\n (include leading dot; type done to finish)");
        }

        private static string GetCopyFileExtension()
        {
            return Prompt("Enter a file extension (or type 'all') for files that should be copied without modification (type done to finish)");
        }

        [ConsoleAction("Show copy and rename configuration")]
        public static void ShowCopyAndRenameConfig()
        {
            if (!File.Exists(_configFile))
            {
                OutLineFormat("{0} file not found, beginning configuration.", ConsoleColor.Yellow, _configFile);
                ConfigureCopyAndRename();
            }
            else
            {
                RenCopyConfig config = File.ReadAllText(_configFile).FromJson<RenCopyConfig>();
                OutLineFormat("SrcFolder: {0}", ConsoleColor.Cyan, config.SourceFolder);
                OutLineFormat("DstFolder: {0}", ConsoleColor.Cyan, config.TargetFolder);
                OutLineFormat("\t*** extensions\r\n");
                foreach (string ext in config.TextReplacementFileExtensions)
                {
                    OutLineFormat("\t{0}", ConsoleColor.Magenta, ext);
                }
                OutLineFormat("\t*** copy extensions\r\n");
                foreach (string ext in config.CopyExtensions)
                {
                    OutLineFormat("\t{0}\r\n", ConsoleColor.Blue, ext);
                }
                OutLineFormat("\t*** replacements\r\n");
                foreach (TextReplacement r in config.TextReplacements)
                {
                    OutLineFormat("\told: {0}, new: {1}", ConsoleColor.Yellow, r.OldText, r.NewText);
                }
				OutLineFormat("\t*** ignore patterns (for files and folders)");
				foreach (string ignore in config.Ignore)
				{
					OutLineFormat("\t{0}", ignore);
				}
            }
        }
    }

}
