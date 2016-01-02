/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KLGates.Javascript.DataControls
{
    public interface IStateControl<T>
    {
        string Name { get; }
        T UserState { get; set; }
        T AppState { get; set; }
    }
}
