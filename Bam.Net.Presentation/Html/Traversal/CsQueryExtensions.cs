using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsQuery;

namespace Bam.Net.Presentation.Html.Traversal
{
    public static class CsQueryExtensions
    {
        public static IDomElement NextSiblingElementOfType(this IDomObject obj, string elementType)
        {
            IDomElement result = obj.NextElementSibling;
            while(result != null)
            {
                if(result.NodeName.Equals(elementType, StringComparison.InvariantCultureIgnoreCase))
                {
                    return result;
                }
                result = result.NextElementSibling;
            }
            return result;
        }

        public static IEnumerable<IDomElement> EachSiblingUntil(this IDomObject obj, string elementType)
        {
            IDomElement next = obj.NextElementSibling;
            while(next != null)
            {
                if(next.NodeName.Equals(elementType, StringComparison.InvariantCultureIgnoreCase))
                {
                    break;
                }
                yield return next;
                next = next.NextElementSibling;
            }
        }

        public static IDomElement NextSiblingWhere(this IDomObject obj, Predicate<IDomObject> predicate)
        {
            IDomElement next = obj.NextElementSibling;
            while(next != null)
            {
                if (predicate(next))
                {
                    return next;
                }
                next = next.NextElementSibling;
            }
            return null;
        }
    }
}
