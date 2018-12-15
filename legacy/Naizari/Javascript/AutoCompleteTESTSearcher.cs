/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Text;

namespace Naizari.Javascript
{
    public class AutoCompleteTESTSearcher: IAutoCompleteSearcher
    {
        #region IAutoCompleteSearcher Members

        [JsonMethod]
        public AutoCompleteItem[] Search(string input)
        {
            List<AutoCompleteItem> returnValues = new List<AutoCompleteItem>();
            string id = System.Guid.NewGuid().ToString();
            int count;
            if (int.TryParse(input, out count))
            {
                for (int i = 0; i < count; i++)
                {
                    
                    AutoCompleteItem item = new AutoCompleteItem(id, id, false);
                    returnValues.Add(item);
                }
            }
            else
            {
                AutoCompleteItem item = new AutoCompleteItem(id, id, false);
                returnValues.Add(item);
            }
            return returnValues.ToArray();
        }

        #endregion
    }
}
