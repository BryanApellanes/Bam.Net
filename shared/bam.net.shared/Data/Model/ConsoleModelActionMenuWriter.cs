/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Bam.Net.Data.Model
{
    public class ConsoleModelActionMenuWriter: IModelActionMenuWriter
    {
        #region IActionMenuWriter Members

        public string Write(ModelActionMenu menu)
        {
            StringBuilder builder = new StringBuilder();
            int l = menu.Actions.Count;

            string title = string.Format("{0} [{1}]", menu.Provider.GetType().Name, menu.MethodMarkerType.Name);
            builder.AppendLine(title);
            builder.AppendLine();

            for (int i = 0; i < l; i++)
            {
                ModelAction action = menu.Actions[i];
                string name = action.Name;
                string desc = !string.IsNullOrEmpty(action.Description) ? string.Format("({0})", action.Description) : string.Empty;
                builder.AppendFormat("{0}. {1} {2}\r\n", i + 1, name, desc);
            }

            return builder.ToString();
        }

        #endregion
    }
}
