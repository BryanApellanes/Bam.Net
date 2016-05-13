﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Encryption;

namespace Bam.Net.Messaging
{
    public interface ISmtpSettingsProvider
    {
        string SmtpSettingsVaultPath { get; set; }
        /// <summary>
        /// When implemented should return
        /// an email whose smtp settings are already 
        /// set to those of the current IHasSmtpSettings
        /// implementation
        /// </summary>
        /// <returns></returns>
        Email CreateEmail(string fromAddress = null, string fromDisplayName = null);
    }
}
