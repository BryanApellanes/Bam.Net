/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rencopy
{
    public class RenCopyConfig
    {
		public RenCopyConfig()
		{
			this.TextReplacementFileExtensions = new string[] { };
			this.CopyExtensions = new string[] { };
			this.Ignore = new string[] { };
		}

        /// <summary>
        /// The file extensions to do text replacements on
        /// </summary>
        public string[] TextReplacementFileExtensions { get; set; }

        /// <summary>
        /// Indicates whether to copy all files or just those specified by the 
        /// TextReplacementFileExtensions and CopyExtensions properties
        /// </summary>
        public bool CopyAllFiles { get; set; }

        /// <summary>
        /// The file extenions to copy without doing text replacements
        /// </summary>
        public string[] CopyExtensions { get; set; }

        /// <summary>
        /// The source folder
        /// </summary>
        public string SourceFolder { get; set; }

        /// <summary>
        /// The target folder
        /// </summary>
        public string TargetFolder { get; set; }

        public TextReplacement[] TextReplacements { get; set; }

		/// <summary>
		/// The names or patterns of file names or folder names to ignore
		/// </summary>
		public string[] Ignore { get; set; }
    }
}
