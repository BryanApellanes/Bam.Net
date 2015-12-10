/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net;

namespace nuver.Nuget
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
            SetVersion(version);
        }

        private void SetVersion(string version)
        {
            Args.ThrowIfNullOrEmpty(version, "version");
            this._partSetters = new Dictionary<int, Action<string>>();
            this._partSetters[0] = (s) =>
            {
                this.Major = s;
            };
            this._partSetters[1] = (s) =>
            {
                this.Minor = s;
            };
            this._partSetters[2] = (s) =>
            {
                this.Patch = s;
            };

            string[] parts = version.DelimitSplit(".");
            SetVersionParts(parts);
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

        public string Value
        {
            get
            {
                return "{0}.{1}.{2}"._Format(Major, Minor, Patch);
            }
        }

        public override string ToString()
        {
            return Value;
        }
    }
}
