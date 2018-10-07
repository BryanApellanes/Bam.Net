/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net;

namespace Bam.Net.Automation.Nuget
{
    public class PackageVersion
    {
        packageMetadata _meta;
        Dictionary<int, Action<string>> _partSetters;

        public PackageVersion(packageMetadata meta)
        {
            Args.ThrowIfNull(meta, "meta");

            this._meta = meta;
            SetVersion(this._meta.version);
        }

        public PackageVersion(string version)
        {
			this._meta = new packageMetadata();
            SetVersion(version);
        }

        private void SetVersion(string version)
        {
            version = string.IsNullOrEmpty(version) ? "1.0.0" : version;
            this._partSetters = new Dictionary<int, Action<string>>
            {
                [0] = (s) =>
                {
                    this.Major = s;
                },
                [1] = (s) =>
                {
                    this.Minor = s;
                },
                [2] = (s) =>
                {
                    this.Patch = s;
                }
            };

            string[] parts = version.DelimitSplit(".");
            SetVersionParts(parts);
            if (version.Contains("-"))
            {
                BuildNumber = version.Split(new char[] { '-' }, 2)[1];                
            }
        }

        internal packageMetadata MetaData
        {
            get
            {
                return _meta;
            }
            set
            {
                _meta = value;
            }
        }
        
        protected void SetVersionParts(string[] parts)
        {
            parts.Each((p, i) =>
            {
                if (_partSetters.ContainsKey(i))
                {
                    _partSetters[i](p);
                }
            });
        }

        public void IncrementMajor(int leadingZeroCount = 0)
        {
            int current = -1;
            if (int.TryParse(this.Major, out current))
            {
                int next = ++current;
                StringBuilder major = GetLeadingZeros(leadingZeroCount);
                major.Append(next);
                Major = major.ToString();
            }            
        }

        public void IncrementMinor(int leadingZeroCount = 0)
        {
            int current = -1;
            if (int.TryParse(this.Minor, out current))
            {
                int next = ++current;
                StringBuilder minor = GetLeadingZeros(leadingZeroCount);
                minor.Append(next);
                Minor = minor.ToString();
            }
        }

        public void IncrementPatch(int leadingZeroCount = 0)
        {
            int current = -1;
            if (int.TryParse(this.Patch, out current))
            {
                int next = ++current;
                StringBuilder patch = GetLeadingZeros(leadingZeroCount);
                patch.Append(next);
                Patch = patch.ToString();
            }
        }

        private static StringBuilder GetLeadingZeros(int leadingZeroCount)
        {
            StringBuilder major = new StringBuilder();
            leadingZeroCount.Times(i =>
            {
                major.Append("0");
            });
            return major;
        }

        string _major;
        public string Major
        {
            get
            {
                return _major;
            }
            set
            {
                _major = value;
                _meta.version = Value;
            }
        }

        string _minor;
        public string Minor
        {
            get
            {
                return _minor;
            }
            set
            {
                _minor = value;
                _meta.version = Value;
            }
        }

        string _patch;
        public string Patch
        {
            get
            {
                return _patch;
            }
            set
            {
                _patch = value;
                _meta.version = Value;
            }
        }

        string _buildNumber;
        public string BuildNumber
        {
            get
            {
                return _buildNumber;
            }
            set
            {
                _buildNumber = value;
                _meta.version = Value;
            }
        }

        public string Value
        {
            get
            {
                string value = "{0}.{1}.{2}"._Format(Major, Minor, Patch);
                if (!string.IsNullOrEmpty(BuildNumber))
                {
                    value = string.Format("{0}-{1}", value, BuildNumber);
                }
                return value;
            }
        }

        public override string ToString()
        {
            return Value;
        }
    }
}
