using Bam.Net.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net
{
    public enum ProcessModes
    {
        Dev,
        Test,
        Prod
    }

    /// <summary>
    /// Represents the mode that the current process is
    /// in.  Intended primarily to determine where to
    /// save and retrieve data from but can be used
    /// to pivot other configuration values as well.
    /// </summary>
    public class ProcessMode
    {
        static ProcessMode()
        {
            Current = FromConfig;
        }

        public ProcessModes Mode { get; set; }
        public static ProcessMode FromConfig
        {
            get
            {
                string fromConfig = DefaultConfiguration.GetAppSetting("ProcessMode", "Dev");
                return FromString(fromConfig);
            }
        }
        
        public static ProcessMode Current
        {
            get;
            set;
        }

        public static ProcessMode Dev { get { return new ProcessMode { Mode = ProcessModes.Dev }; } }
        public static ProcessMode Test { get { return new ProcessMode { Mode = ProcessModes.Test }; } }
        public static ProcessMode Prod { get { return new ProcessMode { Mode = ProcessModes.Prod }; } }
        
        public static ProcessMode FromString(string value)
        {
            return new ProcessMode { Mode = (ProcessModes)Enum.Parse(typeof(ProcessModes), value) };
        }

        public static ProcessMode FromEnum(ProcessModes enumVal)
        {
            return new ProcessMode { Mode = enumVal };
        }

        public override int GetHashCode()
        {
            return Mode.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if(obj is ProcessMode mode)
            {
                return mode.Mode.Equals(Mode);
            }
            return false;
        }

        public override string ToString()
        {
            return Mode.ToString();
        }
    }
}
