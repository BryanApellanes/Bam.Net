/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;

namespace Naizari
{
    public class ProgressStatus
    {

        public event ProgressStatusUpdatedEventHandler Updated;

        int total;
        /// <summary>
        /// Gets or sets the total number of operations in a set of operations
        /// tracked by the current ProgressStatus instance.
        /// </summary>
        public int Total 
        {
            get { return total; }
            set
            {
                total = value;
                //OnUpdated();
            }
        }

        private void OnUpdated()
        {
            if (Updated != null)
                Updated(this, new ProgressStatusEventArgs(this));
        }

        int current;
        /// <summary>
        /// Gets or sets the current operation in a set of operations 
        /// tracked by the current ProgressStatus instance.
        /// </summary>
        public int Current
        {
            get { return current; }
            set
            {
                current = value;
                OnUpdated();
            }
        }

        /// <summary>
        /// Gets the current percentage completed of a set of operations 
        /// tracked by the current ProgressStatus instance.
        /// </summary>
        public int PercentComplete
        {
            get 
            {
                if (Total > 0)
                    return (int)(((decimal)Current / (decimal)Total) * 100);
                else
                    return 100;
            }
        }

        string message;
        /// <summary>
        /// Gets or sets the message associated with the current operation of a set
        /// of operations tracked by the current ProgressStatus instance.
        /// </summary>
        public string Message
        {
            get { return message; }
            set
            {
                message = value;
                OnUpdated();
            }
        }

        bool isActive;
        /// <summary>
        /// Gets or sets the active state of the set of operations represented
        /// by the current ProgressStatus instance.
        /// </summary>
        public bool IsActive
        {
            get { return isActive; }
            set
            {
                isActive = value;
                OnUpdated();
            }
        }

        public void Reset()
        {
            isActive = false;
            message = string.Empty;
            total = 0;
            current = 0;
            OnUpdated();
        }
    }
}
