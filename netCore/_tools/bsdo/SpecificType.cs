﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net;

namespace Bam.Net
{

    public class SpecificType
    {
        string _className;
        public string ClassName
        {
            get
            {
                if (_className == null)
                {
                    _className = TypeName;
                }
                return _className;
            }
            set
            {
                _className = value;
            }
        }
        string _typeName;
        public string TypeName
        {
            get
            {
                return _typeName;
            }
            set
            {
                string t = value.LettersOnly();
                _typeName = t.PascalCase(!t.IsAllCaps());
            }
        }
        public string Extends { get; set; }

        public override string ToString()
        {
            return "{TypeName} extends {Extends}".NamedFormat(this);
        }

        public override bool Equals(object obj)
        {
            SpecificType type = obj as SpecificType;
            if (type == null)
                return false;

            return ToString().Equals(type.ToString());
        }

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }
    }
}
