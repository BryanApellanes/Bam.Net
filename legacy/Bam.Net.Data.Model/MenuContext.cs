/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bam.Net.Data.Model
{
    public abstract class MenuContext
    {
        public MenuContext(Type type, IModelActionMenuWriter writer)
        {
            this.Type = type;
            this.Writer = writer;
            this.Menu = new ModelActionMenu(type.Construct(), typeof(ModelActionAttribute));
        }

        public MenuContext(object value, IModelActionMenuWriter writer)
        {
            if (value != null)
            {
                this.Type = value.GetType();
            }
            this.Writer = writer;
            this.Menu = new ModelActionMenu(value, typeof(ModelActionAttribute));
        }        

        public abstract void Show();

        public object Value
        {
            get;
            set;
        }

        /// <summary>
        /// The menu writer
        /// </summary>
        public IModelActionMenuWriter Writer
        {
            get;
            private set;
        }

        public ModelActionMenu Menu
        {
            get;
            set;
        }

        /// <summary>
        /// The Type to work with
        /// </summary>
        public Type Type
        {
            get;
            private set;
        }

        /// <summary>
        /// The previous menu
        /// </summary>
        public MenuContext LastMenu
        {
            get;
            set;
        }
    }
}
