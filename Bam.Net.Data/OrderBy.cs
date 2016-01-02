/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bam.Net.Data
{
    public static class Order
    {
        public static OrderBy<C> By<C>(Func<C, C> column, SortOrder order = SortOrder.Descending) where C: IQueryFilter, IFilterToken, new()
        {
            return new OrderBy<C>(column, order);
        }
    }

    public class OrderBy<C> where C : IQueryFilter, IFilterToken, new()
    {
        public OrderBy(Func<C, C> column, SortOrder order)
        {
            this.SortOrder = order;
            C c = new C();
            this.Column = column(c);
        }

        public C Column { get; private set; }
        public SortOrder SortOrder { get; private set; }
    }
}
