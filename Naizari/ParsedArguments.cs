/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Naizari
{
    public enum ArgumentParseStatus
    {
        Invalid,
        Success,
        Error
    }

    /// <summary>
    /// Class used to parse command line arguments.  All arguments are 
    /// assumed to be in the format /&lt;name&gt;:&lt;value&gt; or an ArgumentException will be thrown 
    /// during parsing.
    /// </summary>
    public class ParsedArguments
    {
        //List<string> validArgumentNames;
        public ParsedArguments(string[] args, string[] validArgNames)
            : this(args, ArgumentInfo.FromStringArray(validArgNames))
        {
        }

        public ParsedArguments(string[] args, ArgumentInfo[] validArgumentInfos)
        {
            this.parsedArguments = new Dictionary<string, string>();
            if (args.Length == 0)
            {
                this.Status = ArgumentParseStatus.Success;
                this.Message = "No arguments";
                return;
            }
            ArgumentInfoHash validArguments = new ArgumentInfoHash(validArgumentInfos);
            
            foreach (string argument in args)
            {
                string arg = argument.Trim();

                if (!arg.StartsWith("/") || !(arg.Length > 1))
                {
                    Message = "Unrecognized argument format: " + arg;
                    Status = ArgumentParseStatus.Error;
                }
                else
                {
                    string[] nameValue = arg.Substring(1, arg.Length - 1).Split(new string[] { ":" }, StringSplitOptions.RemoveEmptyEntries);
                    string name = string.Empty;
                    if (nameValue.Length > 0)
                        name = nameValue[0];

                    // allow ":" in arg value
                    if (nameValue.Length > 2)
                    {
                        int startIndex = arg.IndexOf(":") + 1;
                        nameValue = new string[] { name, arg.Substring(startIndex, arg.Length - startIndex) };
                    }

                    if (nameValue.Length == 1 && validArguments[name] != null)
                    {
                        if (validArguments[name].AllowNullValue)
                            parsedArguments.Add(name, "true");
                        else
                            Message = "No value specified for " + name;
                    }

                    if (nameValue.Length == 2)
                    {
                        if (validArguments[name] == null)
                        {
                            Message = "Invalid argument name specified";
                            Status = ArgumentParseStatus.Error;
                        }
                        else
                        {
                            if (parsedArguments.ContainsKey(name))
                                parsedArguments[name] = nameValue[1];
                            else
                                parsedArguments.Add(name, nameValue[1]);
                        }
                    }

                }
            }

            if (Status != ArgumentParseStatus.Error)
                Status = ArgumentParseStatus.Success;
        }

        public bool Contains(string argumentToLookFor)
        {
            return parsedArguments.ContainsKey(argumentToLookFor);
        }

        public string Message { get; set; }
        public ArgumentParseStatus Status { get; set; }

        Dictionary<string, string> parsedArguments;
        public string this[string name]
        {
            get
            {
                if (parsedArguments.ContainsKey(name))
                    return parsedArguments[name];

                return null;
            }
        }
    }
}
