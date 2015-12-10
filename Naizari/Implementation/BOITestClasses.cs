/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;

namespace Naizari.Implementation
{
    [BusinessObjectImplementation(typeof(UnimplementedInterfaceName))]
    public class BadBO
    {
    }

    public interface UnimplementedInterfaceName
    { }

    [BusinessObjectImplementation(typeof(IBOTest))]
    public class GoodBO: IBOTest
    {

    }

    public interface IBOTest
    {
    }
}
