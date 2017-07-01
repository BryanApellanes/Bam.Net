using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Data.Repositories;

namespace Bam.Net.Services.Data
{
    public class DataNodeDescriptor: RepoData
    {
        public DataNodeDescriptor()
        {
            Cuid = NCuid.Cuid.Generate();
        }
        public DataNodeDescriptor(object value):this()
        {
            Value = value;
        }
            
        public string Name { get; set; }
        public object Value { get; set; }

        public long ParentId { get; set; }

        public virtual DataNodeDescriptor Parent { get; set; }

        public virtual IEnumerable<DataNodeDescriptor> Children { get; set; }

        object _breadthFirstRecurseLock = new object();
        public IEnumerable<T> BreadthFirstForEachDataNode<T>(Func<DataNodeDescriptor, T> eacher)
        {
            lock (_breadthFirstRecurseLock)
            {
                Queue<DataNodeDescriptor> queue = new Queue<DataNodeDescriptor>();
                queue.Enqueue(this);
                while (queue.Count > 0)
                {
                    DataNodeDescriptor current = queue.Dequeue();
                    foreach(DataNodeDescriptor child in current.Children)
                    {
                        queue.Enqueue(child);
                    }
                    yield return eacher(current);                    
                }
            }
        }

        /// <summary>
        /// Visit each node recursively in a depth first way.  Can cause 
        /// intense memory pressure depending on the size and shape of the
        /// tree
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="eacher"></param>
        /// <returns></returns>
        public IEnumerable<T> DepthFirstForEachDataNode<T>(Func<DataNodeDescriptor, T> eacher)
        {
            return DepthFirstForEachDataNode(this, eacher);
        }

        /// <summary>
        /// Visit each node recursively in a depth first way.  Can cause 
        /// intense memory pressure depending on the size and shape of the
        /// tree
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="start"></param>
        /// <param name="eacher"></param>
        /// <returns></returns>
        public IEnumerable<T> DepthFirstForEachDataNode<T>(DataNodeDescriptor start, Func<DataNodeDescriptor, T> eacher)
        {
            List<T> results = new List<T>();
            foreach(DataNodeDescriptor child in start.Children)
            {
                results.AddRange(DepthFirstForEachDataNode(child, eacher));
            }

            results.Add(eacher(start));

            return results;
        }
    }
}
