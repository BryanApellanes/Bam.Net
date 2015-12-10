/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;

namespace Naizari.Javascript.BoxControls
{
    [Obsolete("This class has horrible performance and will be deleted after being checked in to a single change set.")]
    public class BoxVariableParser
    {
        object currentlyReading;
        object objectToTemplate;
        TextReader source;
        StringBuilder tokenValueBuffer;
        char currentChar;

        List<BoxVariable> variables;

        public BoxVariableParser(string text, object objectToTemplate)
        {
            this.objectToTemplate = objectToTemplate;
            this.currentlyReading = objectToTemplate;
            this.OriginalText = text;
            this.source = new StringReader(text);
            this.tokenValueBuffer = new StringBuilder();
            this.variables = new List<BoxVariable>();
            this.currentChar = ' ';
            while (!AtEndOfSource)
                this.ReadNextVariable();

            this.SetNewText();
        }

        private void SetNewText()
        {
            string newText = this.OriginalText.ToString();
            foreach (BoxVariable var in this.variables)
            {
                if (var.IsValid)
                    newText = newText.Replace(var.VariableName, var.Value);
            }
            this.VariableReplacedText = newText.ToString();
        }

        public BoxVariable[] Variables
        {
            get
            {
                return this.variables.ToArray();
            }
        }

        private bool AtEndOfSource
        {
            get { return this.currentChar == '\0'; }
        }

        private void ReadNextVariable()
        {
           // read until $$
            while (!tokenValueBuffer.ToString().EndsWith(BoxServer.VariablePrefix))
            {
                StoreCurrentCharAndReadNext();
                if (AtEndOfSource)
                    return;
            }
            // clear tokenValueBuffer
            tokenValueBuffer.Length = 0;
            // read until $$
            while (!tokenValueBuffer.ToString().EndsWith(BoxServer.VariableSuffix))
            {
                if (this.currentChar == ' ')
                {
                    this.ExtractBuffer();
                    this.ReadNextVariable();
                }
                StoreCurrentCharAndReadNext();
                if (AtEndOfSource)
                    return;
            }
            // add the $$ back to the front
            string variableName = BoxServer.VariablePrefix + this.ExtractBuffer();
            this.variables.Add(new BoxVariable(variableName, this.objectToTemplate));
        }

        private void ReadNextChar()
        {
            int nextChar = source.Read();
            if (nextChar > 0)
                currentChar = (char)nextChar;
            else
                currentChar = '\0';
        }

        private void StoreCurrentCharAndReadNext()
        {
            tokenValueBuffer.Append(currentChar);
            ReadNextChar();
        }

        private string ExtractBuffer()
        {
            string retVal = tokenValueBuffer.ToString();
            tokenValueBuffer.Length = 0;
            return retVal;
        }

        //private string GetProperty(string propertyName)
        //{
     
        //    PropertyInfo property = this.currentlyReading.GetType().GetProperty(propertyName);
        //    if (property != null)
        //    {
        //        return property.GetValue(this.currentlyReading, null).ToString();                
        //    }
        //    return string.Empty;
        //}

        public object ObjectToTemplate
        {
            get { return this.objectToTemplate; }
            set { this.objectToTemplate = value; }
        }
        public string OriginalText { get; set; }
        public string VariableReplacedText { get; set; }
    }
}
