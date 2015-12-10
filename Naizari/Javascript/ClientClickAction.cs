/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Naizari.Extensions;
using Naizari.Test;
using Naizari.Javascript.JsonControls;

namespace Naizari.Javascript
{
    public class ClientClickAction
    {
        public ClientClickAction(string clickActionString)
        {

            string[] nameValue = StringExtensions.DelimitSplit(clickActionString, ":");
            try
            {
                Expect.IsTrue(nameValue.Length == 2);
            }   // catch the ExpectFailedException and throw JsonInvalidOperationException so all classes from the Javascript namespace
                // throw a form of JsonException.  This will make the entire Json framework easier to debug down the road.
            catch (ExpectFailedException efe)
            {
                throw new JsonInvalidOperationException(string.Format("Invalid clickActionString specified: {0}", clickActionString), efe);
            }
            this.ActionType = (ClientClickActionType)Enum.Parse(typeof(ClientClickActionType), nameValue[0]);
            this.Target = nameValue[1];
        }

        public ClientClickActionType ActionType { get; internal set; }
        public string Target { get; internal set; }

        public override string ToString()
        {
            return string.Format("{0}:{1}", this.ActionType.ToString(), this.Target);
        }
    }
}
