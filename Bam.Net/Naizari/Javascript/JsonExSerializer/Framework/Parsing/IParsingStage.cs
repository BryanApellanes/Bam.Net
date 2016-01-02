/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Text;
using Naizari.Javascript.JsonExSerialization.Framework.Expressions;

namespace Naizari.Javascript.JsonExSerialization.Framework.Parsing
{
    public interface IParsingStage
    {
        Expression Execute(Expression root);
    }
}
