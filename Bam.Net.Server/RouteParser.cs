using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Text.RegularExpressions; 

namespace Bam.Net.Server
{
    // Taken from: https://gist.github.com/wcharczuk/2284226
    /// <summary>
    /// Utiltiy to parse routes based on a token format.
    /// </summary>
    /// <remarks>
    /// Casing doesn't matter!
    /// </remarks>
    /// <example>
    /// <code>
    /// var parser = new RouteParser("{protocol}://mydomain.com/{itemCategory}/{itemId}");
    /// var variables = parser.Variables; //should be .Count == 3
    /// var values = parser.ParseRouteInstance("https://mydomain.com/foo/1");
    /// //values = { { "protocol" => "https"}, { "itemCategory" => "foo"}, { "itemId" => "1" } }
    /// </code>
    /// </example>
    [Serializable]
    public class RouteParser
    {
        public RouteParser(string route, Char variableStartChar = '{', Char variableEndChar = '}')
        {
            RouteFormat = route;
            this.VariableStartChar = variableStartChar;
            this.VariableEndChar = variableEndChar;
            ParseRouteFormat();
        }

        private const string RouteTokenPattern = @"[{0}].+?[{1}]"; //the 0 and 1 are used by the string.format function, they are the start and end characters.
        private const string VariableTokenPattern = "(?<{0}>[^,]*)"; //the <>'s denote the group name; this is used for reference for the variables later.

        /// <summary>
        /// This is the route template that values are extracted based on.
        /// </summary>
        /// <value>
        /// A string containing variables denoted by the <c>VariableStartChar</c> and the <c>VariableEndChar</c>
        /// </value>
        public string RouteFormat { get; set; }

        /// <summary>
        /// This is the character that denotes the beginning of a variable name.
        /// </summary>
        public char VariableStartChar { get; set; }

        /// <summary>
        /// This is the character that denotes the end of a variable name.
        /// </summary>
        public char VariableEndChar { get; set; }

        /// <summary>
        /// A hash set of all variable names parsed from the <c>RouteFormat</c>.
        /// </summary>
        public HashSet<string> Variables { get; set; }

        /// <summary>
        /// Initialize the Variables set based on the <c>RouteFormat</c>
        /// </summary>
        public void ParseRouteFormat()
        {
            var variableList = new List<string>();
            var matchCollection = System.Text.RegularExpressions.Regex.Matches
                (
                    this.RouteFormat
                    , string.Format(RouteTokenPattern, VariableStartChar, VariableEndChar)
                    , RegexOptions.IgnoreCase
                );

            foreach (var match in matchCollection)
            {
                variableList.Add(RemoteVariableChars(match.ToString()));
            }
            Variables = new HashSet<string>(variableList);
        }

        /// <summary>
        /// Extract variable values from a given instance of the route you're trying to parse.
        /// </summary>
        /// <param name="routeInstance">The route instance.</param>
        /// <returns>A dictionary of Variable names mapped to values.</returns>
        public Dictionary<string, string> ParseRouteInstance(string routeInstance)
        {
            var inputValues = new Dictionary<string, string>();
            string formatUrl = new string(RouteFormat.ToArray());
            foreach (string variable in Variables)
            {
                formatUrl = formatUrl.Replace(WrapWithVariableChars(variable), string.Format(VariableTokenPattern, variable));
            }
            var regex = new Regex(formatUrl, RegexOptions.IgnoreCase);
            var matchCollection = regex.Match(routeInstance);

            foreach (var variable in Variables)
            {
                var value = ((Group)matchCollection.Groups[variable]).Value;
                inputValues.Add(variable, value);
            }

            return inputValues;
        }

        /// <summary>
        /// Replace a variable in the <c>RouteFormat</c> with a specified value.
        /// </summary>
        /// <param name="variableName">The variable name to replace.</param>
        /// <param name="variableValue">The value to replace with.</param>
        /// <param name="workingRoute">An 'in progress' route that may contain values that have already been replaced.</param>
        /// <returns>A <c>workingRoute</c></returns>
        public string SetVariable(string variableName, string variableValue, string workingRoute = null)
        {
            if (!variableName.StartsWith(VariableStartChar.ToString()) && !variableName.EndsWith(VariableEndChar.ToString()))
                variableName = string.Format("{1}{0}{2}", variableName, VariableStartChar, VariableEndChar);

            if(!string.IsNullOrEmpty(workingRoute))
                return workingRoute.Replace(variableName, variableValue);
            else
                return RouteFormat.Replace(variableName, variableValue);
        }

        #region Private Helper Methods
        private string RemoteVariableChars(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return input;

            string result = new string(input.ToArray());
            result = result.Replace(VariableStartChar.ToString(), string.Empty).Replace(VariableEndChar.ToString(), string.Empty);
            return result;
        }

        private string WrapWithVariableChars(string input)
        {
            return string.Format("{0}{1}{2}", VariableStartChar, input, VariableEndChar);
        }
        #endregion
    }
}
