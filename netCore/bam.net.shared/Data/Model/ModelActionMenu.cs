/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using Bam.Net;

namespace Bam.Net.Data.Model
{
    public class ModelActionMenu
    {
        public ModelActionMenu(object provider, Type methodMarker = null)
        {
            this.Provider = provider;
            this.MethodMarkerType = methodMarker;
            this.Initialize();
        }

        private void Initialize()
        {
            List<ModelAction> actions = new List<ModelAction>();
            Type type = this.Provider.GetType();
            MethodInfo[] methods = type.GetMethods();
            
            int l = methods.Length;
            for (int i = 0; i < l; i++)
            {
                MethodInfo current = methods[i];
                ModelAction toBeAdded = null;
                object attribute = null;
                if ((MethodMarkerType != null && current.HasCustomAttributeOfType(MethodMarkerType, out attribute)) ||
                    MethodMarkerType == null
                    )
                {
                    toBeAdded = new ModelAction(this.Provider, current);
                    if (attribute is ModelActionAttribute actionAttr)
                    {
                        toBeAdded.Description = actionAttr.Description;
                    }
                }

                if (toBeAdded != null)
                {
                    actions.Add(toBeAdded);
                }
            }

            this.Actions = actions;
        }

        public string Write(IModelActionMenuWriter writer)
        {
            return writer.Write(this);
        }

        public object RunSelection(string selection)
        {
            int num = 0;
            int.TryParse(selection, out num);
            if (num <= 0 || num > Actions.Count)
            {
                throw new ArgumentOutOfRangeException();
            }

            num = num - 1;
            return Actions[num].Run();
        }

        public List<ModelAction> Actions
        {
            get;
            private set;
        }

        /// <summary>
        /// The Attribute type that is marking the 
        /// methods that will be runnable from this
        /// menu
        /// </summary>
        public Type MethodMarkerType
        {
            get;
            private set;
        }
        
        public object Provider
        {
            get;
            private set;
        }
    }
}
